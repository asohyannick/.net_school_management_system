using learning_ms.Web.Domain.Enums.AttendanceStatus;
namespace learning_ms.Web.Application.Common.DTOs.Attendance;
public record CreateAttendanceResponseDto
{
  public required Guid Id { get; init; }
  public required Guid StudentId { get; init; }
  public required Guid CourseId { get; init; }
  public required Guid TutorId { get; init; }
  public Guid? ClassroomId { get; init; }

  public required DateOnly AttendanceDate { get; init; }
  public TimeOnly? CheckInTime { get; init; }
  public TimeOnly? CheckOutTime { get; init; }

  public required AttendanceStatus Status { get; init; }
  public string? Reason { get; init; }
  public required bool IsExcused { get; init; }
  public required bool IsLate { get; init; }
  public int? MinutesLate { get; init; }
  public string? Remarks { get; init; }

  public required Guid RecordedBy { get; init; }
  public required DateTime CreatedAt { get; init; }
}
