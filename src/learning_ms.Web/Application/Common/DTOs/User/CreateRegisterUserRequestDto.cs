namespace learning_ms.Web.Application.Common.DTOs.User;

public record CreateRegisterUserRequestDto
{
   public required string FirstName { get; set; }
   public required string LastName { get; set; }
   public required string Email { get; set; }
   public required string Password { get; set; }
}
