using learning_ms.Web.Application.Common.DTOs.StudentProfile;
using learning_ms.Web.Domain.Entities.StudentProfile;
using Riok.Mapperly.Abstractions;
namespace learning_ms.Web.Application.Mappings.StudentProfileMapper;
[Mapper]
public partial class StudentProfileMapper
{
  [MapperIgnoreTarget(nameof(StudentProfile.Id))]
  [MapperIgnoreTarget(nameof(StudentProfile.ProfilePictureUrl))]
  [MapperIgnoreTarget(nameof(StudentProfile.Age))]
  [MapperIgnoreTarget(nameof(StudentProfile.IsActive))]
  [MapperIgnoreTarget(nameof(StudentProfile.IsGraduated))]
  [MapperIgnoreTarget(nameof(StudentProfile.CreatedAt))]
  [MapperIgnoreTarget(nameof(StudentProfile.UpdatedAt))]
  [MapperIgnoreTarget(nameof(StudentProfile.CreatedBy))]
  [MapperIgnoreTarget(nameof(StudentProfile.UpdatedBy))]
  [MapperIgnoreSource(nameof(CreateStudentProfileRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateStudentProfileRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateStudentProfileRequestDto.IsGraduated))]
  [MapperIgnoreSource(nameof(CreateStudentProfileRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateStudentProfileRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateStudentProfileRequestDto.CreatedBy))]
  [MapperIgnoreSource(nameof(CreateStudentProfileRequestDto.UpdatedBy))]
  [MapperIgnoreSource(nameof(CreateStudentProfileRequestDto.ProfilePictureImages))]
  public partial StudentProfile ToEntity(CreateStudentProfileRequestDto dto);

  [MapperIgnoreSource(nameof(StudentProfile.UpdatedAt))]
  [MapperIgnoreSource(nameof(StudentProfile.CreatedBy))]
  [MapperIgnoreSource(nameof(StudentProfile.UpdatedBy))]
  public partial CreateStudentProfileResponseDto ToResponseDto(StudentProfile entity);
}
