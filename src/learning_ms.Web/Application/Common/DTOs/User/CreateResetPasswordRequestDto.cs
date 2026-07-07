namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateResetPasswordRequestDto
{
   public required string NewPassword { get; set; }
   public required string ConfirmPassword { get; set; }
}
