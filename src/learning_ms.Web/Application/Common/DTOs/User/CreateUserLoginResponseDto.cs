using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateUserLoginResponseDto
{
  public  string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public UserRole Role { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
};
