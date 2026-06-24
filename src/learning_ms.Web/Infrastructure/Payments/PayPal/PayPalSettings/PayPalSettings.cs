namespace learning_ms.Web.Infrastructure.Payments.PayPal;
public sealed class PayPalSettings
{
  public const string SectionName = "PayPalSettings";
  public string ClientId { get; set; } = string.Empty;
  public string SecretKey { get; set; } = string.Empty;
  public string Environment { get; set; } = "sandbox";
}