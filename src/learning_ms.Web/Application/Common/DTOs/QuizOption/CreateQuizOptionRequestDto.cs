namespace learning_ms.Web.Application.Common.DTOs.QuizOption;
public record CreateQuizOptionRequestDto
{
  public Guid? Id { get; init; }

  public required Guid QuizQuestionId { get; init; }

  public required string OptionText { get; init; }

  public bool? IsCorrect { get; init; }
}
