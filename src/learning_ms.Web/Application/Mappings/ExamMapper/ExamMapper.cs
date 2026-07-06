using learning_ms.Web.Application.Common.DTOs.Exam;
using learning_ms.Web.Domain.Entities.Exams;
namespace learning_ms.Web.Application.Mappings.ExamMapper;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class ExamMapper
{
  [MapperIgnoreTarget(nameof(Exam.Id))]
  [MapperIgnoreTarget(nameof(Exam.ExamCode))]
  [MapperIgnoreTarget(nameof(Exam.ExamDocuments))]
  [MapperIgnoreTarget(nameof(Exam.IsPublished))]
  [MapperIgnoreTarget(nameof(Exam.IsActive))]
  [MapperIgnoreTarget(nameof(Exam.CreatedBy))]
  [MapperIgnoreTarget(nameof(Exam.UpdatedBy))]
  [MapperIgnoreTarget(nameof(Exam.CreatedAt))]
  [MapperIgnoreTarget(nameof(Exam.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.ExamCode))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.ExamDocuments))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.IsPublished))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.CreatedBy))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.UpdatedBy))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateExamRequestDto.UpdatedAt))]
  public partial Exam ToEntity(CreateExamRequestDto dto);

  [MapperIgnoreSource(nameof(Exam.CreatedBy))]
  [MapperIgnoreSource(nameof(Exam.UpdatedBy))]
  [MapperIgnoreSource(nameof(Exam.UpdatedAt))]
  [MapProperty(nameof(Exam.ExamDocuments), nameof(CreateExamResponseDto.ExamDocumentUrls))]
  public partial CreateExamResponseDto ToResponseDto(Exam entity);
}
