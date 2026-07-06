using learning_ms.Web.Domain.Enums.ExamModes;
using learning_ms.Web.Domain.Enums.ExamType;
namespace learning_ms.Web.Application.Common.DTOs.Exam;

public record CreateExamRequestDto
{
  public Guid? Id { get; init; }
  public string? ExamCode { get; init; }
  public Guid? CreatedBy { get; init; }
  public Guid? UpdatedBy { get; init; }
  public bool? IsPublished { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public required string Title { get; init; }
  public string? Description { get; init; }

  public Guid? CourseId { get; init; }
  public Guid? SubjectId { get; init; }
  public Guid? ClassId { get; init; }
  public Guid? TutorId { get; init; }

  public ExamType? Type { get; init; }
  public ExamMode? Mode { get; init; }

  public DateTime? StartDate { get; init; }
  public DateTime? EndDate { get; init; }
  public int? DurationInMinutes { get; init; }
  public DateTime? RegistrationDeadline { get; init; }

  public decimal? TotalMarks { get; init; }
  public decimal? PassingMarks { get; init; }

  public bool? IsGradedAutomatically { get; init; }
  public bool? AllowRetake { get; init; }
  public int? MaxAttempts { get; init; }

  public bool? ShuffleQuestions { get; init; }
  public bool? ShowResultsImmediately { get; init; }
  public bool? AllowLateSubmission { get; init; }
  public bool? IsProctored { get; init; }
  public bool? LockBrowser { get; init; }
  public bool? DisableCopyPaste { get; init; }

  public string? Instructions { get; init; }
  public string? Rules { get; init; }

  // ── File uploads — bound from multipart/form-data, not JSON ──
  public List<IFormFile>? ExamDocuments { get; init; }
}
