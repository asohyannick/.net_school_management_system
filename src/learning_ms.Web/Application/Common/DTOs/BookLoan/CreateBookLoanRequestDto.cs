namespace learning_ms.Web.Application.Common.DTOs.BookLoan;
public record CreateBookLoanRequestDto
{
  public Guid? Id { get; init; }
  public Guid? IssuedBy { get; init; }
  public Guid? ReceivedBy { get; init; }
  public DateOnly? ReturnDate { get; init; }
  public decimal? FineAmount { get; init; }
  public bool? FinePaid { get; init; }
  public string? Status { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }
  public required Guid BookId { get; init; }
  public required Guid LibraryId { get; init; }
  public required Guid StudentId { get; init; }

  public required DateOnly LoanDate { get; init; }
  public required DateOnly DueDate { get; init; }

  public string Remarks { get; init; } = string.Empty;
}
