using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.Accommodations;
namespace learning_ms.Web.Application.Validators.Accommodation;
public class CreateAccommodationRequestDtoValidator : AbstractValidator<CreateAccommodationRequestDto>
{
    private static readonly string[] AllowedImageTypes = ["image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxImageSizeBytes = 10 * 1024 * 1024; // 10 MB per image
    private const int MaxImageCount = 15;

    private static readonly string[] AllowedCurrencies = ["USD", "XAF", "EUR", "GBP"];

    public CreateAccommodationRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Accommodation name is required.")
            .MaximumLength(150).WithMessage("Accommodation name must not exceed 150 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Accommodation code is required.")
            .MaximumLength(20).WithMessage("Accommodation code must not exceed 20 characters.");

        RuleFor(x => x.Building)
            .NotEmpty().WithMessage("Building name is required.");

        RuleFor(x => x.RoomNumber)
            .NotEmpty().WithMessage("Room number is required.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be at least 1 bed.")
            .LessThanOrEqualTo(50).WithMessage("Capacity must not exceed 50 beds per room.");

        RuleFor(x => x.Fee)
            .GreaterThanOrEqualTo(0).WithMessage("Fee cannot be negative.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Must(currency => AllowedCurrencies.Contains(currency.ToUpperInvariant()))
            .WithMessage($"Currency must be one of: {string.Join(", ", AllowedCurrencies)}.");

        RuleFor(x => x.WardenId)
            .NotEqual(Guid.Empty).WithMessage("Warden ID must be a valid identifier.")
            .When(x => x.WardenId.HasValue);

        // ── Hostel images (optional, one or more files) ──
        When(x => x.HostelImages is not null && x.HostelImages.Count > 0, () =>
        {
            RuleFor(x => x.HostelImages!.Count)
                .LessThanOrEqualTo(MaxImageCount)
                .WithMessage($"You can upload a maximum of {MaxImageCount} images.");

            RuleForEach(x => x.HostelImages).ChildRules(image =>
            {
                image.RuleFor(f => f.Length)
                    .GreaterThan(0).WithMessage("An uploaded image cannot be an empty file.")
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
