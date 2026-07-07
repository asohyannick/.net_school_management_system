namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateLoginViaMagicLinkTokenRequestDto
{
  public required string Email { get; set; }
}
