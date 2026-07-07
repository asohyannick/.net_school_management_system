namespace learning_ms.Web.Application.Common.DTOs.User;

public record CreateVerifyOTPCodeRequestDto
{
  public required string VerifyOtpCode { get; set; }
};
