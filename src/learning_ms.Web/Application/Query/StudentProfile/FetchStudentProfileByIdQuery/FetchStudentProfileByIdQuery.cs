using learning_ms.Web.Application.Common.DTOs.StudentProfile;
namespace learning_ms.Web.Application.Query.StudentProfile.FetchStudentProfileByIdQuery;
using Mediator;

public record FetchStudentProfileByIdQuery(Guid Id) : IRequest<CreateStudentProfileResponseDto>;
