namespace learning_ms.Web.Application.Common.DTOs.AiChatBot;
public record CreateAiChatBotResponseDto
{
  public required Guid Id { get; init; }
  public required Guid UserId { get; init; }
  public required string SessionId { get; init; }
  public required string ConversationTitle { get; init; }

  public required string ModelName { get; init; }
  public required double Temperature { get; init; }
  public required int MaxTokens { get; init; }

  public Guid? CourseId { get; init; }
  public Guid? AssignmentId { get; init; }
  public Guid? StudentId { get; init; }
  public Guid? TutorId { get; init; }

  public required bool IsActive { get; init; }
  public required DateTime StartedAt { get; init; }
  public required DateTime CreatedAt { get; init; }
}
