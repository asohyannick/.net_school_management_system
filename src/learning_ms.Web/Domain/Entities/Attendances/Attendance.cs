using learning_ms.Web.Domain.Entities.Courses;
using learning_ms.Web.Domain.Entities.TutorProfiles;
using learning_ms.Web.Domain.Enums.AttendanceStatus;
namespace learning_ms.Web.Domain.Entities.Attendances;

public class Attendance
{
  public Guid Id { get; set; }

  public Guid StudentId { get; set; }

  public Guid CourseId { get; set; }

  public Guid TutorId { get; set; }

  public Guid? ClassroomId { get; set; }

  public DateOnly AttendanceDate { get; set; }

  public TimeOnly? CheckInTime { get; set; }

  public TimeOnly? CheckOutTime { get; set; }

  public AttendanceStatus Status { get; set; }

  public string? Reason { get; set; }

  public bool IsExcused { get; set; }

  public bool IsLate { get; set; }

  public int? MinutesLate { get; set; }

  public string? Remarks { get; set; }

  public Guid RecordedBy { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
  public Course Course { get; set; } = default!;

  public TutorProfile Tutor { get; set; } = default!;
}
