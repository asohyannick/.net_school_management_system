using learning_ms.Web.Application.Common.DTOs.TutorProfile;
using learning_ms.Web.Domain.Entities.TutorProfiles;
using Riok.Mapperly.Abstractions;
namespace learning_ms.Web.Application.Mappings.TutorProfileMapper;

[Mapper]
public partial class TutorProfileMapper
{
  [MapperIgnoreTarget(nameof(TutorProfile.Id))]
  [MapperIgnoreTarget(nameof(TutorProfile.ProfilePictureUrl))]
  [MapperIgnoreTarget(nameof(TutorProfile.IsFullTime))]
  [MapperIgnoreTarget(nameof(TutorProfile.IsActive))]
  [MapperIgnoreTarget(nameof(TutorProfile.YearsOfExperience))]
  [MapperIgnoreTarget(nameof(TutorProfile.CreatedAt))]
  [MapperIgnoreTarget(nameof(TutorProfile.UpdatedAt))]
  [MapperIgnoreTarget(nameof(TutorProfile.CreatedBy))]
  [MapperIgnoreTarget(nameof(TutorProfile.UpdatedBy))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.IsFullTime))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.YearsOfExperience))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.CreatedBy))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.UpdatedBy))]
  [MapperIgnoreSource(nameof(CreateTutorProfileRequestDto.ProfilePictureImages))]
  public partial TutorProfile ToEntity(CreateTutorProfileRequestDto dto);
  [MapperIgnoreSource(nameof(TutorProfile.UpdatedAt))]
  [MapperIgnoreSource(nameof(TutorProfile.CreatedBy))]
  [MapperIgnoreSource(nameof(TutorProfile.UpdatedBy))]
  public partial CreateTutorProfileResponseDto ToResponseDto(TutorProfile entity);
}
