using learning_ms.Web.Domain.Enums.QuestionTypes;
namespace learning_ms.Web.Application.Common.DTOs.QuizQuestion;
public record CreateQuizQuestionResponseDto
{
  public required Guid Id { get; init; }

  public required Guid QuizId { get; init; }

  public required string QuestionText { get; init; }

  public required QuestionType QuestionType { get; init; }

  public required decimal Marks { get; init; }

  public required int Order { get; init; }

  public required bool IsRequired { get; init; }

  public List<CreateQuizOptionResponseDto> Options { get; init; } = [];
}

public record CreateQuizOptionResponseDto
{
  public required Guid Id { get; init; }
  public required string OptionText { get; init; }
  public required bool IsCorrect { get; init; }
}
