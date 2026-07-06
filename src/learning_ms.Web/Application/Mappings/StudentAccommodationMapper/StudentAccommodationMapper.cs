using learning_ms.Web.Application.Common.DTOs.StudentAccommodation;
using learning_ms.Web.Domain.Entities.StudentAccommodations;
using Riok.Mapperly.Abstractions;

namespace learning_ms.Web.Application.Mappings.StudentAccommodationMapper;

[Mapper]
public partial class StudentAccommodationMapper
{
  [MapperIgnoreTarget(nameof(StudentAccommodation.Id))]
  [MapperIgnoreTarget(nameof(StudentAccommodation.AmountPaid))]
  [MapperIgnoreTarget(nameof(StudentAccommodation.Balance))]
  [MapperIgnoreTarget(nameof(StudentAccommodation.Status))]
  [MapperIgnoreTarget(nameof(StudentAccommodation.IsActive))]
  [MapperIgnoreTarget(nameof(StudentAccommodation.CreatedAt))]
  [MapperIgnoreTarget(nameof(StudentAccommodation.UpdatedAt))]
  [MapperIgnoreTarget(nameof(StudentAccommodation.Accommodation))]
  [MapperIgnoreSource(nameof(CreateStudentAccommodationRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateStudentAccommodationRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateStudentAccommodationRequestDto.AmountPaid))]
  [MapperIgnoreSource(nameof(CreateStudentAccommodationRequestDto.Status))]
  [MapperIgnoreSource(nameof(CreateStudentAccommodationRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateStudentAccommodationRequestDto.UpdatedAt))]
  public partial StudentAccommodation ToEntity(CreateStudentAccommodationRequestDto dto);

  [MapperIgnoreSource(nameof(StudentAccommodation.UpdatedAt))]
  [MapperIgnoreSource(nameof(StudentAccommodation.Accommodation))]
  public partial CreateStudentAccommodationResponseDto ToResponseDto(StudentAccommodation entity);
}
