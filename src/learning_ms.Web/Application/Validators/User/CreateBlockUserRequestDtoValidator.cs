using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
public class CreateBlockUserRequestDtoValidator : AbstractValidator<CreateBlockUserRequestDto>
{
  public CreateBlockUserRequestDtoValidator()
  {
    RuleFor(x => x.Reason)
      .NotEmpty().WithMessage("A reason is required to block a user.")
      .MaximumLength(500).WithMessage("Reason must not exceed 500 characters.");
  }
}
