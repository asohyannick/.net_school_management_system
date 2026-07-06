using learning_ms.Web.Application.Common.DTOs.Quiz;
using learning_ms.Web.Domain.Entities.Quizzes;
using Riok.Mapperly.Abstractions;
namespace learning_ms.Web.Application.Mappings.QuizMapper;
[Mapper]
public partial class QuizMapper
{
  [MapperIgnoreTarget(nameof(Quiz.Id))]
  [MapperIgnoreTarget(nameof(Quiz.Code))]
  [MapperIgnoreTarget(nameof(Quiz.IsActive))]
  [MapperIgnoreTarget(nameof(Quiz.MaximumAttempts))]
  [MapperIgnoreTarget(nameof(Quiz.ShuffleQuestions))]
  [MapperIgnoreTarget(nameof(Quiz.ShuffleOptions))]
  [MapperIgnoreTarget(nameof(Quiz.ShowResultImmediately))]
  [MapperIgnoreTarget(nameof(Quiz.IsPublished))]
  [MapperIgnoreTarget(nameof(Quiz.CreatedAt))]
  [MapperIgnoreTarget(nameof(Quiz.UpdatedAt))]
  [MapperIgnoreTarget(nameof(Quiz.Questions))]
  [MapperIgnoreTarget(nameof(Quiz.Attempts))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.Code))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.MaximumAttempts))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.ShuffleQuestions))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.ShuffleOptions))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.ShowResultImmediately))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.IsPublished))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateQuizRequestDto.UpdatedAt))]
  public partial Quiz ToEntity(CreateQuizRequestDto dto);

  [MapperIgnoreSource(nameof(Quiz.UpdatedAt))]
  [MapperIgnoreSource(nameof(Quiz.Questions))]
  [MapperIgnoreSource(nameof(Quiz.Attempts))]
  public partial CreateQuizResponseDto ToResponseDto(Quiz entity);
}
