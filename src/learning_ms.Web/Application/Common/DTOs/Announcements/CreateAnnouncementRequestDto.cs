using learning_ms.Web.Domain.Enums.AnnouncementAudiences;
using learning_ms.Web.Domain.Enums.AnnouncementPriorities;
using learning_ms.Web.Domain.Enums.AnnouncementTypes;
namespace learning_ms.Web.Application.Common.DTOs.Announcements;
public record CreateAnnouncementRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public Guid? CreatedBy { get; init; } = Guid.Empty;
  public string? CreatedByRole { get; init; } = string.Empty;
  public DateTime? PublishedAt { get; init; } = DateTime.UtcNow;
  public bool? IsPublished { get; init; } = false;
  public bool? IsArchived { get; init; } = false;
  public int? ViewCount { get; init; }
  public int? LikeCount { get; init; }
  public int? ShareCount { get; init; }
  public int? ReadCount { get; init; }
  public bool? IsApproved { get; init; } = false;
  public Guid? ApprovedBy { get; init; } = Guid.Empty;
  public DateTime? ApprovedAt { get; init; } = DateTime.UtcNow;
  public string? RejectionReason { get; init; } = string.Empty;
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public string Title { get; init; } = string.Empty;
  public string Message { get; init; } = string.Empty;
  public string? Summary { get; init; } = string.Empty;
  public AnnouncementAudience Audience { get; init; } = AnnouncementAudience.All;
  public Guid? CourseId { get; init; } = Guid.Empty;
  public Guid? ClassId { get; init; } = Guid.Empty;
  public Guid? DepartmentId { get; init; } = Guid.Empty;
  public AnnouncementPriority Priority { get; init; } = AnnouncementPriority.High;
  public AnnouncementType Type { get; init; } = AnnouncementType.General;
  public DateTime? ScheduledAt { get; init; } = DateTime.UtcNow;
  public DateTime? ExpiryDate { get; init; } = DateTime.UtcNow;
  public List<IFormFile>? Attachments { get; init; } = [];
  public bool RequiresApproval { get; init; }
}
