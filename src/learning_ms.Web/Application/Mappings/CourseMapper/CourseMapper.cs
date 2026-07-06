using learning_ms.Web.Application.Common.DTOs.Course;
using learning_ms.Web.Domain.Entities.Courses;
namespace learning_ms.Web.Application.Mappings.CourseMapper;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class CourseMapper
{
  [MapperIgnoreTarget(nameof(Course.Id))]
  [MapperIgnoreTarget(nameof(Course.CourseImageUrl))]
  [MapperIgnoreTarget(nameof(Course.IsPublished))]
  [MapperIgnoreTarget(nameof(Course.CreatedAt))]
  [MapperIgnoreTarget(nameof(Course.UpdatedAt))]
  [MapperIgnoreTarget(nameof(Course.CreatedBy))]
  [MapperIgnoreTarget(nameof(Course.UpdatedBy))]
  [MapperIgnoreSource(nameof(CreateCourseRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateCourseRequestDto.IsPublished))]
  [MapperIgnoreSource(nameof(CreateCourseRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateCourseRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateCourseRequestDto.CreatedBy))]
  [MapperIgnoreSource(nameof(CreateCourseRequestDto.UpdatedBy))]
  [MapperIgnoreSource(nameof(CreateCourseRequestDto.CourseImage))]
  public partial Course ToEntity(CreateCourseRequestDto dto);

  [MapperIgnoreSource(nameof(Course.CreatedBy))]
  [MapperIgnoreSource(nameof(Course.UpdatedBy))]
  [MapperIgnoreSource(nameof(Course.UpdatedAt))]
  public partial CreateCourseResponseDto ToResponseDto(Course entity);
}
