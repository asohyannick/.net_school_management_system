using learning_ms.Web.Domain.Entities.QuizAttempts;
using learning_ms.Web.Domain.Entities.QuizQuestions;

namespace learning_ms.Web.Domain.Entities.Quizzes;

public class Quiz
{
  public Guid Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public string Instructions { get; set; } = string.Empty;

  public string Code { get; set; } = string.Empty;

  public Guid CourseId { get; set; }

  public Guid SubjectId { get; set; }

  public Guid TeacherId { get; set; }

  public Guid? ClassId { get; set; }

  public int TotalMarks { get; set; }

  public int PassingMarks { get; set; }

  public int DurationInMinutes { get; set; }

  public int MaximumAttempts { get; set; } = 1;

  public bool ShuffleQuestions { get; set; } = false;

  public bool ShuffleOptions { get; set; } = false;

  public bool ShowResultImmediately { get; set; } = false;

  public bool IsPublished { get; set; } = false;

  public bool IsActive { get; set; } = true;

  public DateTime StartDate { get; set; }

  public DateTime EndDate { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime? UpdatedAt { get; set; }

  public ICollection<QuizQuestion> Questions { get; set; } = [];

  public ICollection<QuizAttempt> Attempts { get; set; } = [];
}
