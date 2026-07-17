using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Query.User.FetchAllUsersQuery;
using Mediator;

public record FetchAllUsersQuery(int Page = 1, int PerPage = 20) : IRequest<PagedResult<UserAdminResponseDto>>;
