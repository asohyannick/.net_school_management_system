namespace learning_ms.Web.Application.Common.DTOs.User;

public record CreateResendOTPCodeRequestDto
{
  public string Email { get; set; } = string.Empty;
};
