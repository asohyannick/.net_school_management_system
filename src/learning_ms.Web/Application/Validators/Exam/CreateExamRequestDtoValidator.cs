using learning_ms.Web.Application.Common.DTOs.Exam;
namespace learning_ms.Web.Application.Validators.Exam;
using FluentValidation;
public class CreateExamRequestDtoValidator : AbstractValidator<CreateExamRequestDto>
{
    private static readonly string[] AllowedDocumentTypes =
        ["application/pdf", "image/jpeg", "image/png", "image/webp",
         "application/msword",
         "application/vnd.openxmlformats-officedocument.wordprocessingml.document"];
    private static readonly string[] AllowedDocumentExtensions =
        [".pdf", ".jpg", ".jpeg", ".png", ".webp", ".doc", ".docx"];
    private const long MaxDocumentSizeBytes = 20 * 1024 * 1024; // 20 MB per document
    private const int MaxDocumentCount = 10;

    public CreateExamRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Exam title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(5000).WithMessage("Description must not exceed 5,000 characters.");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("Course ID is required.");

        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("Subject ID is required.");

        RuleFor(x => x.ClassId)
            .NotEmpty().WithMessage("Class ID is required.");

        RuleFor(x => x.TutorId)
            .NotEmpty().WithMessage("Tutor ID is required.");

        RuleFor(x => x.Type)
            .NotNull().WithMessage("Exam type is required.")
            .IsInEnum().WithMessage("Exam type is invalid.");

        RuleFor(x => x.Mode)
            .NotNull().WithMessage("Exam mode is required.")
            .IsInEnum().WithMessage("Exam mode is invalid.");

        RuleFor(x => x.StartDate)
            .NotNull().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotNull().WithMessage("End date is required.")
            .GreaterThan(x => x.StartDate!.Value)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage("End date must be after the start date.");

        RuleFor(x => x.DurationInMinutes)
            .NotNull().WithMessage("Duration is required.")
            .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes.");

        RuleFor(x => x.RegistrationDeadline)
            .LessThanOrEqualTo(x => x.StartDate!.Value)
            .When(x => x.RegistrationDeadline.HasValue && x.StartDate.HasValue)
            .WithMessage("Registration deadline must be on or before the start date.");

        RuleFor(x => x.TotalMarks)
            .NotNull().WithMessage("Total marks is required.")
            .GreaterThan(0).WithMessage("Total marks must be greater than 0.");

        RuleFor(x => x.PassingMarks)
            .NotNull().WithMessage("Passing marks is required.")
            .GreaterThan(0).WithMessage("Passing marks must be greater than 0.")
            .LessThanOrEqualTo(x => x.TotalMarks!.Value)
            .When(x => x.PassingMarks.HasValue && x.TotalMarks.HasValue)
            .WithMessage("Passing marks cannot exceed total marks.");

        RuleFor(x => x.MaxAttempts)
            .GreaterThanOrEqualTo(1)
            .When(x => x.MaxAttempts.HasValue)
            .WithMessage("Max attempts must be at least 1.");

        RuleFor(x => x.Instructions)
            .MaximumLength(5000).WithMessage("Instructions must not exceed 5,000 characters.");

        RuleFor(x => x.Rules)
            .MaximumLength(5000).WithMessage("Rules must not exceed 5,000 characters.");

        // ── Exam documents (optional, one or more files) ──
        When(x => x.ExamDocuments is not null && x.ExamDocuments.Count > 0, () =>
        {
            RuleFor(x => x.ExamDocuments!.Count)
                .LessThanOrEqualTo(MaxDocumentCount)
                .WithMessage($"You can upload a maximum of {MaxDocumentCount} exam documents.");

            RuleForEach(x => x.ExamDocuments).ChildRules(document =>
            {
                document.RuleFor(f => f.Length)
                    .GreaterThan(0).WithMessage("An uploaded exam document cannot be an empty file.")
                    .LessThanOrEqualTo(MaxDocumentSizeBytes)
                    .WithMessage($"Each document must not exceed {MaxDocumentSizeBytes / (1024 * 1024)}MB.");

                document.RuleFor(f => f.ContentType)
                    .Must(contentType => AllowedDocumentTypes.Contains(contentType))
                    .WithMessage("Each document must be a PDF, DOC, DOCX, JPEG, PNG, or WEBP file.");

                document.RuleFor(f => f.FileName)
                    .Must(name => AllowedDocumentExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                    .WithMessage("One or more exam document file extensions are not allowed.");
            });
        });
    }
}
