using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using learning_ms.Web.Application.Interface.ITokenService;
using learning_ms.Web.Domain.Enums.UserRole;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace learning_ms.Web.Infrastructure.Auth.JwtTokenService;
public class JwtTokenService : ITokenService
{
    private readonly JwtSettings.JwtSettings _settings;
    public JwtTokenService(IOptions<JwtSettings.JwtSettings> settings)
    {
        _settings = settings.Value;
    }
    public AccessTokenResult GenerateAccessToken(
        Guid userId,
        string email,
        string firstName,
        string lastName,
        UserRole role,
        IEnumerable<Claim>? additionalClaims = null)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(ClaimTypes.GivenName, firstName),
            new(ClaimTypes.Surname, lastName),
            new(ClaimTypes.Role, role.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        if (additionalClaims is not null)
            claims.AddRange(additionalClaims);
    
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes);
    
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );
    
        return new AccessTokenResult(new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
    
    public RefreshTokenResult GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var token = Convert.ToBase64String(randomBytes);
        var expiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays);
        return new RefreshTokenResult(token, expiresAt);
    }
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = _settings.Audience,
            ValidateIssuer = true,
            ValidIssuer = _settings.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)),
            ValidateLifetime = false,
        };
        var handler = new JwtSecurityTokenHandler();
        ClaimsPrincipal principal;
        SecurityToken securityToken;
        try
        {
            principal = handler.ValidateToken(token, validationParameters, out securityToken);
        }
        catch
        {
            return null;
        }
        var isValidJwt = securityToken is JwtSecurityToken jwt &&
            jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        return isValidJwt ? principal : null;
    }
}
