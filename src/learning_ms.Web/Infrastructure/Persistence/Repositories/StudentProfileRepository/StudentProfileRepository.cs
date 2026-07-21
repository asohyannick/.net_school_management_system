using learning_ms.Web.Application.Interface.IStudentProfileRepository;
using learning_ms.Web.Domain.Entities.StudentProfile;
namespace learning_ms.Web.Infrastructure.Persistence.Repositories.StudentProfileRepository;
using System.Data;
using Microsoft.EntityFrameworkCore;

public class StudentProfileRepository : IStudentProfileRepository
{
    private readonly AppDbContext _context;

    public StudentProfileRepository(AppDbContext context) => _context = context;

    public Task<StudentProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _context.StudentProfiles.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public Task<StudentProfile?> GetByAdmissionNumberAsync(string admissionNumber, CancellationToken cancellationToken = default) =>
        _context.StudentProfiles.FirstOrDefaultAsync(s => s.AdmissionNumber == admissionNumber, cancellationToken);

    public Task<bool> ExistsByAdmissionNumberAsync(string admissionNumber, CancellationToken cancellationToken = default) =>
        _context.StudentProfiles.AnyAsync(s => s.AdmissionNumber == admissionNumber, cancellationToken);

    public async Task<(List<StudentProfile> Items, int TotalCount)> GetPagedAsync(
        int page, int perPage, CancellationToken cancellationToken = default)
    {
        var query = _context.StudentProfiles.OrderByDescending(s => s.CreatedAt);
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * perPage).Take(perPage).ToListAsync(cancellationToken);
        return (items, totalCount);
    }

    public async Task<(List<StudentProfile> Items, int TotalCount)> SearchAsync(
        StudentProfileSearchFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _context.StudentProfiles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var term = filter.SearchTerm.Trim().ToLower();
            query = query.Where(s =>
                s.FirstName.ToLower().Contains(term) ||
                s.LastName.ToLower().Contains(term) ||
                s.AdmissionNumber.ToLower().Contains(term) ||
                s.Email.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(filter.CurrentClass))
            query = query.Where(s => s.CurrentClass == filter.CurrentClass);

        if (!string.IsNullOrWhiteSpace(filter.AcademicYear))
            query = query.Where(s => s.AcademicYear == filter.AcademicYear);

        if (filter.IsActive.HasValue)
            query = query.Where(s => s.IsActive == filter.IsActive.Value);

        if (filter.IsGraduated.HasValue)
            query = query.Where(s => s.IsGraduated == filter.IsGraduated.Value);

        query = (filter.SortBy?.ToLowerInvariant(), filter.SortDescending) switch
        {
            ("firstname", false) => query.OrderBy(s => s.FirstName),
            ("firstname", true) => query.OrderByDescending(s => s.FirstName),
            ("lastname", false) => query.OrderBy(s => s.LastName),
            ("lastname", true) => query.OrderByDescending(s => s.LastName),
            ("admissionnumber", false) => query.OrderBy(s => s.AdmissionNumber),
            ("admissionnumber", true) => query.OrderByDescending(s => s.AdmissionNumber),
            ("admissiondate", false) => query.OrderBy(s => s.AdmissionDate),
            ("admissiondate", true) => query.OrderByDescending(s => s.AdmissionDate),
            (_, false) => query.OrderBy(s => s.CreatedAt),
            (_, true) => query.OrderByDescending(s => s.CreatedAt),
        };

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((filter.Page - 1) * filter.PerPage)
            .Take(filter.PerPage)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _context.StudentProfiles.CountAsync(cancellationToken);

    public async Task AddAsync(StudentProfile profile, CancellationToken cancellationToken = default) =>
        await _context.StudentProfiles.AddAsync(profile, cancellationToken);

    public void Update(StudentProfile profile) => _context.StudentProfiles.Update(profile);

    public void Remove(StudentProfile profile) => _context.StudentProfiles.Remove(profile);
    
    public async Task<int> AppendImageUrlAndDecrementPendingAsync(
        Guid studentProfileId, string url, CancellationToken cancellationToken = default)
    {
        var connection = _context.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        await using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE student_profiles
            SET profile_picture_url = array_append(profile_picture_url, @url),
                pending_image_count = GREATEST(pending_image_count - 1, 0),
                updated_at = now()
            WHERE id = @id
            RETURNING pending_image_count;
            """;

        var urlParam = command.CreateParameter();
        urlParam.ParameterName = "url";
        urlParam.Value = url;
        command.Parameters.Add(urlParam);

        var idParam = command.CreateParameter();
        idParam.ParameterName = "id";
        idParam.Value = studentProfileId;
        command.Parameters.Add(idParam);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result is int count ? count : -1;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
