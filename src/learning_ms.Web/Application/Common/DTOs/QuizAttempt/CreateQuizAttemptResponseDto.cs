using learning_ms.Web.Domain.Enums.QuizAttemptStatus;
namespace learning_ms.Web.Application.Common.DTOs.QuizAttempt;
public record CreateQuizAttemptResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
  public Guid QuizId { get; init; } = Guid.Empty;
  public Guid StudentId { get; init; } = Guid.Empty;
  public int AttemptNumber { get; init; }
  public DateTime StartedAt { get; init; } = DateTime.Now;  
  public DateTime? SubmittedAt { get; init; } = DateTime.Now;
  public TimeSpan? TimeTaken { get; init; } = TimeSpan.Zero;
  public decimal Score { get; init; } = decimal.Zero;
  public decimal TotalMarks { get; init; } = decimal.Zero;
  public decimal Percentage { get; init; } = decimal.Zero;
  public bool IsPassed { get; init; } = false;
  public QuizAttemptStatus Status { get; init; } = QuizAttemptStatus.Cancelled;
  public bool IsCompleted { get; init; } = false;
  public bool IsGraded { get; init; } = false;
  public DateTime CreatedAt { get; init; } = DateTime.Now;
}
