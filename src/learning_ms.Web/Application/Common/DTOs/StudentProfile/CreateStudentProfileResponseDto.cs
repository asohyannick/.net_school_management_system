using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Application.Common.DTOs.StudentProfile;

public record CreateStudentProfileResponseDto
{
  public required Guid Id { get; init; }

  public required string AdmissionNumber { get; init; }
  public required string FirstName { get; init; }
  public string? MiddleName { get; init; }
  public required string LastName { get; init; }
  public string? Email { get; init; }
  public string? PhoneNumber { get; init; }

  public List<string> ProfilePictureUrl { get; init; } = [];
  public string? Description { get; init; }

  public required DateOnly DateOfBirth { get; init; }
  public required int Age { get; init; }

  public required GenderType Gender { get; init; }
  public required string Nationality { get; init; }
  public string? PlaceOfBirth { get; init; }

  public List<string> Hobbies { get; init; } = [];

  public string? BloodGroup { get; init; }
  public string? Religion { get; init; }

  public required string CurrentClass { get; init; }
  public string? Section { get; init; }
  public required string AcademicYear { get; init; }

  public required DateOnly AdmissionDate { get; init; }

  public string? FatherFullName { get; init; }
  public string? FatherPhoneNumber { get; init; }
  public string? FatherOccupation { get; init; }

  public string? MotherFullName { get; init; }
  public string? MotherPhoneNumber { get; init; }
  public string? MotherOccupation { get; init; }

  public string? GuardianFullName { get; init; }
  public string? GuardianRelationship { get; init; }
  public string? GuardianPhoneNumber { get; init; }
  public string? GuardianEmail { get; init; }

  public required string EmergencyContactName { get; init; }
  public required string EmergencyContactPhoneNumber { get; init; }
  public required string EmergencyContactRelationship { get; init; }

  public required string Address { get; init; }
  public required string City { get; init; }
  public string? State { get; init; }
  public string? PostalCode { get; init; }
  public required string Country { get; init; }

  public string? Allergies { get; init; }
  public string? MedicalConditions { get; init; }
  public string? Medications { get; init; }

  public required bool IsActive { get; init; }
  public required bool IsGraduated { get; init; }

  public required DateTime CreatedAt { get; init; }
}
