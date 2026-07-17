using learning_ms.Web.Domain.Enums.GradeStatus;
namespace learning_ms.Web.Application.Common.DTOs.Grade;
public record CreateGradeResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
  public Guid StudentId { get; init; } = Guid.Empty;
  public Guid CourseId { get; init; } = Guid.Empty;
  public Guid SubjectId { get; init; } = Guid.Empty;
  public Guid TutorId { get; init; } = Guid.Empty;
  public Guid? ExamId { get; init; } = Guid.Empty;
  public decimal Score { get; init; } = decimal.Zero;
  public decimal TotalMarks { get; init; } = decimal.Zero;
  public decimal Percentage { get; init; } = decimal.Zero;
  public  string LetterGrade { get; init; } =  string.Empty;
  public GradeStatus Status { get; init; } = GradeStatus.Approved;
  public decimal ExamScore { get; init; }  = decimal.Zero;
  public  decimal AssignmentScore { get; init; } = decimal.Zero;
  public decimal QuizScore { get; init; } = decimal.Zero;
  public decimal AttendanceScore { get; init; } = decimal.Zero;
  public string? Remarks { get; init; } = string.Empty;
  public string? TutorComment { get; init; } = string.Empty;
  public string? StrengthAreas { get; init; } = string.Empty;
  public string? WeakAreas { get; init; } = string.Empty;
  public int? ClassPosition { get; init; } = int.MinValue;
  public int? OverallPosition { get; init; } = int.MinValue;
  public bool IsPassed { get; init; } = false;
  public bool IsPublished { get; init; } = false;
  public bool IsLocked { get; init; } = false;
  public Guid? SemesterId { get; init; } = Guid.Empty;
  public Guid? AcademicYearId { get; init; } = Guid.Empty;
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
