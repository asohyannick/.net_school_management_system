using learning_ms.Web.Application.Common.Settings.CorsSettings;
using learning_ms.Web.Application.Exceptions.NotFoundException;

namespace learning_ms.Web.Presentation.Extensions.CorsServiceExtensions;

public static class CorsServiceExtensions
{
  public static IServiceCollection AddCorsPolicies(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var settings =
      configuration.GetSection("CorsSettings").Get<CorsSettings>()
      ?? throw new NotFoundException("CorsSettings section is missing from configuration.");

    var rawOrigins = configuration["CorsSettings:AllowedOrigins"] ?? string.Empty;

    if (!string.IsNullOrWhiteSpace(rawOrigins))
    {
      settings.AllowedOrigins = rawOrigins.Split(
        ',',
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
      );
    }

    if (settings.AllowedOrigins.Length == 0)
      throw new InvalidOperationException(
        "CorsSettings.AllowedOrigins is empty. "
          + "Set the CORS_ALLOWED_ORIGINS environment variable."
      );

    services.AddSingleton(settings);

    services.AddCors(options =>
    {
      options.AddPolicy(
        settings.PolicyName,
        policy =>
        {
          policy
            .WithOrigins(settings.AllowedOrigins)
            .WithMethods(settings.AllowedMethods)
            .WithHeaders(settings.AllowedHeaders)
            .WithExposedHeaders(settings.ExposedHeaders)
            .SetPreflightMaxAge(TimeSpan.FromSeconds(settings.PreflightMaxAgeSeconds));

          if (settings.AllowCredentials)
            policy.AllowCredentials();
          else
            policy.DisallowCredentials();
        }
      );

      options.AddPolicy(
        "HealthCheckPolicy",
        policy =>
        {
          policy.WithOrigins(settings.AllowedOrigins).WithMethods("GET").DisallowCredentials();
        }
      );
    });

    return services;
  }

  public static WebApplication UseCorsPolicy(this WebApplication app, IConfiguration configuration)
  {
    var settings = app.Services.GetRequiredService<CorsSettings>();

    var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("CORS");

    logger.LogInformation(
      "CORS policy '{Policy}' active for {Count} origin(s): {Origins}",
      settings.PolicyName,
      settings.AllowedOrigins.Length,
      string.Join(", ", settings.AllowedOrigins)
    );

    app.UseCors(settings.PolicyName);

    return app;
  }
}
