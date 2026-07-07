using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
public class CreateVerifyMagicLinkTokenRequestDtoValidator : AbstractValidator<CreateVerifyMagicLinkTokenRequestDto>
{
  public CreateVerifyMagicLinkTokenRequestDtoValidator()
  {
    RuleFor(x => x.VerifyMagicLinkToken)
      .NotEmpty().WithMessage("Magic link token is required.");
  }
}
