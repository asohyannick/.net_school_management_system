namespace learning_ms.Web.Application.Common.DTOs.Library;
public record CreateLibraryRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public bool? IsActive { get; init; } = false;
  public DateTime? CreatedAt { get; init; } = DateTime.Now;
  public DateTime? UpdatedAt { get; init; } = DateTime.Now;
  public string Name { get; init; } = string.Empty;
  public string? Code { get; init; } = string.Empty;
  public string? Description { get; init; } = string.Empty;
  public string Location { get; init; } = string.Empty;
  public string LibrarianName { get; init; } = string.Empty;
  public string Email { get; init; } = string.Empty;
  public string PhoneNumber { get; init; } = string.Empty;
  public int Capacity { get; init; }
  public TimeOnly OpeningTime { get; init; } = TimeOnly.MinValue;
  public TimeOnly ClosingTime { get; init; } = TimeOnly.MinValue;

  // ── File uploads — bound from multipart/form-data, not JSON ──
  public List<IFormFile>? LibraryImages { get; init; }
}
