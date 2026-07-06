using learning_ms.Web.Domain.Entities.QuizOptions;
using learning_ms.Web.Domain.Entities.Quizzes;
using learning_ms.Web.Domain.Enums.QuestionTypes;
namespace learning_ms.Web.Domain.Entities.QuizQuestions;
public class QuizQuestion
{
  public Guid Id { get; set; }

  public Guid QuizId { get; set; }

  public string QuestionText { get; set; } = string.Empty;

  public QuestionType QuestionType { get; set; }

  public decimal Marks { get; set; }

  public int Order { get; set; }

  public bool IsRequired { get; set; } = true;

  public Quiz Quiz { get; set; } = null!;

  public ICollection<QuizOption> Options { get; set; } = [];
}
