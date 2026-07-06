namespace learning_ms.Web.Application.Common.DTOs.Quiz;
public record CreateQuizResponseDto
{
  public required Guid Id { get; init; }

  public required string Title { get; init; }
  public string? Description { get; init; }
  public string? Instructions { get; init; }
  public required string Code { get; init; }

  public required Guid CourseId { get; init; }
  public required Guid SubjectId { get; init; }
  public required Guid TeacherId { get; init; }
  public Guid? ClassId { get; init; }

  public required int TotalMarks { get; init; }
  public required int PassingMarks { get; init; }
  public required int DurationInMinutes { get; init; }
  public required int MaximumAttempts { get; init; }

  public required bool ShuffleQuestions { get; init; }
  public required bool ShuffleOptions { get; init; }
  public required bool ShowResultImmediately { get; init; }
  public required bool IsPublished { get; init; }
  public required bool IsActive { get; init; }

  public required DateTime StartDate { get; init; }
  public required DateTime EndDate { get; init; }

  public required DateTime CreatedAt { get; init; }
}
