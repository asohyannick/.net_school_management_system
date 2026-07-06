using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.Quiz;
namespace learning_ms.Web.Application.Validators.Quiz;
public class CreateQuizRequestDtoValidator : AbstractValidator<CreateQuizRequestDto>
{
  public CreateQuizRequestDtoValidator()
  {
    RuleFor(x => x.Title)
      .NotEmpty().WithMessage("Title is required.")
      .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

    RuleFor(x => x.Description)
      .MaximumLength(2000).WithMessage("Description must not exceed 2,000 characters.");

    RuleFor(x => x.Instructions)
      .MaximumLength(2000).WithMessage("Instructions must not exceed 2,000 characters.");

    RuleFor(x => x.CourseId)
      .NotEmpty().WithMessage("CourseId is required.");

    RuleFor(x => x.SubjectId)
      .NotEmpty().WithMessage("SubjectId is required.");

    RuleFor(x => x.TeacherId)
      .NotEmpty().WithMessage("TeacherId is required.");

    RuleFor(x => x.TotalMarks)
      .GreaterThan(0).WithMessage("TotalMarks must be greater than 0.");

    RuleFor(x => x.PassingMarks)
      .GreaterThan(0).WithMessage("PassingMarks must be greater than 0.")
      .LessThanOrEqualTo(x => x.TotalMarks)
      .WithMessage("PassingMarks cannot exceed TotalMarks.");

    RuleFor(x => x.DurationInMinutes)
      .GreaterThan(0).WithMessage("DurationInMinutes must be greater than 0.");

    RuleFor(x => x.MaximumAttempts)
      .GreaterThan(0)
      .When(x => x.MaximumAttempts.HasValue)
      .WithMessage("MaximumAttempts must be greater than 0.");

    RuleFor(x => x.StartDate)
      .NotEmpty().WithMessage("StartDate is required.");

    RuleFor(x => x.EndDate)
      .NotEmpty().WithMessage("EndDate is required.")
      .GreaterThan(x => x.StartDate)
      .WithMessage("EndDate must be after StartDate.");
  }
}
