using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.SendMagicLinkCommand;
using Mediator;
public record SendMagicLinkCommand(CreateLoginViaMagicLinkTokenRequestDto Request) : IRequest<Unit>;
