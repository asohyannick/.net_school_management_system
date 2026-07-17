namespace learning_ms.Web.Application.Command.User.DeleteAccountCommand;
using Mediator;
public record DeleteAccountCommand(Guid UserId) : IRequest<Unit>;
