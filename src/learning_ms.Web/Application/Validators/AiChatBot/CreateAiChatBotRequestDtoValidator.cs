using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.AiChatBot;
namespace learning_ms.Web.Application.Validators.AiChatBot;
public class CreateAiChatBotRequestDtoValidator : AbstractValidator<CreateAiChatBotRequestDto>
{
    private static readonly string[] AllowedModels = ["gpt-4.1", "gpt-4.1-mini", "gpt-5", "claude-sonnet-5"];

    public CreateAiChatBotRequestDtoValidator()
    {
        RuleFor(x => x.ConversationTitle)
            .MaximumLength(200).WithMessage("Conversation title must not exceed 200 characters.");

        RuleFor(x => x.ModelName)
            .NotEmpty().WithMessage("Model name is required.")
            .Must(model => AllowedModels.Contains(model))
            .WithMessage($"Model must be one of: {string.Join(", ", AllowedModels)}.");

        RuleFor(x => x.Temperature)
            .InclusiveBetween(0.0, 2.0)
            .WithMessage("Temperature must be between 0 and 2.");

        RuleFor(x => x.MaxTokens)
            .GreaterThan(0).WithMessage("Max tokens must be greater than 0.")
            .LessThanOrEqualTo(32000).WithMessage("Max tokens must not exceed 32,000.");

        RuleFor(x => x.SystemPrompt)
            .MaximumLength(4000).WithMessage("System prompt must not exceed 4,000 characters.")
            .When(x => !string.IsNullOrEmpty(x.SystemPrompt));

        RuleFor(x => x.CourseId)
            .NotEqual(Guid.Empty).WithMessage("Course ID must be a valid identifier.")
            .When(x => x.CourseId.HasValue);

        RuleFor(x => x.AssignmentId)
            .NotEqual(Guid.Empty).WithMessage("Assignment ID must be a valid identifier.")
            .When(x => x.AssignmentId.HasValue);

        RuleFor(x => x.StudentId)
            .NotEqual(Guid.Empty).WithMessage("Student ID must be a valid identifier.")
            .When(x => x.StudentId.HasValue);

        RuleFor(x => x.TutorId)
            .NotEqual(Guid.Empty).WithMessage("Tutor ID must be a valid identifier.")
            .When(x => x.TutorId.HasValue);
    }
}
