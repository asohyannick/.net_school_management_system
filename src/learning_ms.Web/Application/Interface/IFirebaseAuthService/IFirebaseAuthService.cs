namespace learning_ms.Web.Application.Interface.IFirebaseAuthService;

public record FirebaseTokenResult(
  string Uid,
  string? Email,
  bool EmailVerified,
  string? Name,
  string? PhotoUrl,
  string? ProviderId,
  string? PhoneNumber);

public interface IFirebaseAuthService
{
  Task<FirebaseTokenResult> VerifyIdTokenAsync(string idToken, CancellationToken cancellationToken = default);
}
