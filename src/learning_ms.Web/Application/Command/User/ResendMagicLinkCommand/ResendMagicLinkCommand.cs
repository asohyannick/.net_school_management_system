using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.ResendMagicLinkCommand;
using Mediator;

public record ResendMagicLinkCommand(CreateResendMagicLinkTokenRequestDto Request) : IRequest<Unit>;
