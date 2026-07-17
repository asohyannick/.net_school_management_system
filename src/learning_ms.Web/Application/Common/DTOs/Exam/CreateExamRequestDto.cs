using learning_ms.Web.Domain.Enums.ExamModes;
using learning_ms.Web.Domain.Enums.ExamType;
namespace learning_ms.Web.Application.Common.DTOs.Exam;
public record CreateExamRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public string? ExamCode { get; init; } = string.Empty;
  public Guid? CreatedBy { get; init; } = Guid.Empty;
  public Guid? UpdatedBy { get; init; } = Guid.Empty;
  public bool? IsPublished { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public string? Description { get; init; } = string.Empty;
  public string? Title { get; init; } = string.Empty;   
  public Guid? CourseId { get; init; } = Guid.Empty;
  public Guid? SubjectId { get; init; } = Guid.Empty;
  public Guid? ClassId { get; init; } = Guid.Empty;
  public Guid? TutorId { get; init; } = Guid.Empty;
  public ExamType? Type { get; init; } = ExamType.ContinuousAssessment;
  public ExamMode? Mode { get; init; } = ExamMode.Online;
  public DateTime? StartDate { get; init; } = DateTime.UtcNow;
  public DateTime? EndDate { get; init; } = DateTime.UtcNow;
  public int? DurationInMinutes { get; init; } = int.MinValue;
  public DateTime? RegistrationDeadline { get; init; } = DateTime.UtcNow;
  public decimal? TotalMarks { get; init; } = decimal.Zero;
  public decimal? PassingMarks { get; init; } = decimal.Zero;
  public bool? IsGradedAutomatically { get; init; } = false;
  public bool? AllowRetake { get; init; } = false;
  public int? MaxAttempts { get; init; } = int.MinValue;
  public bool? ShuffleQuestions { get; init; } = false;
  public bool? ShowResultsImmediately { get; init; } = false;
  public bool? AllowLateSubmission { get; init; } = false;
  public bool? IsProctored { get; init; } = false;
  public bool? LockBrowser { get; init; } = false;
  public bool? DisableCopyPaste { get; init; } = false;
  public string? Instructions { get; init; } = string.Empty;
  public string? Rules { get; init; } = string.Empty;

  // ── File uploads — bound from multipart/form-data, not JSON ──
  public List<IFormFile>? ExamDocuments { get; init; }
}
