using learning_ms.Web.Infrastructure.Payments.Stripe.StripeSettings;
public static class StripeServiceCollectionExtensions
{
    public static IServiceCollection AddStripeConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection(StripeSettings.SectionName);
        var settings = new StripeSettings
        {
            ApiKey = section[nameof(StripeSettings.ApiKey)] ?? string.Empty,
            PublishableKey = section[nameof(StripeSettings.PublishableKey)] ?? string.Empty,
            WebhookSecret = section[nameof(StripeSettings.WebhookSecret)] ?? string.Empty,
            DefaultCurrency = NormalizeCurrency(section[nameof(StripeSettings.DefaultCurrency)])
        };

        services.AddSingleton(settings);

        global::Stripe.StripeConfiguration.ApiKey = settings.ApiKey;

        if (LooksLikeUnresolvedPlaceholder(settings.ApiKey))
        {
            using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            loggerFactory.CreateLogger("StripeServiceCollectionExtensions").LogWarning(
                "StripeSettings:ApiKey still contains an unresolved \"${{VAR}}\" placeholder. " +
                "Set STRIPE_API_KEY in your environment before adding real Stripe calls.");
        }

        return services;
    }

    private static string NormalizeCurrency(string? raw) =>
        string.IsNullOrWhiteSpace(raw) || LooksLikeUnresolvedPlaceholder(raw)
            ? "usd"
            : raw.ToLowerInvariant();

    private static bool LooksLikeUnresolvedPlaceholder(string value) =>
        value.StartsWith("${") && value.EndsWith('}');
}