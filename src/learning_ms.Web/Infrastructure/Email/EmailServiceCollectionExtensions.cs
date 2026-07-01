using learning_ms.Web.Application.Interface.IEmailService;
using Resend;
namespace learning_ms.Web.Infrastructure.Email;

public static class EmailServiceCollectionExtensions
{
   public static IServiceCollection AddEmailInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection(ResendSettings.SectionName);
        var settings = new ResendSettings
        {
            ApiKey = section[nameof(ResendSettings.ApiKey)] ?? string.Empty,
            SenderEmail = section[nameof(ResendSettings.SenderEmail)] ?? string.Empty,
            SenderName = section[nameof(ResendSettings.SenderName)] ?? string.Empty
        };

        services.AddSingleton(settings);

        services.AddResend(o => o.ApiToken = settings.ApiKey);

        services.AddScoped<IEmailService, ResendEmailService>();

        if (LooksLikeUnresolvedPlaceholder(settings.ApiKey) || LooksLikeUnresolvedPlaceholder(settings.SenderEmail))
        {
            using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            loggerFactory.CreateLogger("EmailServiceCollectionExtensions").LogWarning(
                "ResendSettings:ApiKey or SenderEmail still contains an unresolved \"${{VAR}}\" " +
                "placeholder. Email sending will fail until RESEND_API_KEY / RESEND_SENDER_EMAIL " +
                "are set in your environment (.env, docker-compose, or App Settings).");
        }

        return services;
    }

    private static bool LooksLikeUnresolvedPlaceholder(string value) =>
     value.StartsWith("${") && value.EndsWith('}');
}
