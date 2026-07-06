using learning_ms.Web.Application.Common.DTOs.Course;
namespace learning_ms.Web.Application.Validators.Course;
using FluentValidation;
public class CreateCourseRequestDtoValidator : AbstractValidator<CreateCourseRequestDto>
{
    private static readonly string[] AllowedImageTypes = ["image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5 MB

    private static readonly string[] AllowedLevels = ["Beginner", "Intermediate", "Advanced"];

    public CreateCourseRequestDtoValidator()
    {
        RuleFor(x => x.CourseCode)
            .NotEmpty().WithMessage("Course code is required.")
            .MaximumLength(20).WithMessage("Course code must not exceed 20 characters.");

        RuleFor(x => x.CourseTitle)
            .NotEmpty().WithMessage("Course title is required.")
            .MaximumLength(200).WithMessage("Course title must not exceed 200 characters.");

        RuleFor(x => x.ShortName)
            .MaximumLength(50).WithMessage("Short name must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.ShortName));

        RuleFor(x => x.Description)
            .MaximumLength(3000).WithMessage("Description must not exceed 3,000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.CreditHours)
            .GreaterThan(0).WithMessage("Credit hours must be greater than 0.");

        RuleFor(x => x.TotalLessons)
            .GreaterThan(0).WithMessage("Total lessons must be greater than 0.");

        RuleFor(x => x.DurationInWeeks)
            .GreaterThan(0).WithMessage("Duration in weeks must be greater than 0.");

        RuleFor(x => x.AcademicYear)
            .NotEmpty().WithMessage("Academic year is required.")
            .Matches(@"^\d{4}-\d{4}$")
            .WithMessage("Academic year must be in the format YYYY-YYYY, e.g. 2026-2027.");

        RuleFor(x => x.Semester)
            .NotEmpty().WithMessage("Semester is required.");

        RuleFor(x => x.Level)
            .NotEmpty().WithMessage("Course level is required.")
            .Must(level => AllowedLevels.Contains(level))
            .WithMessage($"Level must be one of: {string.Join(", ", AllowedLevels)}.");

        RuleFor(x => x.MinimumStudents)
            .GreaterThan(0).WithMessage("Minimum students must be greater than 0.");

        RuleFor(x => x.MaximumStudents)
            .GreaterThanOrEqualTo(x => x.MinimumStudents)
            .WithMessage("Maximum students must be greater than or equal to minimum students.");

        RuleFor(x => x.CourseFee)
            .GreaterThanOrEqualTo(0).WithMessage("Course fee cannot be negative.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after the start date.");

        // ── Course image (optional, single file) ──
        When(x => x.CourseImage is not null, () =>
        {
            RuleFor(x => x.CourseImage!.Length)
                .GreaterThan(0).WithMessage("Course image cannot be an empty file.")
                .LessThanOrEqualTo(MaxImageSizeBytes)
                .WithMessage($"Course image must not exceed {MaxImageSizeBytes / (1024 * 1024)}MB.");

            RuleFor(x => x.CourseImage!.ContentType)
                .Must(contentType => AllowedImageTypes.Contains(contentType))
                .WithMessage("Course image must be a JPEG, PNG, or WEBP file.");

            RuleFor(x => x.CourseImage!.FileName)
                .Must(name => AllowedImageExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                .WithMessage("Course image file extension is not allowed.");
        });
    }
}
