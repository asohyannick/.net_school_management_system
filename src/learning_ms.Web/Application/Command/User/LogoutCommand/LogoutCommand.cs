namespace learning_ms.Web.Application.Command.User.LogoutCommand;
using Mediator;
public record LogoutCommand(Guid UserId) : IRequest<Unit>;
