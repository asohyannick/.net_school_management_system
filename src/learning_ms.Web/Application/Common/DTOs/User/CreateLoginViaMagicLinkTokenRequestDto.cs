namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateLoginViaMagicLinkTokenRequestDto
{
  public string Email { get; set; } = string.Empty;
}
