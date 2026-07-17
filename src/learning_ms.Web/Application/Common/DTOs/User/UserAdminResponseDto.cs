using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Application.Common.DTOs.User;
public record UserAdminResponseDto
{
  public Guid Id { get; set; } = Guid.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public UserRole Role { get; set; } = UserRole.AcademicDirector;
  public bool IsActive { get; set; } = false;
  public bool BlockUser { get; set; } = false;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
