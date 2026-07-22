using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Application.Common.DTOs.StudentProfile;
using Microsoft.AspNetCore.Http;
public record CreateStudentProfileRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public bool? IsActive { get; init; }
  public bool? IsGraduated { get; init; }
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public Guid? CreatedBy { get; init; } = Guid.Empty;
  public Guid? UpdatedBy { get; init; } = Guid.Empty;
  public string AdmissionNumber { get; init; } = string.Empty;
  public string FirstName { get; init; } = string.Empty;
  public string MiddleName { get; init; } = string.Empty;
  public string LastName { get; init; } = string.Empty;
  public string? Email { get; init; } = string.Empty;
  public string? PhoneNumber { get; init; } = string.Empty;
  public string? Description { get; init; } = string.Empty;
  public DateOnly DateOfBirth { get; init; } = DateOnly.FromDateTime(DateTime.Now);
  public GenderType Gender { get; init; } = GenderType.Female;
  public string Nationality { get; init; } =  string.Empty;
  public string? PlaceOfBirth { get; init; } = string.Empty;
  public List<string>? Hobbies { get; init; } = [];
  public string? BloodGroup { get; init; } = string.Empty;
  public string? Religion { get; init; } = string.Empty;
  public string CurrentClass { get; init; } = string.Empty;
  public string? Section { get; init; } = string.Empty;
  public string AcademicYear { get; init; } = string.Empty;
  public required DateOnly AdmissionDate { get; init; }
  public string? FatherFullName { get; init; } = string.Empty;
  public string? FatherPhoneNumber { get; init; } = string.Empty;
  public string? FatherOccupation { get; init; } = string.Empty;
  public string? MotherFullName { get; init; } = string.Empty;
  public string? MotherPhoneNumber { get; init; } = string.Empty;
  public string? MotherOccupation { get; init; } = string.Empty;
  public string? GuardianFullName { get; init; } = string.Empty;
  public string? GuardianRelationship { get; init; } = string.Empty;
  public string? GuardianPhoneNumber { get; init; } = string.Empty;
  public string? GuardianEmail { get; init; } = string.Empty;
  public string EmergencyContactName { get; init; } = string.Empty;
  public string EmergencyContactPhoneNumber { get; init; } = string.Empty;
  public string EmergencyContactRelationship { get; init; } = string.Empty;
  public string Address { get; init; } = string.Empty;
  public string City { get; init; } = string.Empty;
  public string? State { get; init; } = string.Empty;
  public string? PostalCode { get; init; } = string.Empty;
  public string Country { get; init; } = string.Empty;
  public string? Allergies { get; init; } = string.Empty;
  public string? MedicalConditions { get; init; } = string.Empty;
  public string? Medications { get; init; } = string.Empty;

  // ── File uploads — bound from multipart/form-data, 
  public List<IFormFile>? ProfilePictureImages { get; init; } = [];
}
