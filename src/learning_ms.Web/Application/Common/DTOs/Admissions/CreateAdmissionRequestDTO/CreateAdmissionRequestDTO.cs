using learning_ms.Web.Domain.Enums.AdmissionStatus;
using learning_ms.Web.Domain.Enums.GenderType;
namespace learning_ms.Web.Application.Common.DTOs.Admissions.CreateAdmissionRequestDTO;
public record CreateAdmissionRequestDto
{
    public Guid? Id { get; init; } = Guid.Empty;
    public string? ApplicationNumber { get; init; } = string.Empty;
    public DateOnly? ApplicationDate { get; init; } = DateOnly.MinValue;
    public AdmissionStatus? Status { get; init; } = AdmissionStatus.Pending;
    public DateOnly? InterviewDate { get; init; } = DateOnly.MinValue;
    public string? InterviewRemarks { get; init; } = string.Empty;
    public DateOnly? AdmissionDecisionDate { get; init; } = DateOnly.MinValue;
    public string? RejectionReason { get; init; } = string.Empty;
    public decimal? AdmissionFee { get; init; } = decimal.Zero;
    public bool? AdmissionFeePaid { get; init; }
    public DateOnly? AdmissionFeePaidDate { get; init; } = DateOnly.MinValue;
    public string? Remarks { get; init; } = string.Empty;
    public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; init; } = Guid.Empty;
    public Guid? UpdatedBy { get; init; } = Guid.Empty;

    // ── Applicant fields ──
    public string FirstName { get; init; } = string.Empty;
    public string MiddleName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public DateOnly DateOfBirth { get; init; } = DateOnly.MinValue;
    public GenderType Gender { get; init; } = GenderType.Male;
    public string Nationality { get; init; } = string.Empty;
    public string PlaceOfBirth { get; init; } = string.Empty;

    // ── File uploads — bound from multipart/form-data, not JSON ──
    public IFormFile? ProfilePicture { get; init; }
    public List<IFormFile> UploadAdmissionDocuments { get; init; } = [];
    public string Address { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string PreviousSchool { get; init; } = string.Empty;
    public string PreviousClass { get; init; } = string.Empty;
    public decimal? PreviousGradeAverage { get; init; } = decimal.Zero;
    public string ApplyingForClass { get; init; } = string.Empty;
    public string AcademicYear { get; init; } = string.Empty;
    public string FatherFullName { get; init; } = string.Empty;
    public string FatherPhoneNumber { get; init; } = string.Empty;
    public string FatherEmail { get; init; } = string.Empty;
    public string FatherOccupation { get; init; } = string.Empty;
    public string MotherFullName { get; init; } = string.Empty;
    public string MotherPhoneNumber { get; init; } = string.Empty;
    public string MotherEmail { get; init; } = string.Empty;
    public string MotherOccupation { get; init; } = string.Empty;
    public string? GuardianFullName { get; init; } = string.Empty;
    public string? GuardianRelationship { get; init; } = string.Empty;
    public string? GuardianPhoneNumber { get; init; } = string.Empty;
    public string? GuardianEmail { get; init; } = string.Empty;
    public string EmergencyContactName { get; init; } = string.Empty;
    public string EmergencyContactPhoneNumber { get; init; } = string.Empty;
    public string EmergencyContactRelationship { get; init; } = string.Empty;
    public string BloodGroup { get; init; } = string.Empty;
    public List<string>? Allergies { get; init; } = [];
    public List<string>? MedicalConditions { get; init; } = [];
    public List<string>? Disabilities { get; init; } = [];
    public bool TermsAndConditionsAccepted { get; init; }
}
