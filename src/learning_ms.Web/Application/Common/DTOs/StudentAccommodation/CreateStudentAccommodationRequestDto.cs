using learning_ms.Web.Domain.Enums.AccommodationStatus;
namespace learning_ms.Web.Application.Common.DTOs.StudentAccommodation;
public record CreateStudentAccommodationRequestDto
{
  public Guid? Id { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }

  public required Guid StudentId { get; init; }
  public required Guid AccommodationId { get; init; }

  public required string BedNumber { get; init; }

  public required DateTime CheckInDate { get; init; }
  public DateTime? CheckOutDate { get; init; }
  public DateTime? ExpectedCheckOutDate { get; init; }

  public required decimal AccommodationFee { get; init; }
  public decimal? AmountPaid { get; init; }

  public AccommodationStatus? Status { get; init; }

  public string? Remarks { get; init; }
}
