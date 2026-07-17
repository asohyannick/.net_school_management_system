namespace learning_ms.Web.Application.Common.DTOs.Accommodations;
public record CreateAccommodationRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public int? OccupiedBeds { get; init; }
  public bool? IsAvailable { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public List<IFormFile>? HostelImages { get; init; } = [];
  public string Name { get; init; } = string.Empty;
  public string Code { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;
  public string Building { get; init; } = string.Empty;
  public string Block { get; init; } = string.Empty;
  public string Floor { get; init; } = string.Empty;
  public string RoomNumber { get; init; } = string.Empty;
  public int Capacity { get; init; }
  public decimal Fee { get; init; } = decimal.Zero;
  public string Currency { get; init; } = "USD";
  public bool HasWiFi { get; init; }
  public bool HasAirConditioning { get; init; }
  public bool HasPrivateBathroom { get; init; }
  public bool HasStudyDesk { get; init; }
  public bool HasWardrobe { get; init; }
  public Guid? WardenId { get; init; } = Guid.Empty;
  public string Remarks { get; init; } = string.Empty;
}
