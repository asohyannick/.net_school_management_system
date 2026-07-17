namespace learning_ms.Web.Application.Common.DTOs.Quiz;
public record CreateQuizRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public bool? IsActive { get; init; } = false;
  public DateTime? CreatedAt { get; init; } = DateTime.Now;
  public DateTime? UpdatedAt { get; init; } = DateTime.Now;
  public string Title { get; init; } = string.Empty;
  public string? Description { get; init; } = string.Empty;
  public string? Instructions { get; init; } = string.Empty;  
  public string? Code { get; init; } = string.Empty;
  public Guid CourseId { get; init; } = Guid.Empty;
  public  Guid SubjectId { get; init; } = Guid.Empty;
  public Guid TeacherId { get; init; } = Guid.Empty;
  public Guid? ClassId { get; init; } = Guid.Empty;
  public  int TotalMarks { get; init; }
  public int PassingMarks { get; init; }
  public int DurationInMinutes { get; init; }
  public int? MaximumAttempts { get; init; }
  public bool? ShuffleQuestions { get; init; } = false;
  public bool? ShuffleOptions { get; init; } = false;
  public bool? ShowResultImmediately { get; init; }  = false;
  public bool? IsPublished { get; init; } = false;
  public DateTime StartDate { get; init; } = DateTime.Now;
  public DateTime EndDate { get; init; } = DateTime.UtcNow;
}
