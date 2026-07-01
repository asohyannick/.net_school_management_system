using System;
using learning_ms.Web.Domain.Enums.AccommodationStatus;
using learning_ms.Web.Domain.Entities.Accommodations;

namespace learning_ms.Web.Domain.Entities.StudentAccommodations;

public class StudentAccommodation
{
  public Guid Id { get; set; }

  public Guid StudentId { get; set; }

  public Guid AccommodationId { get; set; }

  public string BedNumber { get; set; } = string.Empty;

  public DateTime CheckInDate { get; set; }

  public DateTime? CheckOutDate { get; set; }

  public DateTime? ExpectedCheckOutDate { get; set; }

  public decimal AccommodationFee { get; set; }

  public decimal AmountPaid { get; set; }

  public decimal Balance => AccommodationFee - AmountPaid;

  public AccommodationStatus Status { get; set; } = AccommodationStatus.Active;

  public bool IsActive { get; set; } = true;

  public string Remarks { get; set; } = string.Empty;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime? UpdatedAt { get; set; }

  public Accommodation Accommodation { get; set; } = null!; 
}