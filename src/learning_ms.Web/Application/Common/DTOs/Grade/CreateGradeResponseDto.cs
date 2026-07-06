using learning_ms.Web.Domain.Enums.GradeStatus;
namespace learning_ms.Web.Application.Common.DTOs.Grade;
public record CreateGradeResponseDto
{
  public required Guid Id { get; init; }

  public required Guid StudentId { get; init; }
  public required Guid CourseId { get; init; }
  public required Guid SubjectId { get; init; }
  public required Guid TutorId { get; init; }
  public Guid? ExamId { get; init; }

  public required decimal Score { get; init; }
  public required decimal TotalMarks { get; init; }
  public required decimal Percentage { get; init; }
  public required string LetterGrade { get; init; }
  public required GradeStatus Status { get; init; }

  public required decimal ExamScore { get; init; }
  public required decimal AssignmentScore { get; init; }
  public required decimal QuizScore { get; init; }
  public required decimal AttendanceScore { get; init; }

  public string? Remarks { get; init; }
  public string? TutorComment { get; init; }
  public string? StrengthAreas { get; init; }
  public string? WeakAreas { get; init; }

  public int? ClassPosition { get; init; }
  public int? OverallPosition { get; init; }

  public required bool IsPassed { get; init; }
  public required bool IsPublished { get; init; }
  public required bool IsLocked { get; init; }

  public Guid? SemesterId { get; init; }
  public Guid? AcademicYearId { get; init; }

  public required DateTime CreatedAt { get; init; }
}
