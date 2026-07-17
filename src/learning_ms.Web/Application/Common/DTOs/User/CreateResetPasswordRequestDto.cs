namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateResetPasswordRequestDto
{
   public string NewPassword { get; set; } = string.Empty;
   public string ConfirmPassword { get; set; } = string.Empty;
}
