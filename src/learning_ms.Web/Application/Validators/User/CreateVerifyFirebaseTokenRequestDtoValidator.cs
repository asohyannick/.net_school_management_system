using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
public class CreateVerifyFirebaseTokenRequestDtoValidator : AbstractValidator<CreateVerifyFirebaseTokenRequestDto>
{
  public CreateVerifyFirebaseTokenRequestDtoValidator()
  {
    RuleFor(x => x.VerifyFirebaseIdToken)
      .NotEmpty().WithMessage("Firebase ID token is required.");
  }
}
