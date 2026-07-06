using learning_ms.Web.Domain.Enums.AnnouncementAudiences;
using learning_ms.Web.Domain.Enums.AnnouncementPriorities;
using learning_ms.Web.Domain.Enums.AnnouncementTypes;
namespace learning_ms.Web.Application.Common.DTOs.Announcements;

public record CreateAnnouncementResponseDto
{
    public required Guid Id { get; init; }
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

    public required bool IsPublished { get; init; }

    public List<string> AttachmentUrls { get; init; } = [];

    public required bool RequiresApproval { get; init; }
    public required bool IsApproved { get; init; }

    public required DateTime CreatedAt { get; init; }
}
