using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Application.Common.DTOs.Admissions.CreateAdmissionResponseDTO;
public record CreateAdmissionResponseDto
{
  public required Guid Id { get; init; }
  public required string ApplicationNumber { get; init; }
  public required DateOnly ApplicationDate { get; init; }
  public required string Status { get; init; }

  public required string FirstName { get; init; }
  public string MiddleName { get; init; } = string.Empty;
  public required string LastName { get; init; }
  public required string Email { get; init; }
  public required string PhoneNumber { get; init; }
  public required DateOnly DateOfBirth { get; init; }
  public required GenderType Gender { get; init; }
  public required string Nationality { get; init; }
  public required string PlaceOfBirth { get; init; }
  public string ProfilePictureUrl { get; init; } = string.Empty;
  public required string Address { get; init; }
  public required string City { get; init; }
  public string State { get; init; } = string.Empty;
  public string PostalCode { get; init; } = string.Empty;
  public required string Country { get; init; }
  public string PreviousSchool { get; init; } = string.Empty;
  public string PreviousClass { get; init; } = string.Empty;
  public decimal? PreviousGradeAverage { get; init; }
  public required string ApplyingForClass { get; init; }
  public required string AcademicYear { get; init; }
  public string FatherFullName { get; init; } = string.Empty;
  public string FatherPhoneNumber { get; init; } = string.Empty;
  public string FatherEmail { get; init; } = string.Empty;
  public string FatherOccupation { get; init; } = string.Empty;
  public string MotherFullName { get; init; } = string.Empty;
  public string MotherPhoneNumber { get; init; } = string.Empty;
  public string MotherEmail { get; init; } = string.Empty;
  public string MotherOccupation { get; init; } = string.Empty;
  public string? GuardianFullName { get; init; }
  public string? GuardianRelationship { get; init; }
  public string? GuardianPhoneNumber { get; init; }
  public string? GuardianEmail { get; init; }
  public required string EmergencyContactName { get; init; }
  public required string EmergencyContactPhoneNumber { get; init; }
  public required string EmergencyContactRelationship { get; init; }
  public string BloodGroup { get; init; } = string.Empty;
  public List<string>? Allergies { get; init; } = [];
  public List<string>? MedicalConditions { get; init; } = [];
  public List<string>? Disabilities { get; init; } = [];
  public List<string> UploadAdmissionDocuments { get; init; } = [];
  public required bool TermsAndConditionsAccepted { get; init; }
}
