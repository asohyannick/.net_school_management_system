namespace learning_ms.Web.Domain.Entities.Courses;
public class Course
{
  public Guid Id { get; set; }

  public string CourseCode { get; set; } = default!;
  public string CourseTitle { get; set; } = default!;
  public string? ShortName { get; set; }

  public string? Description { get; set; }

  public string? CourseImageUrl { get; set; }

  public int CreditHours { get; set; }

  public int TotalLessons { get; set; }

  public int DurationInWeeks { get; set; }

  public string AcademicYear { get; set; } = default!;

  public string Semester { get; set; } = default!;

  public string Level { get; set; } = default!; 

  public int MaximumStudents { get; set; }

  public int MinimumStudents { get; set; }

  public decimal CourseFee { get; set; }

  public bool IsActive { get; set; } = true;

  public bool IsPublished { get; set; }

  public DateOnly StartDate { get; set; }

  public DateOnly EndDate { get; set; }

  public string? Language { get; set; }

  public string? Syllabus { get; set; }

  public string? LearningObjectives { get; set; }

  public string? Prerequisites { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
