using System.Security.Claims;

namespace learning_ms.Web.Infrastructure.RateLimiting;

public static class RateLimitPartitionKeyResolver
{
  private static readonly string[] UserIdClaimTypes =
  [
      ClaimTypes.NameIdentifier,
        "sub",
        "uid",
        "userId"
  ];

  public static string Resolve(HttpContext httpContext)
  {
    if (httpContext.User.Identity?.IsAuthenticated == true)
    {
      foreach (var claimType in UserIdClaimTypes)
      {
        var value = httpContext.User.FindFirst(claimType)?.Value;
        if (!string.IsNullOrWhiteSpace(value))
        {
          return $"user:{value}";
        }
      }

      var logger = httpContext.RequestServices
          .GetService<ILoggerFactory>()
          ?.CreateLogger("RateLimitPartitionKeyResolver");
      logger?.LogWarning(
          "Authenticated request had no recognizable user-id claim ({ClaimTypes}). " +
          "Falling back to IP-based rate limiting for this request.",
          string.Join(", ", UserIdClaimTypes));
    }

    var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    return $"ip:{ip}";
  }
}