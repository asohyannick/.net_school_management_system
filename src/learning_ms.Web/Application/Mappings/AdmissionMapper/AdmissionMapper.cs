using learning_ms.Web.Application.Common.DTOs.Admissions.CreateAdmissionRequestDTO;
using learning_ms.Web.Application.Common.DTOs.Admissions.CreateAdmissionResponseDTO;
using learning_ms.Web.Domain.Entities.Admissions;
using Riok.Mapperly.Abstractions;
namespace learning_ms.Web.Application.Mappings.AdmissionMapper;

[Mapper]
public partial class AdmissionMapper
{
  [MapperIgnoreTarget(nameof(Admission.Id))]
  [MapperIgnoreTarget(nameof(Admission.ApplicationNumber))]
  [MapperIgnoreTarget(nameof(Admission.ApplicationDate))]
  [MapperIgnoreTarget(nameof(Admission.Status))]
  [MapperIgnoreTarget(nameof(Admission.InterviewDate))]
  [MapperIgnoreTarget(nameof(Admission.InterviewRemarks))]
  [MapperIgnoreTarget(nameof(Admission.AdmissionDecisionDate))]
  [MapperIgnoreTarget(nameof(Admission.RejectionReason))]
  [MapperIgnoreTarget(nameof(Admission.AdmissionFee))]
  [MapperIgnoreTarget(nameof(Admission.AdmissionFeePaid))]
  [MapperIgnoreTarget(nameof(Admission.AdmissionFeePaidDate))]
  [MapperIgnoreTarget(nameof(Admission.Remarks))]
  [MapperIgnoreTarget(nameof(Admission.CreatedAt))]
  [MapperIgnoreTarget(nameof(Admission.UpdatedAt))]
  [MapperIgnoreTarget(nameof(Admission.CreatedBy))]
  [MapperIgnoreTarget(nameof(Admission.UpdatedBy))]
  [MapperIgnoreTarget(nameof(Admission.ProfilePictureUrl))]
  [MapperIgnoreTarget(nameof(Admission.UploadAdmissionDocuments))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.ProfilePicture))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.UploadAdmissionDocuments))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.ApplicationNumber))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.ApplicationDate))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.Status))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.InterviewDate))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.InterviewRemarks))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.AdmissionDecisionDate))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.RejectionReason))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.AdmissionFee))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.AdmissionFeePaid))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.AdmissionFeePaidDate))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.Remarks))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.CreatedBy))]
  [MapperIgnoreSource(nameof(CreateAdmissionRequestDto.UpdatedBy))]
  public partial Admission ToEntity(CreateAdmissionRequestDto dto);

  [MapperIgnoreSource(nameof(Admission.InterviewDate))]
  [MapperIgnoreSource(nameof(Admission.InterviewRemarks))]
  [MapperIgnoreSource(nameof(Admission.AdmissionDecisionDate))]
  [MapperIgnoreSource(nameof(Admission.RejectionReason))]
  [MapperIgnoreSource(nameof(Admission.AdmissionFee))]
  [MapperIgnoreSource(nameof(Admission.AdmissionFeePaid))]
  [MapperIgnoreSource(nameof(Admission.AdmissionFeePaidDate))]
  [MapperIgnoreSource(nameof(Admission.Remarks))]
  [MapperIgnoreSource(nameof(Admission.UpdatedAt))]
  [MapperIgnoreSource(nameof(Admission.CreatedBy))]
  [MapperIgnoreSource(nameof(Admission.UpdatedBy))]
  [MapperIgnoreSource(nameof(Admission.CreatedAt))]
  public partial CreateAdmissionResponseDto ToResponseDto(Admission entity);
}
