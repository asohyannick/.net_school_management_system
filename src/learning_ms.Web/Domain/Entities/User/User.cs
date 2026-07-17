using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Domain.Entities.User;
public class User
{
  public Guid Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public bool IsActive { get; set; } = false;
  public UserRole Role { get; set; } = UserRole.Student;
  public string OTPCode { get; set; } = string.Empty;
  public string ResendOTPCode { get; set; } = string.Empty;
  public DateTime OTPExpirationDate { get; set; } = DateTime.UtcNow;
  public string AccessToken { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
  public DateTime RefreshTokenExpirationDate { get; set; } = DateTime.UtcNow;
  public string MagicLinkToken { get; set; } = string.Empty;
  public string ResendMagicLinkToken { get; set; } = string.Empty;
  public string VerifyMagicLinkToken { get; set;  } = string.Empty;
  public string ForgotPassword { get; set; } = string.Empty;
  
  public DateTime ForgotPasswordExpirationDate { get; set; } = DateTime.UtcNow;
  public string ResetPassword { get; set; } = string.Empty;
  public bool BlockUser { get; set; } = false;
  public bool UnBlockUser { get; set; } = false;
  public DateTime MagicLinkTokenExpirationDate { get; set; } = DateTime.UtcNow;
  // ---------- Firebase Authentication fields ----------
  public string? FirebaseUid { get; set; }
  

  public string? FirebaseProvider { get; set; }

  public string? FirebaseIdToken { get; set; }
  
  public int FailedLoginAttempts { get; set; } = 0;

  public string? FirebaseRefreshToken { get; set; }

  public bool IsFirebaseEmailVerified { get; set; } = false;

  public string? PhoneNumber { get; set; }

  public bool IsPhoneNumberVerified { get; set; } = false;

  public string? PhotoUrl { get; set; }

  public string? FirebaseDisplayName { get; set; }

  public DateTime? FirebaseLastLoginAt { get; set; }

  public DateTime? FirebaseCreatedAt { get; set; }

  public bool IsFirebaseDisabled { get; set; } = false;

  public string? FirebaseTenantId { get; set; }
  
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
