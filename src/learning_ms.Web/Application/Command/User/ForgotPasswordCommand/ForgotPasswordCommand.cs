using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.ForgotPasswordCommand;
using Mediator;
public record ForgotPasswordCommand(CreateForgotPasswordRequestDto Request) : IRequest<Unit>;
