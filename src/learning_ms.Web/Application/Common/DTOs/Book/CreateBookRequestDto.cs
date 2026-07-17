namespace learning_ms.Web.Application.Common.DTOs.Book;
public record CreateBookRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;

  public Guid LibraryId { get; init; } = Guid.Empty;

  public string Title { get; init; } = string.Empty;
  public string Author { get; init; } = string.Empty;
  public string ISBN { get; init; } = string.Empty;
  public string? Publisher { get; init; } = string.Empty;
  public int? PublicationYear { get; init; }
  public string? Edition { get; init; } = string.Empty;
  public string? Category { get; init; } = string.Empty;
  public string? Subject { get; init; } = string.Empty;
  public string? Language { get; init; } = string.Empty;
  public string? ShelfLocation { get; init; } = string.Empty;
  public int TotalCopies { get; init; }
  public int? AvailableCopies { get; init; }
  public decimal? Price { get; init; } = decimal.Zero;
  public string? Description { get; init; } = string.Empty;

  // ── File upload — bound from multipart/form-data
  public IFormFile? CoverImage { get; init; }
}
