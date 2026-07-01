using learning_ms.Web.Application.Common.DTOs.FileUploadResult.FileUploadJobPayload;
namespace learning_ms.Web.Application.Interface.IFileUploadJob;
public interface IFileUploadJob
{
  Task ProcessAndUploadAsync(FileUploadJobPayload payload, CancellationToken cancellationToken);
}