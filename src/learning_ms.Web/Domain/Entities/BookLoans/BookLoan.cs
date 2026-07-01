using learning_ms.Web.Domain.Entities.Books;
using learning_ms.Web.Domain.Entities.Libraries;
using learning_ms.Web.Domain.Enums.BookLoanStatus;

namespace learning_ms.Web.Domain.Entities.BookLoans;

public class BookLoan
{
  public Guid Id { get; set; }

  public Guid BookId { get; set; }

  public Guid LibraryId { get; set; }

  public Guid StudentId { get; set; }

  public DateOnly LoanDate { get; set; }

  public DateOnly DueDate { get; set; }

  public DateOnly? ReturnDate { get; set; }

  public decimal FineAmount { get; set; }

  public bool FinePaid { get; set; }

  public BookLoanStatus Status { get; set; } = BookLoanStatus.Borrowed;

  public string Remarks { get; set; } = string.Empty;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime? UpdatedAt { get; set; }

  public Guid? IssuedBy { get; set; }

  public Guid? ReceivedBy { get; set; }

  public Book Book { get; set; } = null!;

  public Library Library { get; set; } = null!;
}

