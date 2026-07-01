namespace learning_ms.Web.Domain.Entities.AiChatBot;

public class AiChatBot
{
  public Guid Id { get; set; }

  public Guid UserId { get; set; }

  public string UserRole { get; set; } = default!;

  public string SessionId { get; set; } = default!;

  public string ConversationTitle { get; set; } = string.Empty;

  public string? LastUserMessage { get; set; }

  public string? LastBotResponse { get; set; }

  public string ModelName { get; set; } = "gpt-4.1";

  public double Temperature { get; set; }

  public int MaxTokens { get; set; }

  public string? SystemPrompt { get; set; }

  public Guid? CourseId { get; set; }

  public Guid? AssignmentId { get; set; }

  public Guid? StudentId { get; set; }

  public Guid? TutorId { get; set; }

  public int PromptTokens { get; set; }

  public int CompletionTokens { get; set; }

  public decimal EstimatedCost { get; set; }

  public bool IsActive { get; set; } = true;

  public bool IsArchived { get; set; }

  public bool IsPinned { get; set; }

  public bool IsShared { get; set; }

  public bool ContainsSensitiveContent { get; set; }

  public bool FlaggedForReview { get; set; }

  public string? ModerationReason { get; set; }

  public int UserRating { get; set; }

  public string? UserFeedback { get; set; }

  public DateTime StartedAt { get; set; }

  public DateTime? EndedAt { get; set; }

  public DateTime LastMessageAt { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
