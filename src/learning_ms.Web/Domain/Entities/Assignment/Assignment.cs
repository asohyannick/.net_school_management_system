using learning_ms.Web.Domain.Entities.Courses;
using learning_ms.Web.Domain.Entities.TutorProfiles;

namespace learning_ms.Web.Domain.Entities.Assignment;

public class Assignment
{
  public Guid Id { get; set; }

  public Guid CourseId { get; set; }

  public Guid TutorId { get; set; }

  public string Title { get; set; } = default!;

  public string Description { get; set; } = string.Empty;

  public string? Instructions { get; set; }

  public decimal TotalMarks { get; set; }

  public decimal PassingMarks { get; set; }

  public int AllowedAttempts { get; set; } = 1;

  public int EstimatedCompletionTimeInMinutes { get; set; }

  public DateTime AvailableFrom { get; set; }

  public DateTime DueDate { get; set; }

  public DateTime? CloseDate { get; set; }

  public string? AttachmentUrl { get; set; }

  public string? AttachmentFileName { get; set; }

  public bool IsPublished { get; set; }

  public bool AllowLateSubmission { get; set; }

  public bool IsGroupAssignment { get; set; }

  public bool IsActive { get; set; } = true;

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }

  public Course Course { get; set; } = default!;

  public TutorProfile Tutor { get; set; } = default!;
}
