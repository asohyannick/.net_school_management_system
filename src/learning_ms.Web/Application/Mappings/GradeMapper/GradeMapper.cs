using learning_ms.Web.Application.Common.DTOs.Grade;
using learning_ms.Web.Domain.Entities.Grades;
namespace learning_ms.Web.Application.Mappings.GradeMapper;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class GradeMapper
{
    [MapperIgnoreTarget(nameof(Grade.Id))]
    [MapperIgnoreTarget(nameof(Grade.Percentage))]
    [MapperIgnoreTarget(nameof(Grade.LetterGrade))]
    [MapperIgnoreTarget(nameof(Grade.Status))]
    [MapperIgnoreTarget(nameof(Grade.IsPassed))]
    [MapperIgnoreTarget(nameof(Grade.ClassPosition))]
    [MapperIgnoreTarget(nameof(Grade.OverallPosition))]
    [MapperIgnoreTarget(nameof(Grade.IsPublished))]
    [MapperIgnoreTarget(nameof(Grade.IsLocked))]
    [MapperIgnoreTarget(nameof(Grade.IsRechecked))]
    [MapperIgnoreTarget(nameof(Grade.IsRemarkRequested))]
    [MapperIgnoreTarget(nameof(Grade.RemarkReason))]
    [MapperIgnoreTarget(nameof(Grade.UpdatedScoreAfterRemark))]
    [MapperIgnoreTarget(nameof(Grade.CreatedBy))]
    [MapperIgnoreTarget(nameof(Grade.UpdatedBy))]
    [MapperIgnoreTarget(nameof(Grade.CreatedAt))]
    [MapperIgnoreTarget(nameof(Grade.UpdatedAt))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.Id))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.Percentage))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.LetterGrade))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.Status))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.IsPassed))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.ClassPosition))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.OverallPosition))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.IsPublished))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.IsLocked))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.IsRechecked))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.IsRemarkRequested))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.RemarkReason))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.UpdatedScoreAfterRemark))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.CreatedBy))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.UpdatedBy))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.CreatedAt))]
    [MapperIgnoreSource(nameof(CreateGradeRequestDto.UpdatedAt))]
    public partial Grade ToEntity(CreateGradeRequestDto dto);

    [MapperIgnoreSource(nameof(Grade.IsRechecked))]
    [MapperIgnoreSource(nameof(Grade.IsRemarkRequested))]
    [MapperIgnoreSource(nameof(Grade.RemarkReason))]
    [MapperIgnoreSource(nameof(Grade.UpdatedScoreAfterRemark))]
    [MapperIgnoreSource(nameof(Grade.CreatedBy))]
    [MapperIgnoreSource(nameof(Grade.UpdatedBy))]
    [MapperIgnoreSource(nameof(Grade.UpdatedAt))]
    public partial CreateGradeResponseDto ToResponseDto(Grade entity);
}
