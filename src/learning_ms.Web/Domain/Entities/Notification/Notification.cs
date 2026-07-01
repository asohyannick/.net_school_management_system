using learning_ms.Web.Domain.Enums.NotificationPriority;
using learning_ms.Web.Domain.Enums.NotificationStatus;
using learning_ms.Web.Domain.Enums.NotificationType;

namespace learning_ms.Web.Domain.Entities.Notification;

public class Notification
{
  public Guid Id { get; set; }

  public string Title { get; set; } = default!;

  public string Message { get; set; } = default!;

  public NotificationType Type { get; set; }

  public NotificationPriority Priority { get; set; }

  public bool SendEmail { get; set; }

  public bool SendSms { get; set; }

  public bool SendPush { get; set; }

  public bool SendInApp { get; set; } = true;

  public Guid? StudentId { get; set; }

  public Guid? ParentId { get; set; }

  public Guid? StaffId { get; set; }

  public Guid? TutorId { get; set; }

  public string? ExternalEmail { get; set; }

  public string? ExternalPhoneNumber { get; set; }

  public NotificationStatus Status { get; set; }

  public DateTime? SentAt { get; set; }

  public DateTime? ReadAt { get; set; }

  public int RetryCount { get; set; }

  public string? FailureReason { get; set; }

  public string? RelatedEntityType { get; set; } 

  public Guid? RelatedEntityId { get; set; }

  public string? ActionUrl { get; set; }

  public bool IsScheduled { get; set; }

  public DateTime? ScheduledAt { get; set; }

  public bool IsBroadcast { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
