using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateUserLoginResponseDto
{
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public required UserRole Role { get; set;  }
  public required bool IsActive { get; set;  }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
};
