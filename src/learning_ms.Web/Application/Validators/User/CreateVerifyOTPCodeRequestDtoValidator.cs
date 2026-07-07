using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
public class CreateVerifyOTPCodeRequestDtoValidator : AbstractValidator<CreateVerifyOTPCodeRequestDto>
{
  public CreateVerifyOTPCodeRequestDtoValidator()
  {
    RuleFor(x => x.VerifyOtpCode)
      .NotEmpty().WithMessage("OTP code is required.")
      .Matches(@"^\d{6}$").WithMessage("OTP code must be a 6-digit number.");
  }
}
