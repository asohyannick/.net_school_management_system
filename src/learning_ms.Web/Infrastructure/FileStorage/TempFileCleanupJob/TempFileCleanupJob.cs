using Hangfire;
namespace learning_ms.Web.Infrastructure.FileStorage.TempFileCleanupJob;

[AutomaticRetry(Attempts = 1)]
[Queue("maintenance")]
public sealed class TempFileCleanupJob
{
  private static readonly string TempDirectory =
      Path.Combine(Path.GetTempPath(), "file_upload_jobs");

  private static readonly TimeSpan MaxAge = TimeSpan.FromHours(1);

  private readonly ILogger<TempFileCleanupJob> _logger;

  public TempFileCleanupJob(ILogger<TempFileCleanupJob> logger)
  {
    _logger = logger;
  }

  [JobDisplayName("Temp File Cleanup")]
  public Task ExecuteAsync()
  {
    if (!Directory.Exists(TempDirectory))
      return Task.CompletedTask;

    var now = DateTime.UtcNow;
    var deleted = 0;
    var errors = 0;

    foreach (var file in Directory.EnumerateFiles(TempDirectory))
    {
      try
      {
        var age = now - File.GetCreationTimeUtc(file);
        if (age > MaxAge)
        {
          File.Delete(file);
          deleted++;
        }
      }
      catch (Exception ex)
      {
        errors++;
        _logger.LogWarning(ex, "[TempFileCleanupJob] Could not delete '{File}'", file);
      }
    }

    _logger.LogInformation(
        "[TempFileCleanupJob] Deleted {Deleted} orphaned temp file(s). Errors: {Errors}",
        deleted, errors);

    return Task.CompletedTask;
  }
}