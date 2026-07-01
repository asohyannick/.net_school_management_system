namespace learning_ms.Web.Application.Interface.IFileProcessingService;

public interface IFileProcessingService
{
  Task<string> EnqueueUploadAsync(
      IFormFile file,
      string folder,
      CancellationToken cancellationToken = default);

  Task<string> ScheduleUploadAsync(
      IFormFile file,
      string folder,
      TimeSpan delay,
      CancellationToken cancellationToken = default);
}