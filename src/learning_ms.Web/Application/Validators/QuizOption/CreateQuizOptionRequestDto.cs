using learning_ms.Web.Application.Common.DTOs.QuizOption;
namespace learning_ms.Web.Application.Validators.QuizOption;
using FluentValidation;

public class CreateQuizOptionRequestDtoValidator : AbstractValidator<CreateQuizOptionRequestDto>
{
  public CreateQuizOptionRequestDtoValidator()
  {
    RuleFor(x => x.QuizQuestionId)
      .NotEmpty().WithMessage("QuizQuestionId is required.");

    RuleFor(x => x.OptionText)
      .NotEmpty().WithMessage("Option text is required.")
      .MaximumLength(500).WithMessage("Option text must not exceed 500 characters.");
  }
}
