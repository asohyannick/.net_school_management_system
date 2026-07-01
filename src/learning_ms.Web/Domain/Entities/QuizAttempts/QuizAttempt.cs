using learning_ms.Web.Domain.Entities.Quizzes;
using learning_ms.Web.Domain.Enums.QuizAttemptStatus;

namespace learning_ms.Web.Domain.Entities.QuizAttempts;

public class QuizAttempt
{
  public Guid Id { get; set; }

  public Guid QuizId { get; set; }

  public Guid StudentId { get; set; }

  public int AttemptNumber { get; set; } = 1;

  public DateTime StartedAt { get; set; } = DateTime.UtcNow;

  public DateTime? SubmittedAt { get; set; }

  public TimeSpan? TimeTaken { get; set; }

  public decimal Score { get; set; }

  public decimal TotalMarks { get; set; }

  public decimal Percentage { get; set; }

  public bool IsPassed { get; set; }

  public QuizAttemptStatus Status { get; set; } = QuizAttemptStatus.InProgress;

  public bool IsCompleted { get; set; }

  public bool IsGraded { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime? UpdatedAt { get; set; }

  public Quiz Quiz { get; set; } = null!;
}
