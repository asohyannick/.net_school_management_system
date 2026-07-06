namespace learning_ms.Web.Application.Common.DTOs.Accommodations;

public record CreateAccommodationResponseDto
{
  public required Guid Id { get; init; }

  public List<string> HostelImageUrls { get; init; } = [];

  public required string Name { get; init; }
  public required string Code { get; init; }
  public string Description { get; init; } = string.Empty;

  public required string Building { get; init; }
  public string Block { get; init; } = string.Empty;
  public string Floor { get; init; } = string.Empty;
  public required string RoomNumber { get; init; }

  public required int Capacity { get; init; }
  public required int OccupiedBeds { get; init; }
  public required int AvailableBeds { get; init; }

  public required decimal Fee { get; init; }
  public required string Currency { get; init; }

  public bool HasWiFi { get; init; }
  public bool HasAirConditioning { get; init; }
  public bool HasPrivateBathroom { get; init; }
  public bool HasStudyDesk { get; init; }
  public bool HasWardrobe { get; init; }

  public Guid? WardenId { get; init; }

  public required bool IsAvailable { get; init; }
  public required bool IsActive { get; init; }

  public string Remarks { get; init; } = string.Empty;

  public required DateTime CreatedAt { get; init; }
}
