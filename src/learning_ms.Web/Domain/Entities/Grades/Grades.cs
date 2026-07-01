using learning_ms.Web.Domain.Enums.GradeStatus;
namespace learning_ms.Web.Domain.Entities.Grades;

public class Grade
{
  public Guid Id { get; set; }

  public Guid StudentId { get; set; }

  public Guid CourseId { get; set; }

  public Guid SubjectId { get; set; }

  public Guid TutorId { get; set; }

  public Guid? ExamId { get; set; }

  public decimal Score { get; set; }

  public decimal TotalMarks { get; set; }

  public decimal Percentage { get; set; }

  public string LetterGrade { get; set; } = default!;

  public GradeStatus Status { get; set; }

  public decimal ExamScore { get; set; }

  public decimal AssignmentScore { get; set; }

  public decimal QuizScore { get; set; }

  public decimal AttendanceScore { get; set; }

  public string? Remarks { get; set; }

  public string? TutorComment { get; set; }

  public string? StrengthAreas { get; set; }

  public string? WeakAreas { get; set; }

  public int? ClassPosition { get; set; }

  public int? OverallPosition { get; set; }

  public bool IsPassed { get; set; }

  public bool IsPublished { get; set; }

  public bool IsLocked { get; set; }

  public bool IsRechecked { get; set; }

  public bool IsRemarkRequested { get; set; }

  public string? RemarkReason { get; set; }

  public decimal? UpdatedScoreAfterRemark { get; set; }

  public Guid? SemesterId { get; set; }

  public Guid? AcademicYearId { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
