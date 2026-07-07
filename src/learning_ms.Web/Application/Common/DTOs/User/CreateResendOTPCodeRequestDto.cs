namespace learning_ms.Web.Application.Common.DTOs.User;

public record CreateResendOTPCodeRequestDto
{
  public required string Email { get; set;  }
};
