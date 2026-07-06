using learning_ms.Web.Application.Common.DTOs.QuizQuestion;
using learning_ms.Web.Domain.Entities.QuizOptions;
using learning_ms.Web.Domain.Entities.QuizQuestions;
using Riok.Mapperly.Abstractions;
[Mapper]
public partial class QuizQuestionMapper
{
  [MapperIgnoreTarget(nameof(QuizQuestion.Id))]
  [MapperIgnoreTarget(nameof(QuizQuestion.Order))]
  [MapperIgnoreTarget(nameof(QuizQuestion.IsRequired))]
  [MapperIgnoreTarget(nameof(QuizQuestion.Quiz))]
  [MapperIgnoreTarget(nameof(QuizQuestion.Options))]
  [MapperIgnoreSource(nameof(CreateQuizQuestionRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateQuizQuestionRequestDto.Order))]
  [MapperIgnoreSource(nameof(CreateQuizQuestionRequestDto.IsRequired))]
  [MapperIgnoreSource(nameof(CreateQuizQuestionRequestDto.Options))]
  public partial QuizQuestion ToEntity(CreateQuizQuestionRequestDto dto);

  [MapperIgnoreSource(nameof(QuizQuestion.Quiz))]
  public partial CreateQuizQuestionResponseDto ToResponseDto(QuizQuestion entity);

  [MapperIgnoreSource(nameof(QuizOption.QuizQuestionId))]
  [MapperIgnoreSource(nameof(QuizOption.QuizQuestion))]
  public partial CreateQuizOptionResponseDto ToOptionResponseDto(QuizOption entity);
}
