using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Interface.IFirebaseAuthService;
namespace learning_ms.Web.Infrastructure.Auth.FirebaseAuthService;
using FirebaseAdmin.Auth;
public class FirebaseAuthService : IFirebaseAuthService
{
  public async Task<FirebaseTokenResult> VerifyIdTokenAsync(
    string idToken, CancellationToken cancellationToken = default)
  {
    FirebaseToken decodedToken;
    try
    {
      decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken, cancellationToken);
    }
    catch (FirebaseAuthException ex)
    {
      Console.WriteLine($"Firebase verify failed: {ex.Message} | ErrorCode: {ex.AuthErrorCode}");
      throw new BadRequestException("Invalid or expired Firebase ID token.");
    }

    var claims = decodedToken.Claims;

    var email = claims.TryGetValue("email", out var emailValue) ? emailValue?.ToString() : null;
    var emailVerified = claims.TryGetValue("email_verified", out var verifiedValue)
                        && verifiedValue is bool b && b;
    var name = claims.TryGetValue("name", out var nameValue) ? nameValue?.ToString() : null;
    var picture = claims.TryGetValue("picture", out var pictureValue) ? pictureValue?.ToString() : null;
    var phoneNumber = claims.TryGetValue("phone_number", out var phoneValue) ? phoneValue?.ToString() : null;

    string? providerId = null;
    if (claims.TryGetValue("firebase", out var firebaseClaim) &&
        firebaseClaim is IDictionary<string, object> firebaseDict &&
        firebaseDict.TryGetValue("sign_in_provider", out var providerValue))
    {
      providerId = providerValue?.ToString();
    }

    return new FirebaseTokenResult(
      decodedToken.Uid, email, emailVerified, name, picture, providerId, phoneNumber);
  }
}
