using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IUserRepository;

namespace learning_ms.Web.Application.Command.User.ResendOtpCommand;
using Mediator;
using Microsoft.Extensions.Logging;

public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, Unit>
{
    private const int OtpExpiryMinutes = 10;

    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<ResendOtpCommandHandler> _logger;

    public ResendOtpCommandHandler(
        IUserRepository userRepository, IEmailService emailService, ILogger<ResendOtpCommandHandler> logger)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Request.Email, cancellationToken);

        if (user is null)
        {
            _logger.LogInformation(
                "OTP resend requested for unregistered email {Email}.", request.Request.Email);
            return Unit.Value;
        }

        if (user.IsActive)
        {
            throw new BadRequestException("This account has already been verified.");
        }

        var otp = GenerateOtp();
        
        user.OTPCode = otp;
        user.ResendOTPCode = otp;
        user.OTPExpirationDate = DateTime.UtcNow.AddMinutes(OtpExpiryMinutes);
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        try
        {
            await _emailService.SendOtpResendEmailAsync(
                user.Email, user.FirstName, otp, OtpExpiryMinutes, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send OTP resend email to {Email}.", user.Email);
            throw new InternalServerErrorException("Failed to send OTP resend email.", ex);
        }
        return Unit.Value;
    }

    private static string GenerateOtp() =>
        System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
}
