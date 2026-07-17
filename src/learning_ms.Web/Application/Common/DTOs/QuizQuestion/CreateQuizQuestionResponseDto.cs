using learning_ms.Web.Domain.Enums.QuestionTypes;
namespace learning_ms.Web.Application.Common.DTOs.QuizQuestion;
public record CreateQuizQuestionResponseDto
{
  public  Guid Id { get; init; } = Guid.Empty;
  public Guid QuizId { get; init; } = Guid.Empty;
  public string QuestionText { get; init; } = string.Empty;
  public QuestionType QuestionType { get; init; } = QuestionType.MultipleChoice;
  public decimal Marks { get; init; } = decimal.Zero;
  public int Order { get; init; }  = int.MinValue;
  public bool IsRequired { get; init; } = false;
  public List<CreateQuizOptionResponseDto> Options { get; init; } = [];
}

public record CreateQuizOptionResponseDto
{
  public Guid Id { get; init; } =  Guid.Empty;
  public string OptionText { get; init; } = string.Empty;
  public  bool IsCorrect { get; init; } =  false;
}
