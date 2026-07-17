namespace learning_ms.Web.Application.Common.DTOs.User;

public record CreateForgotPasswordRequestDto
{
  public string Email { get; set; } = string.Empty;
}
