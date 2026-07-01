using learning_ms.Web.Domain.Entities.BookLoans;
using learning_ms.Web.Domain.Entities.Libraries;

namespace learning_ms.Web.Domain.Entities.Books;

public class Book
{
  public Guid Id { get; set; }

  public Guid LibraryId { get; set; }

  public string Title { get; set; } = string.Empty;

  public string Author { get; set; } = string.Empty;

  public string ISBN { get; set; } = string.Empty;

  public string Publisher { get; set; } = string.Empty;

  public int? PublicationYear { get; set; }

  public string Edition { get; set; } = string.Empty;

  public string Category { get; set; } = string.Empty;

  public string Subject { get; set; } = string.Empty;

  public string Language { get; set; } = "English";

  public string ShelfLocation { get; set; } = string.Empty;

  public int TotalCopies { get; set; }

  public int AvailableCopies { get; set; }

  public decimal? Price { get; set; }

  public string CoverImageUrl { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public bool IsActive { get; set; } = true;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime? UpdatedAt { get; set; }

  public Library Library { get; set; } = null!;

  public ICollection<BookLoan> BookLoans { get; set; } = [];
}
