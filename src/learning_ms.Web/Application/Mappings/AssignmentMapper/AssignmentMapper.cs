using learning_ms.Web.Application.Common.DTOs.Assignment;
using learning_ms.Web.Domain.Entities.Assignment;
namespace learning_ms.Web.Application.Mappings.AssignmentMapper;
using Riok.Mapperly.Abstractions;
[Mapper]
public partial class AssignmentMapper
{
  [MapperIgnoreTarget(nameof(Assignment.Id))]
  [MapperIgnoreTarget(nameof(Assignment.TutorId))]
  [MapperIgnoreTarget(nameof(Assignment.AttachmentUrl))]
  [MapperIgnoreTarget(nameof(Assignment.AttachmentFileName))]
  [MapperIgnoreTarget(nameof(Assignment.IsPublished))]
  [MapperIgnoreTarget(nameof(Assignment.CreatedAt))]
  [MapperIgnoreTarget(nameof(Assignment.UpdatedAt))]
  [MapperIgnoreTarget(nameof(Assignment.CreatedBy))]
  [MapperIgnoreTarget(nameof(Assignment.UpdatedBy))]
  [MapperIgnoreTarget(nameof(Assignment.Course))]
  [MapperIgnoreTarget(nameof(Assignment.Tutor))]
  [MapperIgnoreSource(nameof(CreateAssignmentRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateAssignmentRequestDto.TutorId))]
  [MapperIgnoreSource(nameof(CreateAssignmentRequestDto.IsPublished))]
  [MapperIgnoreSource(nameof(CreateAssignmentRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateAssignmentRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateAssignmentRequestDto.CreatedBy))]
  [MapperIgnoreSource(nameof(CreateAssignmentRequestDto.UpdatedBy))]
  [MapperIgnoreSource(nameof(CreateAssignmentRequestDto.Attachment))]
  public partial Assignment ToEntity(CreateAssignmentRequestDto dto);

  [MapperIgnoreSource(nameof(Assignment.CreatedBy))]
  [MapperIgnoreSource(nameof(Assignment.UpdatedBy))]
  [MapperIgnoreSource(nameof(Assignment.UpdatedAt))]
  [MapperIgnoreSource(nameof(Assignment.Course))]
  [MapperIgnoreSource(nameof(Assignment.Tutor))]
  public partial CreateAssignmentResponseDto ToResponseDto(Assignment entity);
}
