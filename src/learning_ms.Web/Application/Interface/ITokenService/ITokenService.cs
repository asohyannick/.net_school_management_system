using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Application.Interface.ITokenService;
using System.Security.Claims;
public interface ITokenService
{
  string GenerateAccessToken(
    Guid userId, 
    string email, 
    string FirstName, 
    string LastName, 
    UserRole role, 
    IEnumerable<Claim>? additionalClaims = null 
    );
  RefreshTokenResult GenerateRefreshToken();
  ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
public record RefreshTokenResult(string Token, DateTime ExpiresAtUtc);
