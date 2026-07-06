namespace learning_ms.Web.Application.Common.DTOs.DiscussionForum;
public record CreateDiscussionForumRequestDto
{
  public Guid? Id { get; init; }
  public string? Slug { get; init; }
  public Guid? CreatedBy { get; init; }
  public string? CreatedByRole { get; init; }
  public int? ViewCount { get; init; }
  public int? LikeCount { get; init; }
  public int? ReplyCount { get; init; }
  public bool? IsPinned { get; init; }
  public bool? IsLocked { get; init; }
  public bool? IsApproved { get; init; }
  public bool? IsReported { get; init; }
  public string? ReportReason { get; init; }
  public bool? IsDeleted { get; init; }
  public DateTime? LastReplyAt { get; init; }
  public Guid? LastReplyBy { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public required string Title { get; init; }
  public required string Content { get; init; }

  public Guid? CourseId { get; init; }
  public Guid? ClassId { get; init; }
  public Guid? SubjectId { get; init; }

  // ── File uploads — bound from multipart/form-data, not JSON ──
  public List<IFormFile>? Attachments { get; init; }
}
