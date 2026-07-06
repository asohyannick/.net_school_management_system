namespace learning_ms.Web.Application.Common.DTOs.Book;
public record CreateBookResponseDto
{
  public required Guid Id { get; init; }
  public required Guid LibraryId { get; init; }

  public required string Title { get; init; }
  public required string Author { get; init; }
  public required string ISBN { get; init; }
  public required string Publisher { get; init; }
  public int? PublicationYear { get; init; }
  public required string Edition { get; init; }

  public required string Category { get; init; }
  public required string Subject { get; init; }
  public required string Language { get; init; }

  public required string ShelfLocation { get; init; }

  public required int TotalCopies { get; init; }
  public required int AvailableCopies { get; init; }

  public decimal? Price { get; init; }

  public required string CoverImageUrl { get; init; }
  public string? Description { get; init; }

  public required bool IsActive { get; init; }

  public required DateTime CreatedAt { get; init; }
}
