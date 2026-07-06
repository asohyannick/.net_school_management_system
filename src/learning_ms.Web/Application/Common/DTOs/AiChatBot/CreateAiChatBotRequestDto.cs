namespace learning_ms.Web.Application.Common.DTOs.AiChatBot;
public record CreateAiChatBotRequestDto
{
  public Guid? Id { get; init; }
  public Guid? UserId { get; init; }
  public string? UserRole { get; init; }
  public string? SessionId { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? StartedAt { get; init; }
  public DateTime? LastMessageAt { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }
  public Guid? CreatedBy { get; init; }
  public Guid? UpdatedBy { get; init; }

  public string ConversationTitle { get; init; } = string.Empty;

  public string ModelName { get; init; } = "gpt-4.1";
  public double Temperature { get; init; } = 0.7;
  public int MaxTokens { get; init; } = 2048;
  public string? SystemPrompt { get; init; }

  public Guid? CourseId { get; init; }
  public Guid? AssignmentId { get; init; }
  public Guid? StudentId { get; init; }
  public Guid? TutorId { get; init; }
}
