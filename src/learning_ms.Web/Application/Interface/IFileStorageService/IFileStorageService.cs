using learning_ms.Web.Application.Common.DTOs.FileUploadResult;
namespace learning_ms.Web.Application.Interface.IFileStorageService;
public interface IFileStorageService
{
  Task<FileUploadResult> UploadAsync(
    IFormFile file,
    string folder,
    CancellationToken cancellationToken = default);

  Task<string> GetPresignedUrlAsync(
      string objectName,
      int expiryHours = 1,
      CancellationToken cancellationToken = default);

  Task DeleteAsync(string objectName, CancellationToken cancellationToken = default);

  Task<bool> ExistsAsync(string objectName, CancellationToken cancellationToken = default);
}
