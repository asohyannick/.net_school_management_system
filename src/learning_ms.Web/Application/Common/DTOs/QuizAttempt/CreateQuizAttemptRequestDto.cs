using learning_ms.Web.Domain.Enums.QuizAttemptStatus;
namespace learning_ms.Web.Application.Common.DTOs.QuizAttempt;
public record CreateQuizAttemptRequestDto
{
  public Guid? Id { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public required Guid QuizId { get; init; }
  public required Guid StudentId { get; init; }

  public int? AttemptNumber { get; init; }

  public DateTime? StartedAt { get; init; }
  public DateTime? SubmittedAt { get; init; }
  public TimeSpan? TimeTaken { get; init; }

  public decimal? Score { get; init; }
  public decimal? TotalMarks { get; init; }
  public decimal? Percentage { get; init; }

  public bool? IsPassed { get; init; }
  public QuizAttemptStatus? Status { get; init; }
  public bool? IsCompleted { get; init; }
  public bool? IsGraded { get; init; }
}
