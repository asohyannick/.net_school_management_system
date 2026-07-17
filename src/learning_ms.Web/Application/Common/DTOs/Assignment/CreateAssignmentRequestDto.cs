namespace learning_ms.Web.Application.Common.DTOs.Assignment;
public record CreateAssignmentRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public Guid? TutorId { get; init; } = Guid.Empty;
  public bool? IsPublished { get; init; }
  public bool? IsActive { get; init; } = true;
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public Guid? CreatedBy { get; init; } = Guid.Empty;
  public Guid? UpdatedBy { get; init; } = Guid.Empty;
  public Guid CourseId { get; init; } = Guid.Empty;
  public string Title { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;
  public string? Instructions { get; init; } = string.Empty;
  public decimal TotalMarks { get; init; } = decimal.Zero;
  public decimal PassingMarks { get; init; } = decimal.Zero;
  public int AllowedAttempts { get; init; } = 1;
  public int EstimatedCompletionTimeInMinutes { get; init; }
  public DateTime AvailableFrom { get; init; } = DateTime.MinValue;
  public DateTime DueDate { get; init; } = DateTime.MinValue;
  public DateTime? CloseDate { get; init; } = DateTime.UtcNow;
  public IFormFile? Attachment { get; init; }
  public bool AllowLateSubmission { get; init; }
  public bool IsGroupAssignment { get; init; }
}
