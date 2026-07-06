using learning_ms.Web.Application.Common.DTOs.Announcements;
namespace learning_ms.Web.Application.Validators.Announcements;
using FluentValidation;

public class CreateAnnouncementRequestDtoValidator : AbstractValidator<CreateAnnouncementRequestDto>
{
    private static readonly string[] AllowedAttachmentTypes =
        ["application/pdf", "image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedAttachmentExtensions =
        [".pdf", ".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxAttachmentSizeBytes = 10 * 1024 * 1024; // 10 MB per attachment
    private const int MaxAttachmentCount = 5;

    public CreateAnnouncementRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Announcement title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Announcement message is required.")
            .MaximumLength(5000).WithMessage("Message must not exceed 5,000 characters.");

        RuleFor(x => x.Summary)
            .MaximumLength(500).WithMessage("Summary must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Summary));

        RuleFor(x => x.Audience)
            .IsInEnum().WithMessage("Audience must be a valid value.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Priority must be a valid value.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Announcement type must be a valid value.");

        RuleFor(x => x.CourseId)
            .NotEqual(Guid.Empty).WithMessage("Course ID must be a valid identifier.")
            .When(x => x.CourseId.HasValue);

        RuleFor(x => x.ClassId)
            .NotEqual(Guid.Empty).WithMessage("Class ID must be a valid identifier.")
            .When(x => x.ClassId.HasValue);

        RuleFor(x => x.DepartmentId)
            .NotEqual(Guid.Empty).WithMessage("Department ID must be a valid identifier.")
            .When(x => x.DepartmentId.HasValue);

        RuleFor(x => x.ScheduledAt)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Scheduled date must be in the future.")
            .When(x => x.ScheduledAt.HasValue);

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(x => x.ScheduledAt ?? DateTime.UtcNow)
            .WithMessage("Expiry date must be after the scheduled/publish date.")
            .When(x => x.ExpiryDate.HasValue);

        // ── Attachments (optional, one or more files) ──
        When(x => x.Attachments is not null && x.Attachments.Count > 0, () =>
        {
            RuleFor(x => x.Attachments!.Count)
                .LessThanOrEqualTo(MaxAttachmentCount)
                .WithMessage($"You can upload a maximum of {MaxAttachmentCount} attachments.");

            RuleForEach(x => x.Attachments).ChildRules(attachment =>
            {
                attachment.RuleFor(f => f.Length)
                    .GreaterThan(0).WithMessage("An uploaded attachment cannot be an empty file.")
                    .LessThanOrEqualTo(MaxAttachmentSizeBytes)
                    .WithMessage($"Each attachment must not exceed {MaxAttachmentSizeBytes / (1024 * 1024)}MB.");

                attachment.RuleFor(f => f.ContentType)
                    .Must(contentType => AllowedAttachmentTypes.Contains(contentType))
                    .WithMessage("Each attachment must be a PDF, JPEG, PNG, or WEBP file.");

                attachment.RuleFor(f => f.FileName)
                    .Must(name => AllowedAttachmentExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                    .WithMessage("One or more attachment file extensions are not allowed.");
            });
        });
    }
}
