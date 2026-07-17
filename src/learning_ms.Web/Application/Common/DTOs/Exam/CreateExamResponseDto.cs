using learning_ms.Web.Domain.Enums.ExamModes;
using learning_ms.Web.Domain.Enums.ExamType;
namespace learning_ms.Web.Application.Common.DTOs.Exam;
public record CreateExamResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
  public string Title { get; init; } = string.Empty;
  public string? Description { get; init; } = string.Empty;
  public string ExamCode { get; init; } = string.Empty;
  public Guid CourseId { get; init; } = Guid.Empty;
  public Guid SubjectId { get; init; } = Guid.Empty;
  public Guid ClassId { get; init; } = Guid.Empty;
  public Guid TutorId { get; init; } = Guid.Empty;
  public ExamType Type { get; init; } = ExamType.EntranceExam;
  public ExamMode Mode { get; init; } = ExamMode.Offline;
  public DateTime StartDate { get; init; } = DateTime.MinValue;
  public DateTime EndDate { get; init; } = DateTime.MinValue;
  public int DurationInMinutes { get; init; }
  public DateTime? RegistrationDeadline { get; init; } = DateTime.UtcNow;
  public decimal TotalMarks { get; init; } = decimal.Zero;
  public decimal PassingMarks { get; init; } = decimal.Zero;
  public bool IsGradedAutomatically { get; init; }
  public bool AllowRetake { get; init; }
  public int MaxAttempts { get; init; }
  public bool IsPublished { get; init; }
  public bool IsActive { get; init; }
  public bool ShuffleQuestions { get; init; }
  public bool ShowResultsImmediately { get; init; }
  public bool AllowLateSubmission { get; init; }
  public bool IsProctored { get; init; }
  public bool LockBrowser { get; init; }
  public bool DisableCopyPaste { get; init; }
  public string? Instructions { get; init; } = string.Empty;
  public string? Rules { get; init; } = string.Empty;
  public List<string> ExamDocumentUrls { get; init; } = [];
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
