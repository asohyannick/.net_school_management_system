using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Application.Common.DTOs.HumanResource;
public record CreateEmployeeRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public string? EmployeeNumber { get; init; } = string.Empty;
  public Guid? CreatedBy { get; init; } = Guid.Empty;
  public Guid? UpdatedBy { get; init; } = Guid.Empty;
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public string FirstName { get; init; } = string.Empty;
  public string? MiddleName { get; init; } = string.Empty;
  public string LastName { get; init; } = string.Empty;
  public string Email { get; init; } = string.Empty;
  public string PhoneNumber { get; init; } = string.Empty;
  public string? Biography { get; init; } = string.Empty;
  public DateOnly DateOfBirth { get; init; } = DateOnly.MinValue;
  public GenderType Gender { get; init; } = GenderType.Female;
  public string? MaritalStatus { get; init; } = string.Empty;
  public string? Nationality { get; init; } = string.Empty;
  public string Address { get; init; } = string.Empty;
  public  string City { get; init; } = string.Empty;
  public string? State { get; init; } = string.Empty;
  public string? PostalCode { get; init; }  = string.Empty;
  public string Country { get; init; } = string.Empty;
  public string? Department { get; init; } = string.Empty;
  public string? JobTitle { get; init; } = string.Empty;
  public DateOnly HireDate { get; init; } = DateOnly.MinValue;
  public DateOnly? TerminationDate { get; init; } = DateOnly.MinValue;
  public decimal Salary { get; init; } = decimal.Zero;
  public bool? IsFullTime { get; init; } = false;
  public bool? IsActive { get; init; } = false;
  public string HighestQualification { get; init; } = string.Empty;
  public List<string>? Certifications { get; init; } = [];
  public List<string>? Skills { get; init; } = [];
  public int? YearsOfExperience { get; init; }
  public int? TotalCandidatesInterviewed { get; init; }
  public int? TotalEmployeesHired { get; init; }
  public string EmergencyContactName { get; init; } = string.Empty;
  public string EmergencyContactPhoneNumber { get; init; } = string.Empty; 
  public string EmergencyContactRelationship { get; init; } = string.Empty;
  public string? LinkedInProfile { get; init; } = string.Empty;
  // ── File uploads — bound from multipart/form-data, not JSON ──
  public IFormFile? ProfilePicture { get; init; }
  public List<IFormFile>? CertificationDocuments { get; init; }
}
