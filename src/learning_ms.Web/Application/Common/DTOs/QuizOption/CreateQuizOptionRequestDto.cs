namespace learning_ms.Web.Application.Common.DTOs.QuizOption;
public record CreateQuizOptionRequestDto
{
  public Guid? Id { get; init; } =  Guid.Empty;
  public Guid QuizQuestionId { get; init; } = Guid.Empty;
  public string OptionText { get; init; } = string.Empty;
  public bool? IsCorrect { get; init; } = false;
}
