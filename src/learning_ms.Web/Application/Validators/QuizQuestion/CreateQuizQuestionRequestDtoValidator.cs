using learning_ms.Web.Application.Common.DTOs.QuizQuestion;
namespace learning_ms.Web.Application.Validators.QuizQuestion;
using FluentValidation;
public class CreateQuizQuestionRequestDtoValidator : AbstractValidator<CreateQuizQuestionRequestDto>
{
  public CreateQuizQuestionRequestDtoValidator()
  {
    RuleFor(x => x.QuizId)
      .NotEmpty().WithMessage("QuizId is required.");

    RuleFor(x => x.QuestionText)
      .NotEmpty().WithMessage("Question text is required.")
      .MaximumLength(2000).WithMessage("Question text must not exceed 2,000 characters.");

    RuleFor(x => x.QuestionType)
      .IsInEnum().WithMessage("A valid question type is required.");

    RuleFor(x => x.Marks)
      .GreaterThan(0).WithMessage("Marks must be greater than 0.");

    RuleFor(x => x.Order)
      .GreaterThanOrEqualTo(0)
      .When(x => x.Order.HasValue)
      .WithMessage("Order must not be negative.");

    // ── Options (only meaningful for choice-based question types) ──
    When(x => x.Options is not null && x.Options.Count > 0, () =>
    {
      RuleForEach(x => x.Options).ChildRules(option =>
      {
        option.RuleFor(o => o.OptionText)
          .NotEmpty().WithMessage("Option text is required.")
          .MaximumLength(500).WithMessage("Option text must not exceed 500 characters.");
      });

      RuleFor(x => x.Options)
        .Must(options => options!.Any(o => o.IsCorrect))
        .WithMessage("At least one option must be marked correct.");
    });
  }
}
