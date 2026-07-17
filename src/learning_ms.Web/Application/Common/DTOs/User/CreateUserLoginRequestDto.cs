namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateUserLoginRequestDto
{ 
   public string Email { get; set; } = string.Empty;
   public string Password { get; set; } = string.Empty;
}
