using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Domain.Entities.TutorProfiles;
public class TutorProfile
{
  public Guid Id { get; set; }
  public string EmployeeId { get; set; } = default!;
  public string FirstName { get; set; } = default!;
  public string MiddleName { get; set; } = string.Empty;
  public string LastName { get; set; } = default!;

  public List<string> ProfilePictureUrl { get; set; } = [];

  public GenderType Gender { get; set; } = default!;

  public DateOnly DateOfBirth { get; set; }

  public string Nationality { get; set; } = string.Empty;

  public string MaritalStatus { get; set; } = string.Empty;

  public string Biography { get; set; } = string.Empty;

  public string Email { get; set; } = default!;

  public string PhoneNumber { get; set; } = default!;

  public string AlternatePhoneNumber { get; set; } = string.Empty;

  public string Address { get; set; } = default!;

  public string City { get; set; } = default!;

  public string State { get; set; } = string.Empty;

  public string PostalCode { get; set; } = string.Empty;

  public string Country { get; set; } = default!;

  public string Department { get; set; } = default!;

  public string Position { get; set; } = default!;

  public DateOnly EmploymentDate { get; set; }

  public decimal Salary { get; set; }

  public bool IsFullTime { get; set; }

  public bool IsActive { get; set; } = true;

  public string HighestQualification { get; set; } = default!;

  public string University { get; set; } = string.Empty;

  public int YearsOfExperience { get; set; }

  public List<string> Certifications { get; set; } = [];

  public List<string> Skills { get; set; } = [];

  public List<string> LanguagesSpoken { get; set; } = [];

  public List<string> Subjects { get; set; } = [];

  public List<string> ClassesAssigned { get; set; } = [];

  public string EmergencyContactName { get; set; } = string.Empty;

  public string EmergencyContactPhoneNumber { get; set; } = string.Empty;

  public string EmergencyContactRelationship { get; set; } = string.Empty;

  public string? LinkedInProfile { get; set; }

  public string? PortfolioWebsite { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
