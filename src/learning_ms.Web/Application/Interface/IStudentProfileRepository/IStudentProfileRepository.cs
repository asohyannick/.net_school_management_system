using learning_ms.Web.Domain.Entities.StudentProfile;
namespace learning_ms.Web.Application.Interface.IStudentProfileRepository;
public record StudentProfileSearchFilter(
  string? SearchTerm,
  string? CurrentClass,
  string? AcademicYear,
  bool? IsActive,
  bool? IsGraduated,
  string? SortBy,
  bool SortDescending,
  int Page,
  int PerPage 
  );

public interface IStudentProfileRepository
{
  Task<StudentProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<StudentProfile?> GetByAdmissionNumberAsync(string admissionNumber, CancellationToken cancellationToken = default);
  Task<bool> ExistsByAdmissionNumberAsync(string admissionNumber, CancellationToken cancellationToken = default);
  Task<(List<StudentProfile> Items, int TotalCount)> GetPagedAsync(
    int page, int perPage, CancellationToken cancellationToken = default);
  Task<(List<StudentProfile> Items, int TotalCount)> SearchAsync(
    StudentProfileSearchFilter filter, CancellationToken cancellationToken = default);
  Task<int> CountAsync(CancellationToken cancellationToken = default);
  Task AddAsync(StudentProfile profile, CancellationToken cancellationToken = default);
  void Update(StudentProfile profile);
  void Remove(StudentProfile profile);
  Task<int> AppendImageUrlAndDecrementPendingAsync(
    Guid studentProfileId, string url, CancellationToken cancellationToken = default);
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
