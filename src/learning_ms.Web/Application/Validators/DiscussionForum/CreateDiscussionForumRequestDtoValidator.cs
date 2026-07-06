using learning_ms.Web.Application.Common.DTOs.DiscussionForum;

namespace learning_ms.Web.Application.Validators.DiscussionForum;
using FluentValidation;
public class CreateDiscussionForumRequestDtoValidator : AbstractValidator<CreateDiscussionForumRequestDto>
{
    private static readonly string[] AllowedAttachmentTypes =
        ["application/pdf", "image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedAttachmentExtensions =
        [".pdf", ".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxAttachmentSizeBytes = 10 * 1024 * 1024; // 10 MB per attachment
    private const int MaxAttachmentCount = 5;

    public CreateDiscussionForumRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Discussion title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Discussion content is required.")
            .MaximumLength(10000).WithMessage("Content must not exceed 10,000 characters.");

        RuleFor(x => x.CourseId)
            .NotEqual(Guid.Empty).WithMessage("Course ID must be a valid identifier.")
            .When(x => x.CourseId.HasValue);

        RuleFor(x => x.ClassId)
            .NotEqual(Guid.Empty).WithMessage("Class ID must be a valid identifier.")
            .When(x => x.ClassId.HasValue);

        RuleFor(x => x.SubjectId)
            .NotEqual(Guid.Empty).WithMessage("Subject ID must be a valid identifier.")
            .When(x => x.SubjectId.HasValue);

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
