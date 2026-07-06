using System.Globalization;
using System.Threading.RateLimiting;
namespace learning_ms.Web.Infrastructure.RateLimiting.RateLimitingServiceCollectionExtensions;
public static class RateLimitingServiceCollectionExtensions
{
  public static IServiceCollection AddRateLimitingInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    var settings = BindSettings(configuration);
    services.AddSingleton(settings);

    services.AddRateLimiter(options =>
    {
      options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

      options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
          {
          var partitionKey = RateLimitPartitionKeyResolver.Resolve(httpContext);

          return RateLimitPartition.GetFixedWindowLimiter(
                  partitionKey,
                  _ => new FixedWindowRateLimiterOptions
                {
                  PermitLimit = settings.PermitLimit,
                  Window = TimeSpan.FromMinutes(settings.WindowMinutes),
                  QueueLimit = 0,
                  AutoReplenishment = true
                });
        });

      options.OnRejected = async (context, cancellationToken) =>
          {
          if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
          {
            context.HttpContext.Response.Headers.RetryAfter =
                    ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
          }

          context.HttpContext.Response.ContentType = "application/json";
          await context.HttpContext.Response.WriteAsync(
                  """{"error":"rate_limit_exceeded","message":"Too many requests. Please try again later."}""",
                  cancellationToken);
        };
    });

    return services;
  }

  private static RateLimitSettings BindSettings(IConfiguration configuration)
  {
    var section = configuration.GetSection(RateLimitSettings.SectionName);

    return new RateLimitSettings
    {
      PermitLimit = ReadInt(section, nameof(RateLimitSettings.PermitLimit), defaultValue: 1000),
      WindowMinutes = ReadInt(section, nameof(RateLimitSettings.WindowMinutes), defaultValue: 1)
    };
  }

  private static int ReadInt(IConfiguration section, string key, int defaultValue)
  {
    var raw = section[key];
    if (string.IsNullOrWhiteSpace(raw) || (raw.StartsWith("${") && raw.EndsWith('}')))
    {
      return defaultValue;
    }

    return int.TryParse(raw, out var parsed) ? parsed : defaultValue;
  }
}
