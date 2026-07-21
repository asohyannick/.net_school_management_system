using learning_ms.Web.Application.Common.DTOs.StudentProfile;
namespace learning_ms.Web.Application.Query.StudentProfile.FetchStudentProfilesQuery;
using Mediator;

public record FetchStudentProfilesQuery(int Page = 1, int PerPage = 20)
  : IRequest<PagedResult<CreateStudentProfileResponseDto>>;
