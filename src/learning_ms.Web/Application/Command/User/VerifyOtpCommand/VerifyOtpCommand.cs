using learning_ms.Web.Application.Common.DTOs.User;
using Mediator;
public record VerifyOtpCommand(CreateVerifyOTPCodeRequestDto Request) : IRequest<VerifyOtpResult>;
public record VerifyOtpResult(Guid UserId, string Email);
