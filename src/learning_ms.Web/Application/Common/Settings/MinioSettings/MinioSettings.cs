namespace learning_ms.Web.Application.Common.Settings.MinioSettings;

public class MinioSettings
{
  public string Endpoint { get; set; } = string.Empty;
  public string AccessKey { get; set; } = string.Empty;
  public string SecretKey { get; set; } = string.Empty;
  public string BucketName { get; set; } = string.Empty;
  public bool UseSSL { get; set; } = true;
  public string Region { get; set; } = string.Empty;
}