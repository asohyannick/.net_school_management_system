namespace learning_ms.Web.Application.Common.DTOs.User;

public record CreateForgotPasswordRequestDto
{
   public required string Email { get; set; }
}
