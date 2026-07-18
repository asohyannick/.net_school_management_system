using learning_ms.Web.Application.Command.User.LoginCommand;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.VerifyFirebaseTokenCommand;
using Mediator;
public record VerifyFirebaseTokenCommand(CreateVerifyFirebaseTokenRequestDto Request) : IRequest<LoginResult>;
