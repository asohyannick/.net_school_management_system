using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.StudentProfile;
namespace learning_ms.Web.Application.Validators.StudentProfile;

public class CreateStudentProfileRequestDtoValidator : AbstractValidator<CreateStudentProfileRequestDto>
{
    private static readonly string[] AllowedImageTypes =
        ["image/jpeg", "image/png", "image/webp"];
    private static readonly string[] AllowedImageExtensions =
        [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5 MB per image
    private const int MaxImageCount = 5;

    public CreateStudentProfileRequestDtoValidator()
    {
        RuleFor(x => x.AdmissionNumber)
            .NotEmpty().WithMessage("Admission number is required.")
            .MaximumLength(50).WithMessage("Admission number must not exceed 50 characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email address is required.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Phone number format is invalid.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(x => DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("A valid gender is required.");

        RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage("Nationality is required.")
            .MaximumLength(100).WithMessage("Nationality must not exceed 100 characters.");

        RuleFor(x => x.CurrentClass)
            .NotEmpty().WithMessage("Current class is required.")
            .MaximumLength(50).WithMessage("Current class must not exceed 50 characters.");

        RuleFor(x => x.AcademicYear)
            .NotEmpty().WithMessage("Academic year is required.")
            .MaximumLength(20).WithMessage("Academic year must not exceed 20 characters.");

        RuleFor(x => x.AdmissionDate)
            .NotEmpty().WithMessage("Admission date is required.");

        RuleFor(x => x.EmergencyContactName)
            .NotEmpty().WithMessage("Emergency contact name is required.")
            .MaximumLength(150).WithMessage("Emergency contact name must not exceed 150 characters.");

        RuleFor(x => x.EmergencyContactPhoneNumber)
            .NotEmpty().WithMessage("Emergency contact phone number is required.")
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Emergency contact phone number format is invalid.");

        RuleFor(x => x.EmergencyContactRelationship)
            .NotEmpty().WithMessage("Emergency contact relationship is required.")
            .MaximumLength(50).WithMessage("Emergency contact relationship must not exceed 50 characters.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(300).WithMessage("Address must not exceed 300 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");

        RuleFor(x => x.GuardianEmail)
            .EmailAddress().WithMessage("A valid guardian email is required.")
            .When(x => !string.IsNullOrWhiteSpace(x.GuardianEmail));

        RuleFor(x => x.GuardianPhoneNumber)
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Guardian phone number format is invalid.")
            .When(x => !string.IsNullOrWhiteSpace(x.GuardianPhoneNumber));

        RuleFor(x => x.FatherPhoneNumber)
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Father's phone number format is invalid.")
            .When(x => !string.IsNullOrWhiteSpace(x.FatherPhoneNumber));

        RuleFor(x => x.MotherPhoneNumber)
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Mother's phone number format is invalid.")
            .When(x => !string.IsNullOrWhiteSpace(x.MotherPhoneNumber));

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
