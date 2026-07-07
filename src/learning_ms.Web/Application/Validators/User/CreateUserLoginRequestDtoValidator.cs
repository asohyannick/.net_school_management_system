using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
public class CreateUserLoginRequestDtoValidator : AbstractValidator<CreateUserLoginRequestDto>
{
  public CreateUserLoginRequestDtoValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("A valid email address is required.");
    
    RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is required.")
      .MaximumLength(128).WithMessage("Password must not exceed 128 characters.");
  }
}
