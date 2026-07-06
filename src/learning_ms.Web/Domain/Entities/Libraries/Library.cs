using learning_ms.Web.Domain.Entities.Books;
using learning_ms.Web.Domain.Entities.BookLoans;
namespace learning_ms.Web.Domain.Entities.Libraries;
public class Library
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Code { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public string Location { get; set; } = string.Empty;

  public string LibrarianName { get; set; } = string.Empty;

  public List<string> LibraryImage { get; set; } = [];

  public string Email { get; set; } = string.Empty;

  public string PhoneNumber { get; set; } = string.Empty;

  public int Capacity { get; set; }

  public TimeOnly OpeningTime { get; set; }

  public TimeOnly ClosingTime { get; set; }

  public bool IsActive { get; set; } = true;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime? UpdatedAt { get; set; }

  public ICollection<Book> Books { get; set; } = [];

  public ICollection<BookLoan> BookLoans { get; set; } = [];
}
