using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.TutorProfile;
namespace learning_ms.Web.Application.Validators.TutorProfile;

public class CreateTutorProfileRequestDtoValidator : AbstractValidator<CreateTutorProfileRequestDto>
{
    private static readonly string[] AllowedImageTypes =
        ["image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedImageExtensions =
        [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5 MB per image
    private const int MaxImageCount = 5;

    public CreateTutorProfileRequestDtoValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.")
            .MaximumLength(50).WithMessage("Employee ID must not exceed 50 characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("A valid gender is required.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(x => DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Phone number format is invalid.");

        RuleFor(x => x.AlternatePhoneNumber)
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Alternate phone number format is invalid.")
            .When(x => !string.IsNullOrWhiteSpace(x.AlternatePhoneNumber));

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(300).WithMessage("Address must not exceed 300 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Department is required.")
            .MaximumLength(150).WithMessage("Department must not exceed 150 characters.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required.")
            .MaximumLength(150).WithMessage("Position must not exceed 150 characters.");

        RuleFor(x => x.EmploymentDate)
            .NotEmpty().WithMessage("Employment date is required.");

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0).WithMessage("Salary cannot be negative.");

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .When(x => x.YearsOfExperience.HasValue)
            .WithMessage("Years of experience cannot be negative.");

        RuleFor(x => x.HighestQualification)
            .NotEmpty().WithMessage("Highest qualification is required.")
            .MaximumLength(200).WithMessage("Highest qualification must not exceed 200 characters.");

        RuleFor(x => x.EmergencyContactPhoneNumber)
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Emergency contact phone number format is invalid.")
            .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContactPhoneNumber));

        RuleFor(x => x.LinkedInProfile)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkedInProfile))
            .WithMessage("LinkedIn profile must be a valid URL.");

        RuleFor(x => x.PortfolioWebsite)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrWhiteSpace(x.PortfolioWebsite))
            .WithMessage("Portfolio website must be a valid URL.");

        // ── Profile pictures (optional, one or more files) ──
        When(x => x.ProfilePictureImages is not null && x.ProfilePictureImages.Count > 0, () =>
        {
            RuleFor(x => x.ProfilePictureImages!.Count)
                .LessThanOrEqualTo(MaxImageCount)
                .WithMessage($"You can upload a maximum of {MaxImageCount} profile images.");

            RuleForEach(x => x.ProfilePictureImages).ChildRules(image =>
            {
                image.RuleFor(f => f.Length)
                    .GreaterThan(0).WithMessage("An uploaded profile image cannot be an empty file.")
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
