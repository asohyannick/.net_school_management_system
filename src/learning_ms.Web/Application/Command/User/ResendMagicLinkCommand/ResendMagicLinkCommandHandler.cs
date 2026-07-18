using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IUserRepository;

namespace learning_ms.Web.Application.Command.User.ResendMagicLinkCommand;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class ResendMagicLinkCommandHandler : IRequestHandler<ResendMagicLinkCommand, Unit>
{
    private const int MagicLinkExpiryMinutes = 15;

    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ResendMagicLinkCommandHandler> _logger;

    public ResendMagicLinkCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IConfiguration configuration,
        ILogger<ResendMagicLinkCommandHandler> logger)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _configuration = configuration;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(ResendMagicLinkCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Request.Email, cancellationToken);

        if (user is null || !user.IsActive || user.BlockUser)
        {
            _logger.LogInformation(
                "Magic-link resend requested for {Email} (found={Found}, active={Active}, blocked={Blocked}).",
                request.Request.Email, user is not null, user?.IsActive, user?.BlockUser);
            return Unit.Value;
        }

        var token = GenerateToken();
        
        user.MagicLinkToken = token;
        user.ResendMagicLinkToken = token;
        user.MagicLinkTokenExpirationDate = DateTime.UtcNow.AddMinutes(MagicLinkExpiryMinutes);
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var frontendBaseUrl = _configuration["FrontendSettings:BaseUrl"] ?? "http://localhost:3000";
        var magicLinkUrl = $"{frontendBaseUrl.TrimEnd('/')}/magic-link?token={Uri.EscapeDataString(token)}";

        try
        {
            await _emailService.SendMagicLinkResendEmailAsync(
                user.Email, user.FirstName, magicLinkUrl, MagicLinkExpiryMinutes, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send magic-link resend email to {Email}.", user.Email);
            throw new InternalServerErrorException("Failed to send magic-link resend email", ex);
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
