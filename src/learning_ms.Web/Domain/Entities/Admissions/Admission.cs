using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learning_ms.Web.Domain.Enums.AdmissionStatus;
using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Domain.Entities.Admissions;

public class Admission
{
  public Guid Id { get; set; }

  public string ApplicationNumber { get; set; } = default!;
  public DateOnly ApplicationDate { get; set; }
  public List<string> UploadAdmissionDocuments { get; set; } = [];
  public string FirstName { get; set; } = default!;
  public string MiddleName { get; set; } = string.Empty;
  public string LastName { get; set; } = default!;

  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;

  public DateOnly DateOfBirth { get; set; }

  public GenderType Gender { get; set; } = default!;

  public string Nationality { get; set; } = string.Empty;

  public string PlaceOfBirth { get; set; } = string.Empty;

  public string ProfilePictureUrl { get; set; } = string.Empty;

  public string Address { get; set; } = default!;
  public string City { get; set; } = default!;
  public string State { get; set; } = string.Empty;
  public string PostalCode { get; set; } = string.Empty;
  public string Country { get; set; } = default!;

  public string PreviousSchool { get; set; } = string.Empty;

  public string PreviousClass { get; set; } = string.Empty;

  public decimal? PreviousGradeAverage { get; set; }

  public string ApplyingForClass { get; set; } = default!;

  public string AcademicYear { get; set; } = default!;

  public string FatherFullName { get; set; } = string.Empty;

  public string FatherPhoneNumber { get; set; } = string.Empty;

  public string FatherEmail { get; set; } = string.Empty;

  public string FatherOccupation { get; set; } = string.Empty;

  public string MotherFullName { get; set; } = string.Empty;

  public string MotherPhoneNumber { get; set; } = string.Empty;

  public string MotherEmail { get; set; } = string.Empty;

  public string MotherOccupation { get; set; } = string.Empty;

  public string? GuardianFullName { get; set; }

  public string? GuardianRelationship { get; set; }

  public string? GuardianPhoneNumber { get; set; }

  public string? GuardianEmail { get; set; }

  public string EmergencyContactName { get; set; } = default!;

  public string EmergencyContactPhoneNumber { get; set; } = default!;

  public string EmergencyContactRelationship { get; set; } = default!;

  public string BloodGroup { get; set; } = string.Empty;

  public List<string>? Allergies { get; set; } = [];

  public List<string>? MedicalConditions { get; set; } = [];

  public List<string>? Disabilities { get; set; } = [];

  public AdmissionStatus Status { get; set; } = AdmissionStatus.Pending;

  public DateOnly? InterviewDate { get; set; }

  public string? InterviewRemarks { get; set; }

  public DateOnly? AdmissionDecisionDate { get; set; }

  public string? RejectionReason { get; set; }

  public decimal AdmissionFee { get; set; }

  public bool AdmissionFeePaid { get; set; }

  public DateOnly? AdmissionFeePaidDate { get; set; }

  public string? Remarks { get; set; }

  public bool TermsAndConditionsAccepted { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
