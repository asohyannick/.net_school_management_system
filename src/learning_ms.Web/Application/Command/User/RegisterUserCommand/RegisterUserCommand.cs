using learning_ms.Web.Application.Common.DTOs.User;
using Mediator;
namespace learning_ms.Web.Application.Command.User;
public record RegisterUserCommand(CreateRegisterUserRequestDto Request) : IRequest<RegisterUserResult>;
public record RegisterUserResult(Guid UserId, string Email);

