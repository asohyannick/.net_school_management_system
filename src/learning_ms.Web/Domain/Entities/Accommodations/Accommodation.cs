using learning_ms.Web.Domain.Entities.StudentAccommodations;
namespace learning_ms.Web.Domain.Entities.Accommodations;

public class Accommodation
{
  public Guid Id { get; set; }
  
  public List<string> HostelImage { get; set; } = [];

  public string Name { get; set; } = string.Empty;

  public string Code { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public string Building { get; set; } = string.Empty;

  public string Block { get; set; } = string.Empty;

  public string Floor { get; set; } = string.Empty;

  public string RoomNumber { get; set; } = string.Empty;

  public int Capacity { get; set; }

  public int OccupiedBeds { get; set; }

  public int AvailableBeds => Capacity - OccupiedBeds;

  public decimal Fee { get; set; }

  public string Currency { get; set; } = "USD";

  public bool HasWiFi { get; set; }

  public bool HasAirConditioning { get; set; }

  public bool HasPrivateBathroom { get; set; }

  public bool HasStudyDesk { get; set; }

  public bool HasWardrobe { get; set; }

  public Guid? WardenId { get; set; }

  public bool IsAvailable { get; set; } = true;

  public bool IsActive { get; set; } = true;

  public string Remarks { get; set; } = string.Empty;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime? UpdatedAt { get; set; }

  public ICollection<StudentAccommodation> StudentAccommodations { get; set; } = [];
}
