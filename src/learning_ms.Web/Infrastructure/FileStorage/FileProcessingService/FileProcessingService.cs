using Hangfire;
using learning_ms.Web.Application.Common.DTOs.FileUploadResult.FileUploadJobPayload;
using learning_ms.Web.Application.Common.Settings.FileUploadSettings;
using learning_ms.Web.Application.Interface.IFileProcessingService;
using learning_ms.Web.Application.Interface.IFileUploadJob;
using learning_ms.Web.Infrastructure.FileStorage.FileValidator;
internal sealed class FileProcessingService : IFileProcessingService
{
  private readonly IBackgroundJobClient _jobClient;
  private readonly FileValidator _fileValidator;
  private readonly ILogger<FileProcessingService> _logger;

  private static readonly string TempDirectory =
      Path.Combine(Path.GetTempPath(), "file_upload_jobs");

  public FileProcessingService(
      IBackgroundJobClient jobClient,
      FileUploadSettings fileUploadSettings,
      ILogger<FileProcessingService> logger)
  {
    _jobClient = jobClient;
    _fileValidator = new FileValidator(fileUploadSettings);
    _logger = logger;

    Directory.CreateDirectory(TempDirectory);
  }

  public async Task<string> EnqueueUploadAsync(
      IFormFile file,
      string folder,
      Guid? studentProfileId = null,
      CancellationToken cancellationToken = default)
  {
    await _fileValidator.ValidateAsync(file, cancellationToken);

    var tempPath = await BufferToTempFileAsync(file, cancellationToken);

    var payload = BuildPayload(file, folder, tempPath, studentProfileId);

    var jobId = _jobClient.Enqueue<IFileUploadJob>(
        job => job.ProcessAndUploadAsync(payload, CancellationToken.None));

    _logger.LogInformation(
        "[FileProcessingService] Enqueued job {JobId} for file '{FileName}' → folder '{Folder}'",
        jobId, file.FileName, folder);

    return jobId;
  }

  public async Task<string> ScheduleUploadAsync(
      IFormFile file,
      string folder,
      TimeSpan delay,
      Guid? studentProfileId = null,
      CancellationToken cancellationToken = default)
  {
    await _fileValidator.ValidateAsync(file, cancellationToken);

    var tempPath = await BufferToTempFileAsync(file, cancellationToken);
    var payload = BuildPayload(file, folder, tempPath, studentProfileId);

    var jobId = _jobClient.Schedule<IFileUploadJob>(
        job => job.ProcessAndUploadAsync(payload, CancellationToken.None),
        delay);

    _logger.LogInformation(
        "[FileProcessingService] Scheduled job {JobId} for file '{FileName}' with delay {Delay}",
        jobId, file.FileName, delay);

    return jobId;
  }

  private static async Task<string> BufferToTempFileAsync(
      IFormFile file,
      CancellationToken ct)
  {
    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
    var tempPath = Path.Combine(TempDirectory, $"{Guid.NewGuid()}{extension}");

    await using var dest = new FileStream(
        tempPath, FileMode.Create, FileAccess.Write, FileShare.None,
        bufferSize: 81920, useAsync: true);

    await using var source = file.OpenReadStream();
    await source.CopyToAsync(dest, ct);

    return tempPath;
  }

  private static FileUploadJobPayload BuildPayload(
      IFormFile file,
      string folder,
      string tempPath,
      Guid? studentProfileId) => new()
      {
        TempFilePath = tempPath,
        OriginalFileName = file.FileName,
        ContentType = file.ContentType,
        FileSizeBytes = file.Length,
        Folder = folder,
        EnqueuedAtUtc = DateTime.UtcNow,
        StudentProfileId = studentProfileId,
      };
}
