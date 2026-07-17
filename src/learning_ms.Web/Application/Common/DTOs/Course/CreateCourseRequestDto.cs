namespace learning_ms.Web.Application.Common.DTOs.Course;

public record CreateCourseRequestDto
{
  public Guid? Id { get; init; } = Guid.Empty;
  public bool? IsActive { get; init; } = true;
  public bool? IsPublished { get; init; }
  public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
  public Guid? CreatedBy { get; init; } = Guid.Empty;
  public Guid? UpdatedBy { get; init; } = Guid.Empty;

  public string CourseCode { get; init; } = string.Empty;
  public string CourseTitle { get; init; } = string.Empty;
  public string? ShortName { get; init; } = string.Empty;
  public string? Description { get; init; } = string.Empty;

  public IFormFile? CourseImage { get; init; }

  public int CreditHours { get; init; }
  public int TotalLessons { get; init; }
  public int DurationInWeeks { get; init; }

  public string AcademicYear { get; init; } = string.Empty;
  public string Semester { get; init; } = string.Empty;
  public string Level { get; init; } = string.Empty;

  public int MaximumStudents { get; init; }
  public int MinimumStudents { get; init; }
  public decimal CourseFee { get; init; } = decimal.Zero;

  public DateOnly StartDate { get; init; } = DateOnly.MinValue;
  public DateOnly EndDate { get; init; } = DateOnly.MinValue;

  public string? Language { get; init; } = string.Empty;
  public string? Syllabus { get; init; } = string.Empty;
  public string? LearningObjectives { get; init; } = string.Empty;
  public string? Prerequisites { get; init; } = string.Empty;
}
