using learning_ms.Web.Application.Common.DTOs.Grade;
namespace learning_ms.Web.Application.Validators.Grade;
using FluentValidation;

public class CreateGradeRequestDtoValidator : AbstractValidator<CreateGradeRequestDto>
{
    public CreateGradeRequestDtoValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("Course ID is required.");

        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("Subject ID is required.");

        RuleFor(x => x.TutorId)
            .NotEmpty().WithMessage("Tutor ID is required.");

        RuleFor(x => x.ExamId)
            .NotEqual(Guid.Empty).WithMessage("Exam ID must be a valid identifier.")
            .When(x => x.ExamId.HasValue);

        RuleFor(x => x.Score)
            .GreaterThanOrEqualTo(0).WithMessage("Score cannot be negative.")
            .LessThanOrEqualTo(x => x.TotalMarks)
            .WithMessage("Score cannot exceed total marks.");

        RuleFor(x => x.TotalMarks)
            .GreaterThan(0).WithMessage("Total marks must be greater than 0.");

        RuleFor(x => x.ExamScore)
            .GreaterThanOrEqualTo(0).WithMessage("Exam score cannot be negative.")
            .When(x => x.ExamScore.HasValue);

        RuleFor(x => x.AssignmentScore)
            .GreaterThanOrEqualTo(0).WithMessage("Assignment score cannot be negative.")
            .When(x => x.AssignmentScore.HasValue);

        RuleFor(x => x.QuizScore)
            .GreaterThanOrEqualTo(0).WithMessage("Quiz score cannot be negative.")
            .When(x => x.QuizScore.HasValue);

        RuleFor(x => x.AttendanceScore)
            .GreaterThanOrEqualTo(0).WithMessage("Attendance score cannot be negative.")
            .When(x => x.AttendanceScore.HasValue);

        RuleFor(x => x.Remarks)
            .MaximumLength(2000).WithMessage("Remarks must not exceed 2,000 characters.");

        RuleFor(x => x.TutorComment)
            .MaximumLength(2000).WithMessage("Tutor comment must not exceed 2,000 characters.");

        RuleFor(x => x.StrengthAreas)
            .MaximumLength(1000).WithMessage("Strength areas must not exceed 1,000 characters.");

        RuleFor(x => x.WeakAreas)
            .MaximumLength(1000).WithMessage("Weak areas must not exceed 1,000 characters.");

        RuleFor(x => x.SemesterId)
            .NotEqual(Guid.Empty).WithMessage("Semester ID must be a valid identifier.")
            .When(x => x.SemesterId.HasValue);

        RuleFor(x => x.AcademicYearId)
            .NotEqual(Guid.Empty).WithMessage("Academic Year ID must be a valid identifier.")
            .When(x => x.AcademicYearId.HasValue);
    }
}
