using learning_ms.Web.Application.Common.DTOs.QuizAttempt;
using learning_ms.Web.Domain.Entities.QuizAttempts;
using Riok.Mapperly.Abstractions;
namespace learning_ms.Web.Application.Mappings.QuizAttemptMapper;

[Mapper]
public partial class QuizAttemptMapper
{
  [MapperIgnoreTarget(nameof(QuizAttempt.Id))]
  [MapperIgnoreTarget(nameof(QuizAttempt.AttemptNumber))]
  [MapperIgnoreTarget(nameof(QuizAttempt.StartedAt))]
  [MapperIgnoreTarget(nameof(QuizAttempt.SubmittedAt))]
  [MapperIgnoreTarget(nameof(QuizAttempt.TimeTaken))]
  [MapperIgnoreTarget(nameof(QuizAttempt.Score))]
  [MapperIgnoreTarget(nameof(QuizAttempt.TotalMarks))]
  [MapperIgnoreTarget(nameof(QuizAttempt.Percentage))]
  [MapperIgnoreTarget(nameof(QuizAttempt.IsPassed))]
  [MapperIgnoreTarget(nameof(QuizAttempt.Status))]
  [MapperIgnoreTarget(nameof(QuizAttempt.IsCompleted))]
  [MapperIgnoreTarget(nameof(QuizAttempt.IsGraded))]
  [MapperIgnoreTarget(nameof(QuizAttempt.CreatedAt))]
  [MapperIgnoreTarget(nameof(QuizAttempt.UpdatedAt))]
  [MapperIgnoreTarget(nameof(QuizAttempt.Quiz))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.AttemptNumber))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.StartedAt))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.SubmittedAt))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.TimeTaken))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.Score))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.TotalMarks))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.Percentage))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.IsPassed))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.Status))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.IsCompleted))]
  [MapperIgnoreSource(nameof(CreateQuizAttemptRequestDto.IsGraded))]
  public partial QuizAttempt ToEntity(CreateQuizAttemptRequestDto dto);

  [MapperIgnoreSource(nameof(QuizAttempt.UpdatedAt))]
  [MapperIgnoreSource(nameof(QuizAttempt.Quiz))]
  public partial CreateQuizAttemptResponseDto ToResponseDto(QuizAttempt entity);
}
