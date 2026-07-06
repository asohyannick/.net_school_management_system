namespace learning_ms.Web.Application.Common.DTOs.QuizOption;
public record CreateQuizOptionResponseDto
{
  public required Guid Id { get; init; }

  public required Guid QuizQuestionId { get; init; }

  public required string OptionText { get; init; }

  public required bool IsCorrect { get; init; }
}
