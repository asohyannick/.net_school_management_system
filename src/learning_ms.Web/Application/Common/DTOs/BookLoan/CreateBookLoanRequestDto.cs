namespace learning_ms.Web.Application.Common.DTOs.BookLoan;
public record CreateBookLoanRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public Guid? IssuedBy { get; init; } = Guid.Empty;
  public Guid? ReceivedBy { get; init; } = Guid.Empty;
  public DateOnly? ReturnDate { get; init; } = DateOnly.MinValue;
  public decimal? FineAmount { get; init; } = decimal.Zero;
  public bool? FinePaid { get; init; }
  public string? Status { get; init; } = string.Empty;
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;

  public Guid BookId { get; init; } = Guid.Empty;
  public Guid LibraryId { get; init; } = Guid.Empty;
  public Guid StudentId { get; init; } = Guid.Empty;

  public DateOnly LoanDate { get; init; } = DateOnly.MinValue;
  public DateOnly DueDate { get; init; } = DateOnly.MinValue;

  public string Remarks { get; init; } = string.Empty;
}
