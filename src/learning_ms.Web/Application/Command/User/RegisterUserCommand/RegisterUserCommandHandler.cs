using System.Security.Cryptography;
using learning_ms.Web.Application.Command.User;
using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IPasswordHasher;
using learning_ms.Web.Application.Interface.IUserRepository;
using learning_ms.Web.Domain.Entities.User;
using learning_ms.Web.Domain.Enums.UserRole;
using Mediator;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private const int OtpExpiryMinutes = 10;

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IEmailService emailService,
        ILogger<RegisterUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
        _logger = logger;
    }

    public async ValueTask<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        if (await _userRepository.ExistsByEmailAsync(dto.Email, cancellationToken))
        {
            throw new BadRequestException("An account with this email already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Role = UserRole.Student,
            IsActive = false,
        };

        user.Password = _passwordHasher.HashPassword(dto.Password);

        var otp = GenerateOtp();
        user.OTPCode = otp;
        user.OTPExpirationDate = DateTime.UtcNow.AddMinutes(OtpExpiryMinutes);

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        try
        {
            await _emailService.SendOtpVerificationEmailAsync(
                user.Email, user.FirstName, otp, OtpExpiryMinutes, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send OTP email to {Email} after registration.", user.Email);
            throw new InternalServerErrorException("Failed to send OTP email.", ex);
        }

        return new RegisterUserResult(user.Id, user.FirstName, user.LastName, user.Email, UserRole.Student);
    }

    private static string GenerateOtp() => RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
}
