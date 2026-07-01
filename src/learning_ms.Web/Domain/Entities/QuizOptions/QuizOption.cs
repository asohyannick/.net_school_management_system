using learning_ms.Web.Domain.Entities.QuizQuestions;
namespace learning_ms.Web.Domain.Entities.QuizOptions;

public class QuizOption
{
  public Guid Id { get; set; }

  public Guid QuizQuestionId { get; set; }

  public string OptionText { get; set; } = string.Empty;

  public bool IsCorrect { get; set; }

  public QuizQuestion QuizQuestion { get; set; } = null!;
}