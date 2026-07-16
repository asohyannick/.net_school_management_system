namespace learning_ms.Web.Application.Common.DTOs.User;

public record CreateVerifyOTPCodeRequestDto
{
  public string VerifyOtpCode { get; set; } = string.Empty;
};
