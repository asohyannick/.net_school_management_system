using Hangfire;
using learning_ms.Web.Application.Common.DTOs.FileUploadResult.FileUploadJobPayload;
using learning_ms.Web.Application.Common.DTOs.FileUploadResult;
using learning_ms.Web.Application.Common.Settings.FileUploadSettings;
using learning_ms.Web.Application.Common.Settings.MinioSettings;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IFileUploadJob;
using learning_ms.Web.Application.Interface.IStudentProfileRepository;
using learning_ms.Web.Domain.Exceptions.FileValidationException;
using learning_ms.Web.Infrastructure.FileStorage.FileValidator;
using Minio;
using Minio.DataModel.Args;

[AutomaticRetry(Attempts = 3, DelaysInSeconds = [30, 120, 300])]
[Queue("file-processing")]
public sealed class FileUploadJob : IFileUploadJob
{
  private readonly IMinioClient _minioClient;
  private readonly MinioSettings _minioSettings;
  private readonly FileValidator _fileValidator;
  private readonly IStudentProfileRepository _studentProfileRepository;
  private readonly IEmailService _emailService;
  private readonly IConfiguration _configuration;
  private readonly ILogger<FileUploadJob> _logger;

  public FileUploadJob(
      IMinioClient minioClient,
      MinioSettings minioSettings,
      FileUploadSettings fileUploadSettings,
      IStudentProfileRepository studentProfileRepository,
      IEmailService emailService,
      IConfiguration configuration,
      ILogger<FileUploadJob> logger)
  {
    _minioClient = minioClient;
    _minioSettings = minioSettings;
    _fileValidator = new FileValidator(fileUploadSettings);
    _studentProfileRepository = studentProfileRepository;
    _emailService = emailService;
    _configuration = configuration;
    _logger = logger;
  }

  public async Task ProcessAndUploadAsync(
      FileUploadJobPayload payload,
      CancellationToken cancellationToken = default)
  {
    _logger.LogInformation(
        "[FileUploadJob] Starting — File: {FileName} | Folder: {Folder} | Enqueued: {EnqueuedAt}",
        payload.OriginalFileName, payload.Folder, payload.EnqueuedAtUtc);

    try
    {
      if (!File.Exists(payload.TempFilePath))
        throw new FileValidationException(
            $"Temp file not found at '{payload.TempFilePath}'. " +
            "It may have been deleted before the job ran.");

      await ValidateFromTempFileAsync(payload, cancellationToken);

      var processedPath = await ProcessFileAsync(payload, cancellationToken);

      var result = await UploadToMinioAsync(payload, processedPath, cancellationToken);

      _logger.LogInformation(
          "[FileUploadJob] Completed — Object: {ObjectName} | URL: {Url} | Size: {Bytes} bytes",
          result.ObjectName, result.Url, result.FileSizeBytes);

      if (payload.StudentProfileId.HasValue)
      {
        await HandleStudentProfileImageCompletionAsync(
            payload.StudentProfileId.Value, result.Url, cancellationToken);
      }
    }
    finally
    {
      CleanupTempFiles(payload.TempFilePath);
    }
  }

