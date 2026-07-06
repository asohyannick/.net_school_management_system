namespace learning_ms.Web.Application.Common.DTOs.Accommodations;

public record CreateAccommodationRequestDto
{
  public Guid? Id { get; init; }
  public int? OccupiedBeds { get; init; }
  public bool? IsAvailable { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public List<IFormFile>? HostelImages { get; init; }

  public required string Name { get; init; }
  public required string Code { get; init; }
  public string Description { get; init; } = string.Empty;

  public required string Building { get; init; }
  public string Block { get; init; } = string.Empty;
  public string Floor { get; init; } = string.Empty;
  public required string RoomNumber { get; init; }

  public required int Capacity { get; init; }

  public required decimal Fee { get; init; }
  public string Currency { get; init; } = "USD";

  public bool HasWiFi { get; init; }
  public bool HasAirConditioning { get; init; }
  public bool HasPrivateBathroom { get; init; }
  public bool HasStudyDesk { get; init; }
  public bool HasWardrobe { get; init; }

  public Guid? WardenId { get; init; }

  public string Remarks { get; init; } = string.Empty;
}
