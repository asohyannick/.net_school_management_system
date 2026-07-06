using learning_ms.Web.Application.Common.DTOs.HumanResource;
using learning_ms.Web.Domain.Entities.HumanResources;
namespace learning_ms.Web.Application.Mappings.HumanResourceMapper;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class HumanResourceMapper
{
    [MapperIgnoreTarget(nameof(HumanResource.Id))]
    [MapperIgnoreTarget(nameof(HumanResource.EmployeeNumber))]
    [MapperIgnoreTarget(nameof(HumanResource.ProfilePictureUrl))]
    [MapperIgnoreTarget(nameof(HumanResource.UploadCertifications))]
    [MapperIgnoreTarget(nameof(HumanResource.IsActive))]
    [MapperIgnoreTarget(nameof(HumanResource.TotalCandidatesInterviewed))]
    [MapperIgnoreTarget(nameof(HumanResource.TotalEmployeesHired))]
    [MapperIgnoreTarget(nameof(HumanResource.CreatedBy))]
    [MapperIgnoreTarget(nameof(HumanResource.UpdatedBy))]
    [MapperIgnoreTarget(nameof(HumanResource.CreatedAt))]
    [MapperIgnoreTarget(nameof(HumanResource.UpdatedAt))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.Id))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.EmployeeNumber))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.IsActive))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.TotalCandidatesInterviewed))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.TotalEmployeesHired))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.CreatedBy))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.UpdatedBy))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.CreatedAt))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.UpdatedAt))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.ProfilePicture))]
    [MapperIgnoreSource(nameof(CreateEmployeeRequestDto.CertificationDocuments))]
    public partial HumanResource ToEntity(CreateEmployeeRequestDto dto);

    [MapperIgnoreSource(nameof(HumanResource.CreatedBy))]
    [MapperIgnoreSource(nameof(HumanResource.UpdatedBy))]
    [MapperIgnoreSource(nameof(HumanResource.UpdatedAt))]
    [MapperIgnoreSource(nameof(HumanResource.TotalCandidatesInterviewed))]
    [MapperIgnoreSource(nameof(HumanResource.TotalEmployeesHired))]
    public partial CreateEmployeeResponseDto ToResponseDto(HumanResource entity);
}
