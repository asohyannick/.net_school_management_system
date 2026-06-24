namespace learning_ms.Web.Infrastructure.Payments.Stripe.StripeSettings;
public sealed class StripeSettings
{
  public const string SectionName = "StripeSettings";

  public string ApiKey { get; set; } = string.Empty;

  public string PublishableKey { get; set; } = string.Empty;
  
  public string WebhookSecret { get; set; } = string.Empty;

  public string DefaultCurrency { get; set; } = "usd";
}