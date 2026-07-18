using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Application.Interface.ITokenService;
using System.Security.Claims;
public interface ITokenService
{
  AccessTokenResult GenerateAccessToken(
    Guid userId,
    string email,
    string FirstName,
    string LastName,
    UserRole role,
    IEnumerable<Claim>? additionalClaims = null
  );

  RefreshTokenResult GenerateRefreshToken(Guid userId);

  ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);

  ClaimsPrincipal? ValidateRefreshToken(string token);
}

public record AccessTokenResult(string Token, DateTime ExpiresAtUtc);
public record RefreshTokenResult(string Token, DateTime ExpiresAtUtc);
