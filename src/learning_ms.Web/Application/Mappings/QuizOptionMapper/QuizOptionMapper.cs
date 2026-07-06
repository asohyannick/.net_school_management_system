using learning_ms.Web.Application.Common.DTOs.QuizOption;
using learning_ms.Web.Domain.Entities.QuizOptions;
using Riok.Mapperly.Abstractions;
namespace learning_ms.Web.Application.Mappings.QuizOptionMapper;
[Mapper]
public partial class QuizOptionMapper
{
  [MapperIgnoreTarget(nameof(QuizOption.Id))]
  [MapperIgnoreTarget(nameof(QuizOption.IsCorrect))]
  [MapperIgnoreTarget(nameof(QuizOption.QuizQuestion))]
  [MapperIgnoreSource(nameof(CreateQuizOptionRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateQuizOptionRequestDto.IsCorrect))]
  public partial QuizOption ToEntity(CreateQuizOptionRequestDto dto);

  [MapperIgnoreSource(nameof(QuizOption.QuizQuestion))]
  public partial CreateQuizOptionResponseDto ToResponseDto(QuizOption entity);
}
