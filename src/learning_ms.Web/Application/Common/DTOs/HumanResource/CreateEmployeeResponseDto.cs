using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Application.Common.DTOs.HumanResource;
public record CreateEmployeeResponseDto
{
  public required Guid Id { get; init; }
  public required string EmployeeNumber { get; init; }

  public required string FirstName { get; init; }
  public string? MiddleName { get; init; }
  public required string LastName { get; init; }

  public required string Email { get; init; }
  public required string PhoneNumber { get; init; }

  public List<string> ProfilePictureUrl { get; init; } = [];
  public string? Biography { get; init; }

  public required DateOnly DateOfBirth { get; init; }
  public required GenderType Gender { get; init; }
  public string? MaritalStatus { get; init; }
  public string? Nationality { get; init; }

  public required string Address { get; init; }
  public required string City { get; init; }
  public string? State { get; init; }
  public string? PostalCode { get; init; }
  public required string Country { get; init; }

  public required string Department { get; init; }
  public required string JobTitle { get; init; }

  public required DateOnly HireDate { get; init; }
  public DateOnly? TerminationDate { get; init; }

  public required decimal Salary { get; init; }
  public required bool IsFullTime { get; init; }
  public required bool IsActive { get; init; }

  public required string HighestQualification { get; init; }
  public List<string> Certifications { get; init; } = [];
  public List<string> Skills { get; init; } = [];
  public List<string> UploadCertifications { get; init; } = [];

  public required int YearsOfExperience { get; init; }

  public required string EmergencyContactName { get; init; }
  public required string EmergencyContactPhoneNumber { get; init; }
  public required string EmergencyContactRelationship { get; init; }

  public string? LinkedInProfile { get; init; }

  public required DateTime CreatedAt { get; init; }
}
