namespace learning_ms.Web.Application.Common.DTOs.Library;
public record CreateLibraryResponseDto
{
  public required Guid Id { get; init; }

  public required string Name { get; init; }
  public required string Code { get; init; }
  public string? Description { get; init; }
  public required string Location { get; init; }
  public required string LibrarianName { get; init; }

  public List<string> LibraryImage { get; init; } = [];

  public required string Email { get; init; }
  public required string PhoneNumber { get; init; }

  public required int Capacity { get; init; }

  public required TimeOnly OpeningTime { get; init; }
  public required TimeOnly ClosingTime { get; init; }

  public required bool IsActive { get; init; }

  public required DateTime CreatedAt { get; init; }
}
