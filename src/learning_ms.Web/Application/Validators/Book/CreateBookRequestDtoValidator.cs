using learning_ms.Web.Application.Common.DTOs.Book;

namespace learning_ms.Web.Application.Validators.Book;
using FluentValidation;
public class CreateBookRequestDtoValidator : AbstractValidator<CreateBookRequestDto>
{
    private static readonly string[] AllowedImageTypes =
        ["image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedImageExtensions =
        [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxCoverImageSizeBytes = 5 * 1024 * 1024; // 5 MB

    public CreateBookRequestDtoValidator()
    {
        RuleFor(x => x.LibraryId)
            .NotEmpty().WithMessage("Library ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(300).WithMessage("Title must not exceed 300 characters.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author is required.")
            .MaximumLength(200).WithMessage("Author must not exceed 200 characters.");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^(?:\d{10}|\d{13}|[\d-]{13,17})$")
            .WithMessage("ISBN must be a valid 10 or 13 digit ISBN.");

        RuleFor(x => x.Publisher)
            .MaximumLength(200).WithMessage("Publisher must not exceed 200 characters.");

        RuleFor(x => x.PublicationYear)
            .InclusiveBetween(1000, DateTime.UtcNow.Year)
            .When(x => x.PublicationYear.HasValue)
            .WithMessage($"Publication year must be between 1000 and {DateTime.UtcNow.Year}.");

        RuleFor(x => x.Edition)
            .MaximumLength(100).WithMessage("Edition must not exceed 100 characters.");

        RuleFor(x => x.Category)
            .MaximumLength(100).WithMessage("Category must not exceed 100 characters.");

        RuleFor(x => x.Subject)
            .MaximumLength(100).WithMessage("Subject must not exceed 100 characters.");

        RuleFor(x => x.Language)
            .MaximumLength(50).WithMessage("Language must not exceed 50 characters.");

        RuleFor(x => x.ShelfLocation)
            .MaximumLength(100).WithMessage("Shelf location must not exceed 100 characters.");

        RuleFor(x => x.TotalCopies)
            .GreaterThan(0).WithMessage("Total copies must be greater than 0.");

        RuleFor(x => x.AvailableCopies)
            .GreaterThanOrEqualTo(0).WithMessage("Available copies cannot be negative.")
            .LessThanOrEqualTo(x => x.TotalCopies)
            .When(x => x.AvailableCopies.HasValue)
            .WithMessage("Available copies cannot exceed total copies.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.")
            .When(x => x.Price.HasValue);

        RuleFor(x => x.Description)
            .MaximumLength(3000).WithMessage("Description must not exceed 3,000 characters.");

        // ── Cover image (optional, single file) ──
        When(x => x.CoverImage is not null, () =>
        {
            RuleFor(x => x.CoverImage!.Length)
                .GreaterThan(0).WithMessage("Cover image cannot be an empty file.")
                .LessThanOrEqualTo(MaxCoverImageSizeBytes)
                .WithMessage($"Cover image must not exceed {MaxCoverImageSizeBytes / (1024 * 1024)}MB.");

            RuleFor(x => x.CoverImage!.ContentType)
                .Must(contentType => AllowedImageTypes.Contains(contentType))
                .WithMessage("Cover image must be a JPEG, PNG, or WEBP file.");

            RuleFor(x => x.CoverImage!.FileName)
                .Must(name => AllowedImageExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                .WithMessage("Cover image file extension is not allowed.");
        });
    }
}
