using learning_ms.Web.Domain.Enums.ExamModes;
using learning_ms.Web.Domain.Enums.ExamType;

namespace learning_ms.Web.Domain.Entities.Exams;

public class Exam
{
  public Guid Id { get; set; }

  public string Title { get; set; } = default!;

  public string Description { get; set; } = string.Empty;

  public string ExamCode { get; set; } = default!;

  public Guid CourseId { get; set; }

  public Guid SubjectId { get; set; }

  public Guid ClassId { get; set; }

  public Guid TutorId { get; set; }

  public ExamType Type { get; set; }

  public ExamMode Mode { get; set; }

  public DateTime StartDate { get; set; }

  public DateTime EndDate { get; set; }

  public int DurationInMinutes { get; set; }

  public DateTime? RegistrationDeadline { get; set; }

  public decimal TotalMarks { get; set; }

  public decimal PassingMarks { get; set; }

  public bool IsGradedAutomatically { get; set; }

  public bool AllowRetake { get; set; }

  public int MaxAttempts { get; set; } = 1;

  public bool IsPublished { get; set; }

  public bool IsActive { get; set; } = true;

  public bool ShuffleQuestions { get; set; }

  public bool ShowResultsImmediately { get; set; }

  public bool AllowLateSubmission { get; set; }

  public bool IsProctored { get; set; }

  public bool LockBrowser { get; set; }

  public bool DisableCopyPaste { get; set; }

  public string? Instructions { get; set; }

  public string? Rules { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
