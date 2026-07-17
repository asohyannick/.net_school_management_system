namespace learning_ms.Web.Application.Common.DTOs.Library;
public record CreateLibraryResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
  public string Name { get; init; } = string.Empty;
  public string Code { get; init; } = string.Empty;
  public string? Description { get; init; } = string.Empty;
  public string Location { get; init; } = string.Empty;
  public string LibrarianName { get; init; } = string.Empty;
  public List<string> LibraryImage { get; init; } = [];
  public string Email { get; init; } = string.Empty;
  public string PhoneNumber { get; init; } = string.Empty;
  public int Capacity { get; init; }
  public TimeOnly OpeningTime { get; init; } =  TimeOnly.MinValue;
  public TimeOnly ClosingTime { get; init; } = TimeOnly.MinValue;
  public bool IsActive { get; init; } = false;
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
