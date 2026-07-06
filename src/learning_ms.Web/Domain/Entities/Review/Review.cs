namespace learning_ms.Web.Domain.Entities.Review;
public class Review
{
  public Guid Id { get; set; }

  public Guid StudentId { get; set; }

  public Guid? CourseId { get; set; }

  public Guid? TutorId { get; set; }

  public string Title { get; set; } = default!;

  public string Message { get; set; } = default!;

  public int Rating { get; set; }

  public string? StudentName { get; set; }

  public string? ProfileImageUrl { get; set; }

  public bool IsApproved { get; set; }

  public bool IsFeatured { get; set; }

  public bool IsAnonymous { get; set; }

  public bool IsEdited { get; set; }

  public bool IsDeleted { get; set; }

  public string? AdminReply { get; set; }

  public DateTime? AdminReplyDate { get; set; }

  public int HelpfulCount { get; set; }

  public int ReportCount { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
