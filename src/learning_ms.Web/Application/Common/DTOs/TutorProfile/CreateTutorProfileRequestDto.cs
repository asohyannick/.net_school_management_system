using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Application.Common.DTOs.TutorProfile;
using Microsoft.AspNetCore.Http;

public record CreateTutorProfileRequestDto
{
  public Guid? Id { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }
  public Guid? CreatedBy { get; init; }
  public Guid? UpdatedBy { get; init; }

  public required string EmployeeId { get; init; }

  public required string FirstName { get; init; }
  public string? MiddleName { get; init; }
  public required string LastName { get; init; }

  public required GenderType Gender { get; init; }

  public required DateOnly DateOfBirth { get; init; }

  public string? Nationality { get; init; }
  public string? MaritalStatus { get; init; }
  public string? Biography { get; init; }

  public required string Email { get; init; }
  public required string PhoneNumber { get; init; }
  public string? AlternatePhoneNumber { get; init; }

  public required string Address { get; init; }
  public required string City { get; init; }
  public string? State { get; init; }
  public string? PostalCode { get; init; }
  public required string Country { get; init; }

  public required string Department { get; init; }
  public required string Position { get; init; }

  public required DateOnly EmploymentDate { get; init; }

  public required decimal Salary { get; init; }

  public bool? IsFullTime { get; init; }

  public required string HighestQualification { get; init; }
  public string? University { get; init; }
  public int? YearsOfExperience { get; init; }

  public List<string>? Certifications { get; init; }
  public List<string>? Skills { get; init; }
  public List<string>? LanguagesSpoken { get; init; }
  public List<string>? Subjects { get; init; }
  public List<string>? ClassesAssigned { get; init; }

  public string? EmergencyContactName { get; init; }
  public string? EmergencyContactPhoneNumber { get; init; }
  public string? EmergencyContactRelationship { get; init; }

  public string? LinkedInProfile { get; init; }
  public string? PortfolioWebsite { get; init; }

  // ── File uploads — bound from multipart/form-data, not JSON ──
  public List<IFormFile>? ProfilePictureImages { get; init; }
}
