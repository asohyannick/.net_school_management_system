namespace learning_ms.Web.Application.Common.DTOs.Accommodations;
public record CreateAccommodationResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
  public List<string> HostelImageUrls { get; init; } = [];
  public string Name { get; init; } = string.Empty;
  public string Code { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;
  public string Building { get; init; } = string.Empty;
  public string Block { get; init; } = string.Empty;
  public string Floor { get; init; } = string.Empty;
  public string RoomNumber { get; init; } = string.Empty;
  public int Capacity { get; init; }
  public int OccupiedBeds { get; init; }
  public int AvailableBeds { get; init; }
  public decimal Fee { get; init; } = decimal.Zero;
  public string Currency { get; init; } = "USD";
  public bool HasWiFi { get; init; }
  public bool HasAirConditioning { get; init; }
  public bool HasPrivateBathroom { get; init; }
  public bool HasStudyDesk { get; init; }
  public bool HasWardrobe { get; init; }
  public Guid? WardenId { get; init; } = Guid.Empty;
  public bool IsAvailable { get; init; }
  public bool IsActive { get; init; }
  public string Remarks { get; init; } = string.Empty;
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
