namespace learning_ms.Web.Infrastructure.Payments.PayPal.PayPalServiceCollectionExtensions;

public static class PayPalServiceCollectionExtensions
{
    public const string PayPalHttpClientName = "PayPal";

    private const string SandboxBaseUrl = "https://api-m.sandbox.paypal.com";
    private const string LiveBaseUrl = "https://api-m.paypal.com";

    public static IServiceCollection AddPayPalConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection(PayPalSettings.SectionName);
        var settings = new PayPalSettings
        {
            ClientId = section[nameof(PayPalSettings.ClientId)] ?? string.Empty,
            SecretKey = section[nameof(PayPalSettings.SecretKey)] ?? string.Empty,
            Environment = NormalizeEnvironment(section[nameof(PayPalSettings.Environment)])
        };

        services.AddSingleton(settings);

        var baseUrl = settings.Environment == "live" ? LiveBaseUrl : SandboxBaseUrl;

        services.AddHttpClient(PayPalHttpClientName, client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        });

        if (LooksLikeUnresolvedPlaceholder(settings.ClientId) || LooksLikeUnresolvedPlaceholder(settings.SecretKey))
        {
            using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            loggerFactory.CreateLogger("PayPalServiceCollectionExtensions").LogWarning(
                "PayPalSettings:ClientId or SecretKey still contains an unresolved \"${{VAR}}\" " +
                "placeholder. Set PAYPAL_API_CLIENT / PAYPAL_SECRET_KEY before adding real PayPal calls.");
        }

        return services;
    }

    private static string NormalizeEnvironment(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw) || LooksLikeUnresolvedPlaceholder(raw))
        {
            return "sandbox";
        }

        var normalized = raw.ToLowerInvariant();
        return normalized == "live" ? "live" : "sandbox";
    }

    private static bool LooksLikeUnresolvedPlaceholder(string value) =>
        value.StartsWith("${") && value.EndsWith('}');
}