using learning_ms.Web.Application.Common.DTOs.StudentProfile;
namespace learning_ms.Web.Application.Query.StudentProfile.SearchStudentProfilesQuery;
using Mediator;

public record SearchStudentProfilesQuery(
    string? SearchTerm,
    string? CurrentClass,
    string? AcademicYear,
    bool? IsActive,
    bool? IsGraduated,
    string? SortBy,
    bool SortDescending,
    int Page = 1,
    int PerPage = 20
    )  : IRequest<PagedResult<CreateStudentProfileResponseDto>>;
