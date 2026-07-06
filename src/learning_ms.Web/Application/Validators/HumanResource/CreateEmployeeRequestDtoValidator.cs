using learning_ms.Web.Application.Common.DTOs.HumanResource;

namespace learning_ms.Web.Application.Validators.HumanResource;
using FluentValidation;

public class CreateEmployeeRequestDtoValidator : AbstractValidator<CreateEmployeeRequestDto>
{
    private static readonly string[] AllowedImageTypes =
        ["image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedImageExtensions =
        [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxProfilePictureSizeBytes = 5 * 1024 * 1024; // 5 MB

    private static readonly string[] AllowedCertTypes =
        ["application/pdf", "image/jpeg", "image/png", "image/webp",
         "application/msword",
         "application/vnd.openxmlformats-officedocument.wordprocessingml.document"];
    private static readonly string[] AllowedCertExtensions =
        [".pdf", ".jpg", ".jpeg", ".png", ".webp", ".doc", ".docx"];
    private const long MaxCertSizeBytes = 10 * 1024 * 1024; // 10 MB per file
    private const int MaxCertCount = 10;

    public CreateEmployeeRequestDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.MiddleName)
            .MaximumLength(100).WithMessage("Middle name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Phone number format is invalid.");

        RuleFor(x => x.Biography)
            .MaximumLength(3000).WithMessage("Biography must not exceed 3,000 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(dob => dob <= DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)))
            .WithMessage("Employee must be at least 18 years old.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Gender is invalid.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(300).WithMessage("Address must not exceed 300 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");

        RuleFor(x => x.HireDate)
            .NotEmpty().WithMessage("Hire date is required.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)))
            .WithMessage("Hire date cannot be in the future.");

        RuleFor(x => x.TerminationDate)
            .GreaterThan(x => x.HireDate)
            .When(x => x.TerminationDate.HasValue)
            .WithMessage("Termination date must be after the hire date.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than 0.");

        RuleFor(x => x.HighestQualification)
            .NotEmpty().WithMessage("Highest qualification is required.")
            .MaximumLength(200).WithMessage("Highest qualification must not exceed 200 characters.");

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .When(x => x.YearsOfExperience.HasValue)
            .WithMessage("Years of experience cannot be negative.");

        RuleFor(x => x.EmergencyContactName)
            .NotEmpty().WithMessage("Emergency contact name is required.")
            .MaximumLength(150).WithMessage("Emergency contact name must not exceed 150 characters.");

        RuleFor(x => x.EmergencyContactPhoneNumber)
            .NotEmpty().WithMessage("Emergency contact phone number is required.")
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Emergency contact phone number format is invalid.");

        RuleFor(x => x.EmergencyContactRelationship)
            .NotEmpty().WithMessage("Emergency contact relationship is required.")
            .MaximumLength(100).WithMessage("Emergency contact relationship must not exceed 100 characters.");

        RuleFor(x => x.LinkedInProfile)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkedInProfile))
            .WithMessage("LinkedIn profile must be a valid URL.");

        // ── Profile picture (optional, single file) ──
        When(x => x.ProfilePicture is not null, () =>
        {
            RuleFor(x => x.ProfilePicture!.Length)
                .GreaterThan(0).WithMessage("Profile picture cannot be an empty file.")
                .LessThanOrEqualTo(MaxProfilePictureSizeBytes)
                .WithMessage($"Profile picture must not exceed {MaxProfilePictureSizeBytes / (1024 * 1024)}MB.");

            RuleFor(x => x.ProfilePicture!.ContentType)
                .Must(contentType => AllowedImageTypes.Contains(contentType))
                .WithMessage("Profile picture must be a JPEG, PNG, or WEBP file.");

            RuleFor(x => x.ProfilePicture!.FileName)
                .Must(name => AllowedImageExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                .WithMessage("Profile picture file extension is not allowed.");
        });

        // ── Certification documents (optional, one or more files) ──
        When(x => x.CertificationDocuments is not null && x.CertificationDocuments.Count > 0, () =>
        {
            RuleFor(x => x.CertificationDocuments!.Count)
                .LessThanOrEqualTo(MaxCertCount)
                .WithMessage($"You can upload a maximum of {MaxCertCount} certification documents.");

            RuleForEach(x => x.CertificationDocuments).ChildRules(document =>
            {
                document.RuleFor(f => f.Length)
                    .GreaterThan(0).WithMessage("An uploaded certification document cannot be an empty file.")
                    .LessThanOrEqualTo(MaxCertSizeBytes)
                    .WithMessage($"Each certification document must not exceed {MaxCertSizeBytes / (1024 * 1024)}MB.");

                document.RuleFor(f => f.ContentType)
                    .Must(contentType => AllowedCertTypes.Contains(contentType))
                    .WithMessage("Each certification document must be a PDF, DOC, DOCX, JPEG, PNG, or WEBP file.");

                document.RuleFor(f => f.FileName)
                    .Must(name => AllowedCertExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                    .WithMessage("One or more certification document file extensions are not allowed.");
            });
        });
    }
}
