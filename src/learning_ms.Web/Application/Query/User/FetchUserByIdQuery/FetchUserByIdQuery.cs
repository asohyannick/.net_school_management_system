using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Query.User.FetchUserByIdQuery;
using Mediator;

public record FetchUserByIdQuery(Guid UserId) : IRequest<UserAdminResponseDto>;
