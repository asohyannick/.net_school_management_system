namespace learning_ms.Web.Application.Common.DTOs.DiscussionForum;
public record CreateDiscussionForumResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
  public  string Title { get; init; } = string.Empty;
  public string Content { get; init; } = string.Empty;
  public string? Slug { get; init; } = string.Empty;
  public required Guid CreatedBy { get; init; }
  public required string CreatedByRole { get; init; }
  public Guid? CourseId { get; init; } = Guid.Empty;
  public Guid? ClassId { get; init; } =  Guid.Empty;
  public Guid? SubjectId { get; init; } = Guid.Empty;
  public bool IsPinned { get; init; } = false;
  public bool IsLocked { get; init; } = false;
  public bool IsApproved { get; init; } = false;
  public List<string> AttachmentUrls { get; init; } = [];
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
