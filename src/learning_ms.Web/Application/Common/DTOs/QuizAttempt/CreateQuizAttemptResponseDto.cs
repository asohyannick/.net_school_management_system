using learning_ms.Web.Domain.Enums.QuizAttemptStatus;
namespace learning_ms.Web.Application.Common.DTOs.QuizAttempt;
public record CreateQuizAttemptResponseDto
{
  public required Guid Id { get; init; }

  public required Guid QuizId { get; init; }
  public required Guid StudentId { get; init; }

  public required int AttemptNumber { get; init; }

  public required DateTime StartedAt { get; init; }
  public DateTime? SubmittedAt { get; init; }
  public TimeSpan? TimeTaken { get; init; }

  public required decimal Score { get; init; }
  public required decimal TotalMarks { get; init; }
  public required decimal Percentage { get; init; }

  public required bool IsPassed { get; init; }
  public required QuizAttemptStatus Status { get; init; }
  public required bool IsCompleted { get; init; }
  public required bool IsGraded { get; init; }

  public required DateTime CreatedAt { get; init; }
}
