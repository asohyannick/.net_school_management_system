using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IUserRepository;
using Mediator;
public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, VerifyOtpResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<VerifyOtpCommandHandler> _logger;

    public VerifyOtpCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        ILogger<VerifyOtpCommandHandler> logger)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async ValueTask<VerifyOtpResult> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var otpCode = request.Request.VerifyOtpCode;

        var user = await _userRepository.GetByOtpCodeAsync(otpCode, cancellationToken)
            ?? throw new BadRequestException("Invalid or expired verification code.");

        if (user.IsActive)
        {
            throw new BadRequestException("This account has already been verified.");
        }

        if (user.OTPExpirationDate < DateTime.UtcNow)
        {
            throw new BadRequestException("This verification code has expired. Please request a new one.");
        }

        user.IsActive = true;
        user.OTPCode = string.Empty;
        user.OTPExpirationDate = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        try
        {
            await _emailService.SendWelcomeEmailAsync(user.Email, user.FirstName, user.LastName, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send welcome email to {Email} after OTP verification.", user.Email);
            throw new InternalServerErrorException("Failed to send welcome email to " + ex);
        }

        return new VerifyOtpResult(user.Id, user.Email);
    }
}
