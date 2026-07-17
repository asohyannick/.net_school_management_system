using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.ResetPasswordCommand;
using Mediator;

public record ResetPasswordCommand(Guid UserId, CreateResetPasswordRequestDto Request) : IRequest<Unit>;
