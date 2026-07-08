namespace learning_ms.Web.Infrastructure.Auth.JwtSettings;
public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public required string SecretKey { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public int AccessTokenExpirationMinutes { get; init; } = 15;
    public int RefreshTokenExpirationDays { get; init; } = 7;
}
