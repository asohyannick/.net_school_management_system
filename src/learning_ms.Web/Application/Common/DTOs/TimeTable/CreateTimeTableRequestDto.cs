namespace learning_ms.Web.Application.Common.DTOs.TimeTable;
public record CreateTimeTableRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public string Name { get; init; } = string.Empty;
  public string? Description { get; init; } = string.Empty;
  public Guid AcademicYearId { get; init; } = Guid.Empty;
  public Guid TermId { get; init; } = Guid.Empty;
  public Guid ClassId { get; init; } = Guid.Empty;
  public Guid SectionId { get; init; } = Guid.Empty;
  public DayOfWeek DayOfWeek { get; init; } = DayOfWeek.Monday;
  public TimeOnly StartTime { get; init; } = TimeOnly.MinValue;
  public TimeOnly EndTime { get; init; } = TimeOnly.MinValue;
  public Guid SubjectId { get; init; } = Guid.Empty;
  public Guid TeacherId { get; init; } = Guid.Empty;
  public Guid RoomId { get; init; } = Guid.Empty;
  public string PeriodName { get; init; } = string.Empty;
  public int PeriodNumber { get; init; }
  public bool? IsBreak { get; init; }
}
