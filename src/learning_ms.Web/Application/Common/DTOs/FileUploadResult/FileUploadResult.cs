namespace learning_ms.Web.Application.Common.DTOs.FileUploadResult;
public record FileUploadResult(
    string ObjectName,
    string BucketName,
    string Url,
    string OriginalFileName,
    string ContentType,
    long FileSizeBytes
);
