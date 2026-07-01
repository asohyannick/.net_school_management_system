namespace learning_ms.Web.Infrastructure.Payments.PayPal.PayPalServiceCollectionExtensions;

public static class PayPalServiceCollectionExtensions
{
  public const string PayPalHttpClientName = "PayPal";

  private const string SandboxBaseUrl = "https://api-m.sandbox.paypal.com";
  private const string LiveBaseUrl = "https://api-m.paypal.com";

  public static IServiceCollection AddPayPalConfiguration(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var section = configuration.GetSection(PayPalSettings.SectionName);
    var settings = new PayPalSettings
    {
      ClientId = section[nameof(PayPalSettings.ClientId)] ?? string.Empty,
      SecretKey = section[nameof(PayPalSettings.SecretKey)] ?? string.Empty,
      Environment = NormalizeEnvironment(section[nameof(PayPalSettings.Environment)]),
    };

    services.Configure<PayPalSettings>(configuration.GetSection(PayPalSettings.SectionName));

    services.AddSingleton(sp =>
      sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<PayPalSettings>>().Value
    );

    var baseUrl = settings.Environment == "live" ? LiveBaseUrl : SandboxBaseUrl;

    services.AddHttpClient(
      PayPalHttpClientName,
      client =>
      {
        client.BaseAddress = new Uri(baseUrl);
      }
    );
    return services;
  }

  private static string NormalizeEnvironment(string? environment)
  {
    if (IsInvalid(environment))
      return "sandbox";

    var normalized = environment!.Trim().ToLowerInvariant();

    return normalized switch
    {
      "live" => "live",
      "sandbox" => "sandbox",
      _ => "sandbox",
    };
  }

  private static bool IsInvalid(string? value)
  {
    return string.IsNullOrWhiteSpace(value)
      || value.Contains("${", StringComparison.Ordinal)
      || value.Equals("placeholder", StringComparison.OrdinalIgnoreCase);
  }
}
