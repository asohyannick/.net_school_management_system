using learning_ms.Web.Application.Common.DTOs.Assignment;
namespace learning_ms.Web.Application.Validators.Assignment;
using FluentValidation;

public class CreateAssignmentRequestDtoValidator : AbstractValidator<CreateAssignmentRequestDto>
{
    private static readonly string[] AllowedAttachmentTypes =
        ["application/pdf", "image/jpeg", "image/png",
         "application/msword",
         "application/vnd.openxmlformats-officedocument.wordprocessingml.document"];
    private static readonly string[] AllowedAttachmentExtensions =
        [".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx"];
    private const long MaxAttachmentSizeBytes = 20 * 1024 * 1024; // 20 MB

    public CreateAssignmentRequestDtoValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEqual(Guid.Empty).WithMessage("A valid course must be selected.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Assignment title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(3000).WithMessage("Description must not exceed 3,000 characters.");

        RuleFor(x => x.Instructions)
            .MaximumLength(5000).WithMessage("Instructions must not exceed 5,000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Instructions));

        RuleFor(x => x.TotalMarks)
            .GreaterThan(0).WithMessage("Total marks must be greater than 0.");

        RuleFor(x => x.PassingMarks)
            .GreaterThan(0).WithMessage("Passing marks must be greater than 0.")
            .LessThanOrEqualTo(x => x.TotalMarks)
            .WithMessage("Passing marks cannot exceed total marks.");

        RuleFor(x => x.AllowedAttempts)
            .GreaterThan(0).WithMessage("Allowed attempts must be at least 1.")
            .LessThanOrEqualTo(10).WithMessage("Allowed attempts must not exceed 10.");

        RuleFor(x => x.EstimatedCompletionTimeInMinutes)
            .GreaterThan(0).WithMessage("Estimated completion time must be greater than 0 minutes.");

        RuleFor(x => x.AvailableFrom)
            .NotEmpty().WithMessage("Available-from date is required.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Due date is required.")
            .GreaterThan(x => x.AvailableFrom)
            .WithMessage("Due date must be after the available-from date.");

        RuleFor(x => x.CloseDate)
            .GreaterThanOrEqualTo(x => x.DueDate)
            .WithMessage("Close date must be on or after the due date.")
            .When(x => x.CloseDate.HasValue);

        // ── Attachment (optional, single file) ──
        When(x => x.Attachment is not null, () =>
        {
            RuleFor(x => x.Attachment!.Length)
                .GreaterThan(0).WithMessage("Attachment cannot be an empty file.")
                .LessThanOrEqualTo(MaxAttachmentSizeBytes)
                .WithMessage($"Attachment must not exceed {MaxAttachmentSizeBytes / (1024 * 1024)}MB.");

            RuleFor(x => x.Attachment!.ContentType)
                .Must(contentType => AllowedAttachmentTypes.Contains(contentType))
                .WithMessage("Attachment must be a PDF, Word document, JPEG, or PNG file.");

            RuleFor(x => x.Attachment!.FileName)
                .Must(name => AllowedAttachmentExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                .WithMessage("Attachment file extension is not allowed.");
        });
    }
}
