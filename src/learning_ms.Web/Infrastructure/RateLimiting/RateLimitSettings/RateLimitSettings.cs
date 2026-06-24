namespace learning_ms.Web.Infrastructure.RateLimiting;

public sealed class RateLimitSettings
{
    public const string SectionName = "RateLimitSettings";

    public int PermitLimit { get; set; } = 1000;

    public int WindowMinutes { get; set; } = 1;
}