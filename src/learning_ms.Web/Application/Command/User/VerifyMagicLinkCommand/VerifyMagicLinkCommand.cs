using learning_ms.Web.Application.Command.User.LoginCommand;
using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.VerifyMagicLinkCommand;
using Mediator;
public record VerifyMagicLinkCommand(CreateVerifyMagicLinkTokenRequestDto Request) : IRequest<LoginResult>;
