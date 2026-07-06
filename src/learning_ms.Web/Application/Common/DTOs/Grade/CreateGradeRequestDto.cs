using learning_ms.Web.Domain.Enums.GradeStatus;

namespace learning_ms.Web.Application.Common.DTOs.Grade;

public record CreateGradeRequestDto
{
  public Guid? Id { get; init; }
  public Guid? CreatedBy { get; init; }
  public Guid? UpdatedBy { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public decimal? Percentage { get; init; }
  public string? LetterGrade { get; init; }
  public GradeStatus? Status { get; init; }
  public bool? IsPassed { get; init; }
  public int? ClassPosition { get; init; }
  public int? OverallPosition { get; init; }
  public bool? IsPublished { get; init; }
  public bool? IsLocked { get; init; }
  public bool? IsRechecked { get; init; }
  public bool? IsRemarkRequested { get; init; }
  public string? RemarkReason { get; init; }
  public decimal? UpdatedScoreAfterRemark { get; init; }

  public required Guid StudentId { get; init; }
  public required Guid CourseId { get; init; }
  public required Guid SubjectId { get; init; }
  public required Guid TutorId { get; init; }
  public Guid? ExamId { get; init; }

  public required decimal Score { get; init; }
  public required decimal TotalMarks { get; init; }

  public decimal? ExamScore { get; init; }
  public decimal? AssignmentScore { get; init; }
  public decimal? QuizScore { get; init; }
  public decimal? AttendanceScore { get; init; }

  public string? Remarks { get; init; }
  public string? TutorComment { get; init; }
  public string? StrengthAreas { get; init; }
  public string? WeakAreas { get; init; }

  public Guid? SemesterId { get; init; }
  public Guid? AcademicYearId { get; init; }
}
