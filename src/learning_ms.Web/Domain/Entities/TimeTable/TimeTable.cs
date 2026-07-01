namespace learning_ms.Web.Domain.Entities.TimeTable;

public class TimeTable
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public Guid AcademicYearId { get; set; }

  public Guid TermId { get; set; }

  public Guid ClassId { get; set; }

  public Guid SectionId { get; set; }

  public DayOfWeek DayOfWeek { get; set; }

  public TimeOnly StartTime { get; set; }

  public TimeOnly EndTime { get; set; }

  public Guid SubjectId { get; set; }

  public Guid TeacherId { get; set; }

  public Guid RoomId { get; set; }

  public string PeriodName { get; set; } = string.Empty;

  public int PeriodNumber { get; set; }

  public bool IsBreak { get; set; } = false;

  public bool IsActive { get; set; } = true;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime? UpdatedAt { get; set; }
}
