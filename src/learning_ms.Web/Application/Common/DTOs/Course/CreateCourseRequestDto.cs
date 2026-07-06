namespace learning_ms.Web.Application.Common.DTOs.Course;

public record CreateCourseRequestDto
{
  public Guid? Id { get; init; }
  public bool? IsActive { get; init; } = true;
  public bool? IsPublished { get; init; }
  public DateTime? CreatedAt { get; init; }
  public DateTime? UpdatedAt { get; init; }
  public Guid? CreatedBy { get; init; }
  public Guid? UpdatedBy { get; init; }

  public required string CourseCode { get; init; }
  public required string CourseTitle { get; init; }
  public string? ShortName { get; init; }
  public string? Description { get; init; }

  public IFormFile? CourseImage { get; init; }

  public required int CreditHours { get; init; }
  public required int TotalLessons { get; init; }
  public required int DurationInWeeks { get; init; }

  public required string AcademicYear { get; init; }
  public required string Semester { get; init; }
  public required string Level { get; init; }

  public required int MaximumStudents { get; init; }
  public required int MinimumStudents { get; init; }
  public required decimal CourseFee { get; init; }

  public required DateOnly StartDate { get; init; }
  public required DateOnly EndDate { get; init; }

  public string? Language { get; init; }
  public string? Syllabus { get; init; }
  public string? LearningObjectives { get; init; }
  public string? Prerequisites { get; init; }
}
