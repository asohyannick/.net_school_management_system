namespace learning_ms.Web.Application.Common.DTOs.Assignment;
public record CreateAssignmentResponseDto
{
  public required Guid Id { get; init; }
  public required Guid CourseId { get; init; }
  public required Guid TutorId { get; init; }

  public required string Title { get; init; }
  public string Description { get; init; } = string.Empty;
  public string? Instructions { get; init; }

  public required decimal TotalMarks { get; init; }
  public required decimal PassingMarks { get; init; }
  public required int AllowedAttempts { get; init; }
  public required int EstimatedCompletionTimeInMinutes { get; init; }

  public required DateTime AvailableFrom { get; init; }
  public required DateTime DueDate { get; init; }
  public DateTime? CloseDate { get; init; }

  public string? AttachmentUrl { get; init; }
  public string? AttachmentFileName { get; init; }

  public required bool IsPublished { get; init; }
  public required bool AllowLateSubmission { get; init; }
  public required bool IsGroupAssignment { get; init; }
  public required bool IsActive { get; init; }

  public required DateTime CreatedAt { get; init; }
}
