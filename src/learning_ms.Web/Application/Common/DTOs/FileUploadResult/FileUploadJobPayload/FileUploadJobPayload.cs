namespace learning_ms.Web.Application.Common.DTOs.FileUploadResult.FileUploadJobPayload;
public sealed class FileUploadJobPayload
{
  public string TempFilePath { get; init; } = string.Empty;
  public string OriginalFileName { get; init; } = string.Empty;
  public string ContentType { get; init; } = string.Empty;
  public long FileSizeBytes { get; init; } = long.MinValue;
  public string Folder { get; init; } = string.Empty;
  public DateTime EnqueuedAtUtc { get; init; } = DateTime.UtcNow;
}
