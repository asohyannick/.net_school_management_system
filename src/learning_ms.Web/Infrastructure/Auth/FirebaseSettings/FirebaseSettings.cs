namespace learning_ms.Web.Infrastructure.Auth.FirebaseSettings;
public sealed class FirebaseSettings
{
  public const string SectionName = "FirebaseSettings";
  public string ProjectId { get; set; } = string.Empty;
  public string KeyId { get; set; } = string.Empty;
  public string PrivateKey { get; set; } = string.Empty;
  public string ClientEmail { get; set; } = string.Empty;
  public string ClientId { get; set; } = string.Empty;
}
