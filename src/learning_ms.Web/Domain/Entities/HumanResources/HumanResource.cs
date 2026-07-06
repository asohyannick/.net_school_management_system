using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Domain.Entities.HumanResources;
public class HumanResource
{
  public Guid Id { get; set; }

  public string EmployeeNumber { get; set; } = default!;

  public string FirstName { get; set; } = default!;

  public string MiddleName { get; set; } = string.Empty;

  public string LastName { get; set; } = default!;

  public string Email { get; set; } = default!;

  public string PhoneNumber { get; set; } = default!;

  public List<string>? ProfilePictureUrl { get; set; } = [];

  public string Biography { get; set; } = string.Empty;

  public DateOnly DateOfBirth { get; set; }

  public GenderType Gender { get; set; } = default!;

  public string MaritalStatus { get; set; } = string.Empty;

  public string Nationality { get; set; } = string.Empty;

  public string Address { get; set; } = default!;

  public string City { get; set; } = default!;

  public string State { get; set; } = string.Empty;

  public string PostalCode { get; set; } = string.Empty;

  public string Country { get; set; } = default!;

  public string Department { get; set; } = "Human Resources";

  public string JobTitle { get; set; } = "HR Officer";

  public DateOnly HireDate { get; set; }

  public DateOnly? TerminationDate { get; set; }

  public decimal Salary { get; set; }

  public bool IsFullTime { get; set; }

  public bool IsActive { get; set; } = true;

  public string HighestQualification { get; set; } = default!;

  public List<string> Certifications { get; set; } = new();

  public List<string> Skills { get; set; } = new();

  public List<string> UploadCertifications { get; set; } = [];

  public int YearsOfExperience { get; set; }

  public int TotalCandidatesInterviewed { get; set; }

  public int TotalEmployeesHired { get; set; }

  public string EmergencyContactName { get; set; } = default!;

  public string EmergencyContactPhoneNumber { get; set; } = default!;

  public string EmergencyContactRelationship { get; set; } = default!;

  public string? LinkedInProfile { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
