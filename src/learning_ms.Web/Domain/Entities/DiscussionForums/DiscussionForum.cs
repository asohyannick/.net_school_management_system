
namespace learning_ms.Web.Domain.Entities.DiscussionForums;

public class DiscussionForum
{
  public Guid Id { get; set; }

  public string Title { get; set; } = default!;

  public string Content { get; set; } = default!;

  public string? Slug { get; set; }

  public Guid CreatedBy { get; set; }

  public string CreatedByRole { get; set; } = default!; 

  public Guid? CourseId { get; set; }

  public Guid? ClassId { get; set; }

  public Guid? SubjectId { get; set; }

  public int ViewCount { get; set; }

  public int LikeCount { get; set; }

  public int ReplyCount { get; set; }

  public bool IsPinned { get; set; }

  public bool IsLocked { get; set; }

  public bool IsApproved { get; set; }

  public bool IsReported { get; set; }

  public string? ReportReason { get; set; }

  public bool IsDeleted { get; set; }

  public List<string> AttachmentUrls { get; set; } = new();

  public DateTime? LastReplyAt { get; set; }

  public Guid? LastReplyBy { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }
}
