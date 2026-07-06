using learning_ms.Web.Domain.Enums.AnnouncementAudiences;
using learning_ms.Web.Domain.Enums.AnnouncementPriorities;
using learning_ms.Web.Domain.Enums.AnnouncementTypes;
namespace learning_ms.Web.Application.Common.DTOs.Announcements;
public record CreateAnnouncementRequestDto
{
    public Guid? Id { get; init; }
    public Guid? CreatedBy { get; init; }
    public string? CreatedByRole { get; init; }
    public DateTime? PublishedAt { get; init; }
    public bool? IsPublished { get; init; }
    public bool? IsArchived { get; init; }
    public int? ViewCount { get; init; }
    public int? LikeCount { get; init; }
    public int? ShareCount { get; init; }
    public int? ReadCount { get; init; }
    public bool? IsApproved { get; init; }
    public Guid? ApprovedBy { get; init; }
    public DateTime? ApprovedAt { get; init; }
    public string? RejectionReason { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public required string Title { get; init; }
    public required string Message { get; init; }
    public string? Summary { get; init; }

    public required AnnouncementAudience Audience { get; init; }
    public Guid? CourseId { get; init; }
    public Guid? ClassId { get; init; }
    public Guid? DepartmentId { get; init; }

    public required AnnouncementPriority Priority { get; init; }
    public required AnnouncementType Type { get; init; }

    public DateTime? ScheduledAt { get; init; }
    public DateTime? ExpiryDate { get; init; }

    public List<IFormFile>? Attachments { get; init; }

    public bool RequiresApproval { get; init; }
}
