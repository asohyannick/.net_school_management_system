using learning_ms.Web.Domain.Enums.ExamModes;
using learning_ms.Web.Domain.Enums.ExamType;
namespace learning_ms.Web.Application.Common.DTOs.Exam;
public record CreateExamResponseDto
{
  public required Guid Id { get; init; }
  public required string Title { get; init; }
  public string? Description { get; init; }
  public required string ExamCode { get; init; }

  public required Guid CourseId { get; init; }
  public required Guid SubjectId { get; init; }
  public required Guid ClassId { get; init; }
  public required Guid TutorId { get; init; }

  public required ExamType Type { get; init; }
  public required ExamMode Mode { get; init; }

  public required DateTime StartDate { get; init; }
  public required DateTime EndDate { get; init; }
  public required int DurationInMinutes { get; init; }
  public DateTime? RegistrationDeadline { get; init; }

  public required decimal TotalMarks { get; init; }
  public required decimal PassingMarks { get; init; }

  public required bool IsGradedAutomatically { get; init; }
  public required bool AllowRetake { get; init; }
  public required int MaxAttempts { get; init; }

  public required bool IsPublished { get; init; }
  public required bool IsActive { get; init; }

  public required bool ShuffleQuestions { get; init; }
  public required bool ShowResultsImmediately { get; init; }
  public required bool AllowLateSubmission { get; init; }
  public required bool IsProctored { get; init; }
  public required bool LockBrowser { get; init; }
  public required bool DisableCopyPaste { get; init; }

  public string? Instructions { get; init; }
  public string? Rules { get; init; }

  public List<string> ExamDocumentUrls { get; init; } = [];

  public required DateTime CreatedAt { get; init; }
}
