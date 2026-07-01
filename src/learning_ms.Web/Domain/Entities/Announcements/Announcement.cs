using learning_ms.Web.Domain.Enums.AnnouncementAudiences;
using learning_ms.Web.Domain.Enums.AnnouncementPriorities;
using learning_ms.Web.Domain.Enums.AnnouncementTypes;

namespace learning_ms.Web.Domain.Entities.Announcements;

public class Announcement
{
  public Guid Id { get; set; }

  public string Title { get; set; } = default!;

  public string Message { get; set; } = default!;

  public string? Summary { get; set; }

  public AnnouncementAudience Audience { get; set; }

  public Guid? CourseId { get; set; }

  public Guid? ClassId { get; set; }

  public Guid? DepartmentId { get; set; }

  public AnnouncementPriority Priority { get; set; }

  public AnnouncementType Type { get; set; }

  public DateTime PublishedAt { get; set; }

  public DateTime? ScheduledAt { get; set; }

  public DateTime? ExpiryDate { get; set; }

  public bool IsPublished { get; set; }

  public bool IsArchived { get; set; }

  public List<string> AttachmentUrls { get; set; } = new();

  public int ViewCount { get; set; }

  public int LikeCount { get; set; }

  public int ShareCount { get; set; }

  public int ReadCount { get; set; }

  public Guid CreatedBy { get; set; }

  public string CreatedByRole { get; set; } = default!;

  public bool RequiresApproval { get; set; }

  public bool IsApproved { get; set; }

  public Guid? ApprovedBy { get; set; }

  public DateTime? ApprovedAt { get; set; }

  public string? RejectionReason { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }
}
