namespace learning_ms.Web.Application.Common.DTOs.DiscussionForum;

public record CreateDiscussionForumRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public string? Slug { get; init; } = string.Empty;
  public Guid? CreatedBy { get; init; } = Guid.Empty;
  public string? CreatedByRole { get; init; } = string.Empty;
  public int? ViewCount { get; init; }
  public int? LikeCount { get; init; } 
  public int? ReplyCount { get; init; }
  public bool? IsPinned { get; init; } = false;
  public bool? IsLocked { get; init; } = false;
  public bool? IsApproved { get; init; } = false;
  public bool? IsReported { get; init; } = false;
  public string? ReportReason { get; init; } = string.Empty;
  public bool? IsDeleted { get; init; }
  public DateTime? LastReplyAt { get; init; } = DateTime.UtcNow;
  public Guid? LastReplyBy { get; init; } = Guid.Empty;
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public string Title { get; init; } = string.Empty;
  public string Content { get; init; } = string.Empty;
  public Guid? CourseId { get; init; } = Guid.Empty;
  public Guid? ClassId { get; init; } = Guid.Empty;
  public Guid? SubjectId { get; init; } = Guid.Empty;

  // ── File uploads — bound from multipart/form-data, not JSON ──
  public List<IFormFile>? Attachments { get; init; } = [];
}