  private async Task HandleStudentProfileImageCompletionAsync(
      Guid studentProfileId, string imageUrl, CancellationToken ct)
  {
    var remaining = await _studentProfileRepository.AppendImageUrlAndDecrementPendingAsync(
        studentProfileId, imageUrl, ct);

    _logger.LogInformation(
        "[FileUploadJob] Recorded image for StudentProfile {Id} — {Remaining} image(s) still pending.",
        studentProfileId, remaining);

    if (remaining != 0)
    {
      return; 
    }

    var profile = await _studentProfileRepository.GetByIdAsync(studentProfileId, ct);
    if (profile is null || string.IsNullOrWhiteSpace(profile.Email))
    {
      _logger.LogWarning(
          "[FileUploadJob] Could not send activation email for StudentProfile {Id} — profile or email missing.",
          studentProfileId);
      return;
    }

    var frontendBaseUrl = _configuration["FrontendSettings:BaseUrl"] ?? "http://localhost:3000";
    var activationUrl = $"{frontendBaseUrl.TrimEnd('/')}/activate?studentProfileId={profile.Id}";

    try
    {
      await _emailService.SendAccountActivationEmailAsync(
          profile.Email, profile.FirstName, activationUrl, ct);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
          "[FileUploadJob] Failed to send activation email for StudentProfile {Id}.", studentProfileId);
    }
  }

  private async Task ValidateFromTempFileAsync(
      FileUploadJobPayload payload,
      CancellationToken ct)
  {
    _logger.LogInformation(
        "[FileUploadJob] Validating {FileName} ({ContentType})",
        payload.OriginalFileName, payload.ContentType);

    await using var stream = new FileStream(
        payload.TempFilePath, FileMode.Open, FileAccess.Read, FileShare.Read,
        bufferSize: 4096, useAsync: true);

    var formFile = new TempFormFile(stream, payload.OriginalFileName, payload.ContentType, payload.FileSizeBytes);
    await _fileValidator.ValidateAsync(formFile, ct);
  }

  private async Task<string> ProcessFileAsync(
      FileUploadJobPayload payload,
      CancellationToken ct)
  {
    var mime = payload.ContentType.ToLowerInvariant();

    return mime switch
    {
      "image/jpeg" or "image/png"
          => await ProcessImageAsync(payload, ct),

      "application/pdf"
          => await ProcessDocumentAsync(payload, ct),

      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
      or "application/vnd.ms-excel"
      or "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
      or "application/msword"
      or "text/csv"
          => await ProcessDocumentAsync(payload, ct),

      "video/mp4" or "video/x-msvideo" or "video/x-matroska"
          => await ProcessVideoAsync(payload, ct),

      _ => payload.TempFilePath
    };
  }

  private async Task<string> ProcessImageAsync(
      FileUploadJobPayload payload,
      CancellationToken ct)
  {
    _logger.LogInformation(
        "[FileUploadJob] Processing image: {FileName}", payload.OriginalFileName);

    await Task.CompletedTask;
    return payload.TempFilePath;
  }

  private async Task<string> ProcessDocumentAsync(
      FileUploadJobPayload payload,
      CancellationToken ct)
  {
    _logger.LogInformation(
        "[FileUploadJob] Processing document: {FileName}", payload.OriginalFileName);

    await Task.CompletedTask;
    return payload.TempFilePath;
  }

  private async Task<string> ProcessVideoAsync(
      FileUploadJobPayload payload,
      CancellationToken ct)
  {
    _logger.LogInformation(
        "[FileUploadJob] Processing video: {FileName}", payload.OriginalFileName);
    await Task.CompletedTask;
    return payload.TempFilePath;
  }

  private async Task<FileUploadResult> UploadToMinioAsync(
      FileUploadJobPayload payload,
      string filePath,
      CancellationToken ct)
  {
    await EnsureBucketExistsAsync(ct);

    var extension = Path.GetExtension(payload.OriginalFileName).ToLowerInvariant();
    var objectName = $"{payload.Folder.TrimEnd('/')}/{Guid.NewGuid()}{extension}";
    var fileSize = new FileInfo(filePath).Length;

    _logger.LogInformation(
        "[FileUploadJob] Uploading to MinIO — Bucket: {Bucket} | Object: {Object}",
        _minioSettings.BucketName, objectName);

    await using var stream = new FileStream(
        filePath, FileMode.Open, FileAccess.Read, FileShare.Read,
        bufferSize: 81920, useAsync: true);

    var putArgs = new PutObjectArgs()
        .WithBucket(_minioSettings.BucketName)
        .WithObject(objectName)
        .WithStreamData(stream)
        .WithObjectSize(fileSize)
        .WithContentType(payload.ContentType);

    await _minioClient.PutObjectAsync(putArgs, ct);

    var scheme = _minioSettings.UseSSL ? "https" : "http";
    var url = $"{scheme}://{_minioSettings.Endpoint}/{_minioSettings.BucketName}/{objectName}";

    return new FileUploadResult(
        ObjectName: objectName,
        BucketName: _minioSettings.BucketName,
        Url: url,
        OriginalFileName: payload.OriginalFileName,
        ContentType: payload.ContentType,
        FileSizeBytes: fileSize);
  }

  private void CleanupTempFiles(string tempFilePath)
  {
    try
    {
      if (File.Exists(tempFilePath))
      {
        File.Delete(tempFilePath);
        _logger.LogInformation(
            "[FileUploadJob] Cleaned up temp file: {Path}", tempFilePath);
      }
    }
    catch (Exception ex)
    {
      _logger.LogWarning(ex,
          "[FileUploadJob] Failed to delete temp file: {Path}", tempFilePath);
    }
  }

  private async Task EnsureBucketExistsAsync(CancellationToken ct)
  {
    var existsArgs = new BucketExistsArgs().WithBucket(_minioSettings.BucketName);
    bool exists = await _minioClient.BucketExistsAsync(existsArgs, ct);

    if (!exists)
    {
      var makeArgs = new MakeBucketArgs()
          .WithBucket(_minioSettings.BucketName)
          .WithLocation(_minioSettings.Region);

      await _minioClient.MakeBucketAsync(makeArgs, ct);
      _logger.LogInformation(
          "[FileUploadJob] Created MinIO bucket '{Bucket}'", _minioSettings.BucketName);
    }
  }
}

internal sealed class TempFormFile : IFormFile
{
  private readonly Stream _stream;

  public TempFormFile(Stream stream, string fileName, string contentType, long length)
  {
    _stream = stream;
    FileName = fileName;
    ContentType = contentType;
    Length = length;
  }

  public string ContentType { get; }
  public string ContentDisposition => $"form-data; name=\"file\"; filename=\"{FileName}\"";
  public IHeaderDictionary Headers => new HeaderDictionary();
  public long Length { get; }
  public string Name => "file";
  public string FileName { get; }

  public void CopyTo(Stream target) => _stream.CopyTo(target);
  public Task CopyToAsync(Stream target, CancellationToken ct) => _stream.CopyToAsync(target, ct);
  public Stream OpenReadStream() => _stream;
}
