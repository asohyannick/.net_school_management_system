namespace learning_ms.Web.Application.Common.DTOs.AiChatBot;
public record CreateAiChatBotRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public Guid? UserId { get; init; } = Guid.Empty;
  public string? UserRole { get; init; } = string.Empty;
  public string? SessionId { get; init; } = string.Empty;
  public bool? IsActive { get; init; }
  public DateTime? StartedAt { get; init; } = DateTime.UtcNow;
  public DateTime? LastMessageAt { get; init; } = DateTime.UtcNow;
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt {get; init; } = DateTime.UtcNow;
  public Guid? CreatedBy { get; init; } = Guid.Empty;
  public Guid? UpdatedBy { get; init; } = Guid.Empty;
  public string ConversationTitle { get; init; } = string.Empty;
  public string ModelName { get; init; } = "gpt-4.1";
  public double Temperature { get; init; } = 0.7;
  public int MaxTokens { get; init; } = 2048;
  public string? SystemPrompt { get; init; } = string.Empty;
  public Guid? CourseId { get; init; } = Guid.Empty;
  public Guid? AssignmentId { get; init; } = Guid.Empty;
  public Guid? StudentId { get; init; } = Guid.Empty;
  public Guid? TutorId { get; init; } = Guid.Empty;
}
