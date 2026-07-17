using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IUserRepository;
namespace learning_ms.Web.Application.Command.User.ForgotPasswordCommand;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private const int ResetLinkExpiryMinutes = 30;

    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;

    public ForgotPasswordCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IConfiguration configuration,
        ILogger<ForgotPasswordCommandHandler> logger)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _configuration = configuration;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Request.Email, cancellationToken);

        // Deliberately silent on "not found" — prevents email-enumeration attacks.
        // The caller always sees the same success message regardless.
        if (user is null)
        {
            _logger.LogInformation(
                "Forgot-password requested for unregistered email {Email}.", request.Request.Email);
            return Unit.Value;
        }

        var token = GenerateToken();
        user.ForgotPassword = token;
        user.ForgotPasswordExpirationDate = DateTime.UtcNow.AddMinutes(ResetLinkExpiryMinutes);
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var frontendBaseUrl = _configuration["FrontendSettings:BaseUrl"] ?? "http://localhost:3000";
        var resetUrl = $"{frontendBaseUrl.TrimEnd('/')}/reset-password?token={Uri.EscapeDataString(token)}";

        try
        {
            await _emailService.SendForgotPasswordEmailAsync(
                user.Email, user.FirstName, resetUrl, ResetLinkExpiryMinutes, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send forgot-password email to {Email}.", user.Email);
            throw new InternalServerErrorException("Failed  to send forgot-password email to {Email}.", ex);
        }

        return Unit.Value;
    }

    private static string GenerateToken()
    {
        var randomBytes = new byte[48];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes)
            .Replace("+", "-").Replace("/", "_").Replace("=", "");
    }
}
