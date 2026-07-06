namespace learning_ms.Web.Application.Common.DTOs.Quiz;
public record CreateQuizRequestDto
{
  public Guid? Id { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public required string Title { get; init; }
  public string? Description { get; init; }
  public string? Instructions { get; init; }
  public string? Code { get; init; }

  public required Guid CourseId { get; init; }
  public required Guid SubjectId { get; init; }
  public required Guid TeacherId { get; init; }
  public Guid? ClassId { get; init; }

  public required int TotalMarks { get; init; }
  public required int PassingMarks { get; init; }
  public required int DurationInMinutes { get; init; }

  public int? MaximumAttempts { get; init; }

  public bool? ShuffleQuestions { get; init; }
  public bool? ShuffleOptions { get; init; }
  public bool? ShowResultImmediately { get; init; }
  public bool? IsPublished { get; init; }

  public required DateTime StartDate { get; init; }
  public required DateTime EndDate { get; init; }
}
