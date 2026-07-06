namespace learning_ms.Web.Application.Common.DTOs.BookLoan;
public record CreateBookLoanResponseDto
{
  public required Guid Id { get; init; }
  public required Guid BookId { get; init; }
  public required Guid LibraryId { get; init; }
  public required Guid StudentId { get; init; }

  public required DateOnly LoanDate { get; init; }
  public required DateOnly DueDate { get; init; }
  public DateOnly? ReturnDate { get; init; }

  public required decimal FineAmount { get; init; }
  public required bool FinePaid { get; init; }
  public required string Status { get; init; }
  public string Remarks { get; init; } = string.Empty;

  public Guid? IssuedBy { get; init; }

  public required DateTime CreatedAt { get; init; }
}
