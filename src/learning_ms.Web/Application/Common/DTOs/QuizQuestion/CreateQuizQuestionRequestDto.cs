using learning_ms.Web.Domain.Enums.QuestionTypes;
namespace learning_ms.Web.Application.Common.DTOs.QuizQuestion;
public record CreateQuizQuestionRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public Guid QuizId { get; init; } = Guid.Empty;
  public string QuestionText { get; init; } = string.Empty;
  public QuestionType QuestionType { get; init; } = QuestionType.MultipleChoice;
  public decimal Marks { get; init; } = decimal.Zero;
  public int? Order { get; init; }
  public bool? IsRequired { get; init; }
  public List<CreateQuizOptionRequestDto>? Options { get; init; } = [];
}

public record CreateQuizOptionRequestDto
{
  public string OptionText { get; init; } = string.Empty;
  public bool IsCorrect { get; init; }
}
