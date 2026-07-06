namespace learning_ms.Web.Application.Common.DTOs.Library;
public record CreateLibraryRequestDto
{
  public Guid? Id { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public required string Name { get; init; }
  public string? Code { get; init; }
  public string? Description { get; init; }
  public required string Location { get; init; }
  public required string LibrarianName { get; init; }

  public required string Email { get; init; }
  public required string PhoneNumber { get; init; }

  public required int Capacity { get; init; }

  public required TimeOnly OpeningTime { get; init; }
  public required TimeOnly ClosingTime { get; init; }

  // ── File uploads — bound from multipart/form-data, not JSON ──
  public List<IFormFile>? LibraryImages { get; init; }
}
