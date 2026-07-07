using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Domain.Entities.StudentProfile;
public class StudentProfile
{
  public Guid Id { get; set; }

  public string AdmissionNumber { get; set; } = default!;
  public string FirstName { get; set; } = default!;
  public string MiddleName { get; set; } = string.Empty;
  public string LastName { get; set; } = default!;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;

  public List<string>? ProfilePictureUrl { get; set; } = [];
  public string? Description { get; set; }

  public DateOnly DateOfBirth { get; set; }
  public int Age { get; set; }

  public GenderType Gender { get; set; } = default!;
  public string Nationality { get; set; } = default!;
  public string PlaceOfBirth { get; set; } = string.Empty;

  public List<string> Hobbies { get; set; } = new();

  public string? BloodGroup { get; set; }
  public string? Religion { get; set; }

  public string CurrentClass { get; set; } = default!;
  public string Section { get; set; } = string.Empty;
  public string AcademicYear { get; set; } = default!;

  public DateOnly AdmissionDate { get; set; }

  public string FatherFullName { get; set; } = string.Empty;
  public string FatherPhoneNumber { get; set; } = string.Empty;
  public string FatherOccupation { get; set; } = string.Empty;

  public string MotherFullName { get; set; } = string.Empty;
  public string MotherPhoneNumber { get; set; } = string.Empty;
  public string MotherOccupation { get; set; } = string.Empty;

  public string? GuardianFullName { get; set; }
  public string? GuardianRelationship { get; set; }
  public string? GuardianPhoneNumber { get; set; }
  public string? GuardianEmail { get; set; }

  public string EmergencyContactName { get; set; } = default!;
  public string EmergencyContactPhoneNumber { get; set; } = default!;
  public string EmergencyContactRelationship { get; set; } = default!;

  public string Address { get; set; } = default!;
  public string City { get; set; } = default!;
  public string State { get; set; } = string.Empty;
  public string PostalCode { get; set; } = string.Empty;
  public string Country { get; set; } = default!;

  public string? Allergies { get; set; }
  public string? MedicalConditions { get; set; }
  public string? Medications { get; set; }

  public bool IsActive { get; set; } = true;
  public bool IsGraduated { get; set; } = false;

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }
  public Guid? UpdatedBy { get; set; }
}
