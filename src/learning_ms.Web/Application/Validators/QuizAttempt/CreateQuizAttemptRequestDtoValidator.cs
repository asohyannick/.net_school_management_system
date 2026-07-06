using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.QuizAttempt;

namespace learning_ms.Web.Application.Validators.QuizAttempt;

public class CreateQuizAttemptRequestDtoValidator : AbstractValidator<CreateQuizAttemptRequestDto>
{
  public CreateQuizAttemptRequestDtoValidator()
  {
    RuleFor(x => x.QuizId)
      .NotEmpty().WithMessage("QuizId is required.");

    RuleFor(x => x.StudentId)
      .NotEmpty().WithMessage("StudentId is required.");
  }
}
