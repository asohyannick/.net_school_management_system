namespace learning_ms.Web.Application.Common.DTOs.Book;
public record CreateBookRequestDto
{
  public Guid? Id { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public required Guid LibraryId { get; init; }

  public required string Title { get; init; }
  public required string Author { get; init; }
  public required string ISBN { get; init; }
  public string? Publisher { get; init; }
  public int? PublicationYear { get; init; }
  public string? Edition { get; init; }
  public string? Category { get; init; }
  public string? Subject { get; init; }
  public string? Language { get; init; }
  public string? ShelfLocation { get; init; }
  public required int TotalCopies { get; init; }
  public int? AvailableCopies { get; init; }
  public decimal? Price { get; init; }
  public string? Description { get; init; }

  // ── File upload — bound from multipart/form-data,
  public IFormFile? CoverImage { get; init; }
}
