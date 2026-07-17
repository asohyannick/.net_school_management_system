using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.ResendOtpCommand;
using Mediator;

public record ResendOtpCommand(CreateResendOTPCodeRequestDto Request) : IRequest<Unit>;
