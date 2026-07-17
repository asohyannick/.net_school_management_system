using learning_ms.Web.Domain.Enums.AttendanceStatus;
namespace learning_ms.Web.Application.Common.DTOs.Attendance;
public record CreateAttendanceResponseDto
{
  public Guid Id { get; init; } = Guid.Empty;
  public Guid StudentId { get; init; } = Guid.Empty;
  public Guid CourseId { get; init; } = Guid.Empty;
  public Guid TutorId { get; init; } = Guid.Empty;
  public Guid? ClassroomId { get; init; } = Guid.Empty;

  public DateOnly AttendanceDate { get; init; } = DateOnly.MinValue;
  public TimeOnly? CheckInTime { get; init; } = TimeOnly.MinValue;
  public TimeOnly? CheckOutTime { get; init; } = TimeOnly.MinValue;

  public AttendanceStatus Status { get; init; } = AttendanceStatus.Present;
  public string? Reason { get; init; } = string.Empty;
  public bool IsExcused { get; init; }
  public bool IsLate { get; init; }
  public int? MinutesLate { get; init; }
  public string? Remarks { get; init; } = string.Empty;

  public Guid RecordedBy { get; init; } = Guid.Empty;
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
