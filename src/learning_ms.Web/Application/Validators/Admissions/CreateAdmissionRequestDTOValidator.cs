using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.Admissions.CreateAdmissionRequestDTO;
namespace learning_ms.Web.Application.Validators.Admissions;
public class CreateAdmissionRequestDtoValidator : AbstractValidator<CreateAdmissionRequestDto>
{
  private static readonly string[] AllowedImageTypes = ["image/jpeg", "image/png", "image/webp"];
  private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];
  private const long MaxProfilePictureSizeBytes = 10 * 1024 * 1024; // 10 MB per profile pic

  private static readonly string[] AllowedDocumentTypes =
  [
    "application/pdf",
    "image/jpeg",
    "image/png",
  ];
  private static readonly string[] AllowedDocumentExtensions = [".pdf", ".jpg", ".jpeg", ".png"];
  private const long MaxDocumentSizeBytes = 10 * 1024 * 1024; // 10 MB per document
  private const int MaxDocumentCount = 10;

  public CreateAdmissionRequestDtoValidator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .WithMessage("First name is required.")
      .MaximumLength(100)
      .WithMessage("First name must not exceed 100 characters.");

    RuleFor(x => x.LastName)
      .NotEmpty()
      .WithMessage("Last name is required.")
      .MaximumLength(100)
      .WithMessage("Last name must not exceed 100 characters.");

    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage("Email address is required.")
      .EmailAddress()
      .WithMessage("Email address is not in a valid format.");

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .WithMessage("Phone number is required.")
      .Matches(@"^\+?[0-9\s\-]{7,15}$")
      .WithMessage("Phone number format is invalid.");

    RuleFor(x => x.DateOfBirth)
      .NotEmpty()
      .WithMessage("Date of birth is required.")
      .LessThan(x => DateOnly.FromDateTime(DateTime.UtcNow))
      .WithMessage("Date of birth must be in the past.");

    RuleFor(x => x.Gender).IsInEnum().WithMessage("Gender must be a valid value.");

    RuleFor(x => x.Nationality).NotEmpty().WithMessage("Nationality is required.");

    RuleFor(x => x.PlaceOfBirth).NotEmpty().WithMessage("Place of birth is required.");

    RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");

    RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");

    RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");

    RuleFor(x => x.ApplyingForClass)
      .NotEmpty()
      .WithMessage("You must specify the class being applied for.");

    RuleFor(x => x.AcademicYear)
      .NotEmpty()
      .WithMessage("Academic year is required.")
      .Matches(@"^\d{4}-\d{4}$")
      .WithMessage("Academic year must be in the format YYYY-YYYY, e.g. 2026-2027.");

    RuleFor(x => x.EmergencyContactName)
      .NotEmpty()
      .WithMessage("Emergency contact name is required.");

    RuleFor(x => x.EmergencyContactPhoneNumber)
      .NotEmpty()
      .WithMessage("Emergency contact phone number is required.")
      .Matches(@"^\+?[0-9\s\-]{7,15}$")
      .WithMessage("Emergency contact phone number format is invalid.");

    RuleFor(x => x.EmergencyContactRelationship)
      .NotEmpty()
      .WithMessage("Emergency contact relationship is required.");

    RuleFor(x => x.FatherEmail)
      .EmailAddress()
      .WithMessage("Father's email address is not in a valid format.")
      .When(x => !string.IsNullOrEmpty(x.FatherEmail));

    RuleFor(x => x.MotherEmail)
      .EmailAddress()
      .WithMessage("Mother's email address is not in a valid format.")
      .When(x => !string.IsNullOrEmpty(x.MotherEmail));

    RuleFor(x => x.GuardianEmail)
      .EmailAddress()
      .WithMessage("Guardian's email address is not in a valid format.")
      .When(x => !string.IsNullOrEmpty(x.GuardianEmail));

    RuleFor(x => x.TermsAndConditionsAccepted)
      .Equal(true)
      .WithMessage("You must accept the terms and conditions to submit an application.");

    // ── Profile picture (optional single file) ──
    When(
      x => x.ProfilePicture is not null,
      () =>
      {
        RuleFor(x => x.ProfilePicture!.Length)
          .GreaterThan(0)
          .WithMessage("Profile picture cannot be an empty file.")
          .LessThanOrEqualTo(MaxProfilePictureSizeBytes)
          .WithMessage(
            $"Profile picture must not exceed {MaxProfilePictureSizeBytes / (1024 * 1024)}MB."
          );

        RuleFor(x => x.ProfilePicture!.ContentType)
          .Must(contentType => AllowedImageTypes.Contains(contentType))
          .WithMessage("Profile picture must be a JPEG, PNG, or WEBP image.");

        RuleFor(x => x.ProfilePicture!.FileName)
          .Must(name => AllowedImageExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
          .WithMessage("Profile picture file extension is not allowed.");
      }
    );

    // ── Admission documents (required, one or more files) ──
    RuleFor(x => x.UploadAdmissionDocuments)
      .NotEmpty()
      .WithMessage("At least one admission document must be uploaded.")
      .Must(files => files.Count <= MaxDocumentCount)
      .WithMessage($"You can upload a maximum of {MaxDocumentCount} documents.");

    RuleForEach(x => x.UploadAdmissionDocuments)
      .ChildRules(document =>
      {
        document
          .RuleFor(f => f.Length)
          .GreaterThan(0)
          .WithMessage("An uploaded document cannot be an empty file.")
          .LessThanOrEqualTo(MaxDocumentSizeBytes)
          .WithMessage($"Each document must not exceed {MaxDocumentSizeBytes / (1024 * 1024)}MB.");

        document
          .RuleFor(f => f.ContentType)
          .Must(contentType => AllowedDocumentTypes.Contains(contentType))
          .WithMessage("Each document must be a PDF, JPEG, or PNG file.");

        document
          .RuleFor(f => f.FileName)
          .Must(name =>
            AllowedDocumentExtensions.Contains(Path.GetExtension(name).ToLowerInvariant())
          )
          .WithMessage("One or more document file extensions are not allowed.");
      });
  }
}
