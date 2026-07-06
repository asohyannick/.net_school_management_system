using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Domain.Entities.User;
public class User
{
  public Guid Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public bool IsActive { get; set; } = true;
  public UserRole Role { get; set; } = UserRole.Student;
  public string OTPCode { get; set; } = string.Empty;
  public string ResentOTPCode { get; set; } = string.Empty;
  public DateTime OTPExpirationDate { get; set; } = DateTime.UtcNow;
  public string AccessToken { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
  public string MagicLinkToken { get; set; } = string.Empty;
  public string ResendMagicLinkToken { get; set; } = string.Empty;
  public string ForgotPassword { get; set; } = string.Empty;
  public string ResetPassword { get; set; } = string.Empty;
  public bool BlockUser { get; set; } = false;
  public bool UnBlockUser { get; set; } = false;
  public DateTime MagicLinkTokenExpirationDate { get; set; } = DateTime.UtcNow;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
