using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.BlockUserCommand;
using Mediator;
public record BlockUserCommand(Guid UserId, CreateBlockUserRequestDto Request) : IRequest<Unit>;
