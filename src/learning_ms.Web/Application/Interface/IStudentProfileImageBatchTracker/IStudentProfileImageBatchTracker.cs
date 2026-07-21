namespace learning_ms.Web.Application.Interface.IStudentProfileImageBatchTracker;

public interface IStudentProfileImageBatchTracker
{
  Task RegisterBatchAsync(Guid studentProfileId, int imageCount, CancellationToken cancellationToken = default);
  Task<bool> MarkImageCompleteAsync(Guid studentProfileId, CancellationToken cancellationToken = default);
}
