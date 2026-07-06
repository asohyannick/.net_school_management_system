using learning_ms.Web.Domain.Enums.AccommodationStatus;
namespace learning_ms.Web.Application.Common.DTOs.StudentAccommodation;
public record CreateStudentAccommodationResponseDto
{
  public required Guid Id { get; init; }

  public required Guid StudentId { get; init; }
  public required Guid AccommodationId { get; init; }

  public required string BedNumber { get; init; }

  public required DateTime CheckInDate { get; init; }
  public DateTime? CheckOutDate { get; init; }
  public DateTime? ExpectedCheckOutDate { get; init; }

  public required decimal AccommodationFee { get; init; }
  public required decimal AmountPaid { get; init; }
  public required decimal Balance { get; init; }

  public required AccommodationStatus Status { get; init; }

  public required bool IsActive { get; init; }

  public string? Remarks { get; init; }

  public required DateTime CreatedAt { get; init; }
}
