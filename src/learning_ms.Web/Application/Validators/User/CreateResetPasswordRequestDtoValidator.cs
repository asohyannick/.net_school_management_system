using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
public class CreateResetPasswordRequestDtoValidator : AbstractValidator<CreateResetPasswordRequestDto>
{
  public CreateResetPasswordRequestDtoValidator()
  {
    RuleFor(x => x.NewPassword)
      .NotEmpty().WithMessage("New password is required.")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
      .MaximumLength(128).WithMessage("Password must not exceed 128 characters.")
      .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
      .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
      .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
      .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

    RuleFor(x => x.ConfirmPassword)
      .NotEmpty().WithMessage("Please confirm your new password.")
      .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
  }
}
