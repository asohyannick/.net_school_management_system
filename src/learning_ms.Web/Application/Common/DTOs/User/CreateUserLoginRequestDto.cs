namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateUserLoginRequestDto
{ 
   public required string Email { get; set; }
   public required string Password { get; set; }
}
