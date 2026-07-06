using learning_ms.Web.Application.Common.DTOs.Library;
namespace learning_ms.Web.Application.Validators.Library;
using FluentValidation;
public class CreateLibraryRequestDtoValidator : AbstractValidator<CreateLibraryRequestDto>
{
    private static readonly string[] AllowedImageTypes =
        ["image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedImageExtensions =
        [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5 MB per image
    private const int MaxImageCount = 5;

    public CreateLibraryRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Library name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description must not exceed 2,000 characters.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(300).WithMessage("Location must not exceed 300 characters.");

        RuleFor(x => x.LibrarianName)
            .NotEmpty().WithMessage("Librarian name is required.")
            .MaximumLength(150).WithMessage("Librarian name must not exceed 150 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Phone number format is invalid.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0.");

        RuleFor(x => x.OpeningTime)
            .NotEmpty().WithMessage("Opening time is required.");

        RuleFor(x => x.ClosingTime)
            .NotEmpty().WithMessage("Closing time is required.")
            .GreaterThan(x => x.OpeningTime)
            .WithMessage("Closing time must be after opening time.");

        // ── Library images (optional, one or more files) ──
        When(x => x.LibraryImages is not null && x.LibraryImages.Count > 0, () =>
        {
            RuleFor(x => x.LibraryImages!.Count)
                .LessThanOrEqualTo(MaxImageCount)
                .WithMessage($"You can upload a maximum of {MaxImageCount} library images.");

            RuleForEach(x => x.LibraryImages).ChildRules(image =>
            {
                image.RuleFor(f => f.Length)
                    .GreaterThan(0).WithMessage("An uploaded library image cannot be an empty file.")
                    .LessThanOrEqualTo(MaxImageSizeBytes)
                    .WithMessage($"Each image must not exceed {MaxImageSizeBytes / (1024 * 1024)}MB.");

                image.RuleFor(f => f.ContentType)
                    .Must(contentType => AllowedImageTypes.Contains(contentType))
                    .WithMessage("Each image must be a JPEG, PNG, or WEBP file.");

                image.RuleFor(f => f.FileName)
                    .Must(name => AllowedImageExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                    .WithMessage("One or more image file extensions are not allowed.");
            });
        });
    }
}
