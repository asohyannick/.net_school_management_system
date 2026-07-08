namespace learning_ms.Web.Application.Common.DTOs.TimeTable;
public record CreateTimeTableRequestDto
{
  public Guid? Id { get; init; }
  public bool? IsActive { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }
  public required string Name { get; init; }
  public string? Description { get; init; }
  public required Guid AcademicYearId { get; init; }
  public required Guid TermId { get; init; }
  public required Guid ClassId { get; init; }
  public required Guid SectionId { get; init; }
  public required DayOfWeek DayOfWeek { get; init; }
  public required TimeOnly StartTime { get; init; }
  public required TimeOnly EndTime { get; init; }
  public required Guid SubjectId { get; init; }
  public required Guid TeacherId { get; init; }
  public required Guid RoomId { get; init; }
  public required string PeriodName { get; init; }
  public required int PeriodNumber { get; init; }
  public bool? IsBreak { get; init; }
}
