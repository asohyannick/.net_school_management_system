namespace learning_ms.Web.Application.Common.DTOs.DiscussionForum;
public record CreateDiscussionForumResponseDto
{
  public required Guid Id { get; init; }
  public required string Title { get; init; }
  public required string Content { get; init; }
  public string? Slug { get; init; }

  public required Guid CreatedBy { get; init; }
  public required string CreatedByRole { get; init; }

  public Guid? CourseId { get; init; }
  public Guid? ClassId { get; init; }
  public Guid? SubjectId { get; init; }

  public required bool IsPinned { get; init; }
  public required bool IsLocked { get; init; }
  public required bool IsApproved { get; init; }

  public List<string> AttachmentUrls { get; init; } = [];

  public required DateTime CreatedAt { get; init; }
}
