using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
public class CreateForgotPasswordRequestDtoValidator : AbstractValidator<CreateForgotPasswordRequestDto>
{
  public CreateForgotPasswordRequestDtoValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("A valid email address is required.")
      .MaximumLength(256).WithMessage("Email must not exceed 256 characters.");
  }
}
