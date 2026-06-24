using learning_ms.Web.Application.Common.DTOs.FileUploadResult;
using learning_ms.Web.Application.Common.Settings.FileUploadSettings;
using learning_ms.Web.Application.Common.Settings.MinioSettings;
using learning_ms.Web.Application.Interface.IFileStorageService;
using learning_ms.Web.Infrastructure.FileStorage.FileValidator;
using Minio;
using Minio.DataModel.Args;
internal sealed class MinioFileStorageService : IFileStorageService
{
  private readonly IMinioClient _minioClient;
  private readonly MinioSettings _minioSettings;
  private readonly FileValidator _fileValidator;
  private readonly ILogger<MinioFileStorageService> _logger;

  public MinioFileStorageService(
      IMinioClient minioClient,
      MinioSettings minioSettings,
      FileUploadSettings fileUploadSettings,
      ILogger<MinioFileStorageService> logger)
  {
    _minioClient = minioClient;
    _minioSettings = minioSettings;
    _fileValidator = new FileValidator(fileUploadSettings);
    _logger = logger;
  }
  public async Task<FileUploadResult> UploadAsync(
      IFormFile file,
      string folder,
      CancellationToken cancellationToken = default)
  {
    await _fileValidator.ValidateAsync(file, cancellationToken);

    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
    var objectName = $"{folder.TrimEnd('/')}/{Guid.NewGuid()}{extension}";

    await EnsureBucketExistsAsync(cancellationToken);

    await using var stream = file.OpenReadStream();

    var putArgs = new PutObjectArgs()
        .WithBucket(_minioSettings.BucketName)
        .WithObject(objectName)
        .WithStreamData(stream)
        .WithObjectSize(file.Length)
        .WithContentType(file.ContentType);

    await _minioClient.PutObjectAsync(putArgs, cancellationToken);

    _logger.LogInformation(
        "Uploaded file {OriginalName} → {ObjectName} ({Bytes} bytes)",
        file.FileName, objectName, file.Length);

    var url = BuildPublicUrl(objectName);

    return new FileUploadResult(
        ObjectName: objectName,
        BucketName: _minioSettings.BucketName,
        Url: url,
        OriginalFileName: file.FileName,
        ContentType: file.ContentType,
        FileSizeBytes: file.Length);
  }
  public async Task<string> GetPresignedUrlAsync(
      string objectName,
      int expiryHours = 1,
      CancellationToken cancellationToken = default)
  {
    var args = new PresignedGetObjectArgs()
        .WithBucket(_minioSettings.BucketName)
        .WithObject(objectName)
        .WithExpiry(expiryHours * 3600);

    return await _minioClient.PresignedGetObjectAsync(args);
  }
  public async Task DeleteAsync(string objectName, CancellationToken cancellationToken = default)
  {
    var args = new RemoveObjectArgs()
        .WithBucket(_minioSettings.BucketName)
        .WithObject(objectName);

    await _minioClient.RemoveObjectAsync(args, cancellationToken);

    _logger.LogInformation("Deleted MinIO object {ObjectName}", objectName);
  }
  public async Task<bool> ExistsAsync(string objectName, CancellationToken cancellationToken = default)
  {
    try
    {
      var args = new StatObjectArgs()
          .WithBucket(_minioSettings.BucketName)
          .WithObject(objectName);

      await _minioClient.StatObjectAsync(args, cancellationToken);
      return true;
    }
    catch (Minio.Exceptions.ObjectNotFoundException)
    {
      return false;
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
      _logger.LogInformation("Created MinIO bucket '{Bucket}'", _minioSettings.BucketName);
    }
  }

  private string BuildPublicUrl(string objectName)
  {
    var scheme = _minioSettings.UseSSL ? "https" : "http";
    return $"{scheme}://{_minioSettings.Endpoint}/{_minioSettings.BucketName}/{objectName}";
  }
}