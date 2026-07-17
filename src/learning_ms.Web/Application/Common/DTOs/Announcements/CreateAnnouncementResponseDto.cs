using learning_ms.Web.Domain.Enums.AnnouncementAudiences;
using learning_ms.Web.Domain.Enums.AnnouncementPriorities;
using learning_ms.Web.Domain.Enums.AnnouncementTypes;
namespace learning_ms.Web.Application.Common.DTOs.Announcements;
public record CreateAnnouncementResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
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
  public bool IsPublished { get; init; } = false;
  public List<string> AttachmentUrls { get; init; } = [];
  public bool RequiresApproval { get; init; } = false;
  public bool IsApproved { get; init; } = false;
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
