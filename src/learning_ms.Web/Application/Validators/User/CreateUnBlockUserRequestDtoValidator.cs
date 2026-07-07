using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
public class CreateUnBlockUserRequestDtoValidator : AbstractValidator<CreateUnBlockUserRequestDto>
{
  public CreateUnBlockUserRequestDtoValidator()
  {
    RuleFor(x => x.Reason)
      .NotEmpty().WithMessage("A reason is required to unblock a user.")
      .MaximumLength(500).WithMessage("Reason must not exceed 500 characters.");
  }
}
