using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.UnBlockUserCommand;
using Mediator;
public record UnBlockUserCommand(Guid UserId, CreateUnBlockUserRequestDto Request) : IRequest<Unit>;
