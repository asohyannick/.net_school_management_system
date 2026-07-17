using learning_ms.Web.Domain.Enums.AccommodationStatus;
namespace learning_ms.Web.Application.Common.DTOs.StudentAccommodation;
public record CreateStudentAccommodationResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
  public Guid StudentId { get; init; } = Guid.Empty;
  public Guid AccommodationId { get; init; } = Guid.Empty;
  public string BedNumber { get; init; } = string.Empty;
  public DateTime CheckInDate { get; init; } = DateTime.MinValue;
  public DateTime? CheckOutDate { get; init; } = DateTime.UtcNow;
  public DateTime? ExpectedCheckOutDate { get; init; } = DateTime.UtcNow;
  public decimal AccommodationFee { get; init; } = decimal.Zero;
  public decimal AmountPaid { get; init; } = decimal.Zero;
  public decimal Balance { get; init; } = decimal.Zero;
  public AccommodationStatus Status { get; init; } = AccommodationStatus.Active;
  public bool IsActive { get; init; }
  public string? Remarks { get; init; } = string.Empty;
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
