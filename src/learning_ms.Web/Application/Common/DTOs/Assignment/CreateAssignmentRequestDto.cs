namespace learning_ms.Web.Application.Common.DTOs.Assignment;
public record CreateAssignmentRequestDto
{
  public Guid? Id { get; init; }
  public Guid? TutorId { get; init; }
  public bool? IsPublished { get; init; }
  public bool? IsActive { get; init; } = true;
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }
  public Guid? CreatedBy { get; init; }
  public Guid? UpdatedBy { get; init; }
  
  public required Guid CourseId { get; init; }

  public required string Title { get; init; }
  public string Description { get; init; } = string.Empty;
  public string? Instructions { get; init; }

  public required decimal TotalMarks { get; init; }
  public required decimal PassingMarks { get; init; }
  public int AllowedAttempts { get; init; } = 1;
  public required int EstimatedCompletionTimeInMinutes { get; init; }

  public required DateTime AvailableFrom { get; init; }
  public required DateTime DueDate { get; init; }
  public DateTime? CloseDate { get; init; }

  public IFormFile? Attachment { get; init; }

  public bool AllowLateSubmission { get; init; }
  public bool IsGroupAssignment { get; init; }
}
