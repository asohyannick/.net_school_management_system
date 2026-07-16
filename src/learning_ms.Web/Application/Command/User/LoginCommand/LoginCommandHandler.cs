using learning_ms.Web.Application.Common.DTOs.User;
using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Exceptions.ForbiddenAccessException;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.ITokenService;
using learning_ms.Web.Application.Interface.IUserRepository;
namespace learning_ms.Web.Application.Command.User.LoginCommand;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private const int MaxFailedLoginAttempts = 5;

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<Domain.Entities.User.User> _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher<Domain.Entities.User.User> passwordHasher,
        ITokenService tokenService,
        IEmailService emailService,
        ILogger<LoginCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _emailService = emailService;
        _logger = logger;
    }

    public async ValueTask<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        var user = await _userRepository.GetByEmailAsync(dto.Email, cancellationToken)
            ?? throw new BadRequestException("Invalid email or password.");

        if (user.BlockUser)
        {
            throw new ForbiddenAccessException("This account has been blocked. Please contact support.");
        }

        var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

        if (verifyResult == PasswordVerificationResult.Failed)
        {
            user.FailedLoginAttempts += 1;

            if (user.FailedLoginAttempts >= MaxFailedLoginAttempts)
            {
                user.BlockUser = true;
                user.UpdatedAt = DateTime.UtcNow;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync(cancellationToken);

                try
                {
                    await _emailService.SendAccountBlockedEmailAsync(
                        user.Email, user.FirstName,
                        "Too many failed login attempts.", cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send account-blocked email to {Email}.", user.Email);
                }

                throw new ForbiddenAccessException(
                    "This account has been blocked due to too many failed login attempts.");
            }

            user.UpdatedAt = DateTime.UtcNow;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync(cancellationToken);

            throw new BadRequestException("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            throw new BadRequestException("Please verify your email before logging in.");
        }

        if (verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.Password = _passwordHasher.HashPassword(user, dto.Password);
        }

        user.FailedLoginAttempts = 0;

        var accessTokenResult = _tokenService.GenerateAccessToken(
            user.Id, user.Email, user.FirstName, user.LastName, user.Role);
        var refreshTokenResult = _tokenService.GenerateRefreshToken();

        user.AccessToken = accessTokenResult.Token;
        user.RefreshToken = refreshTokenResult.Token;
        user.RefreshTokenExpirationDate = refreshTokenResult.ExpiresAtUtc;
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var userDto = new CreateUserLoginResponseDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };

        return new LoginResult(
            userDto,
            accessTokenResult.Token,
            refreshTokenResult.Token,
            accessTokenResult.ExpiresAtUtc,
            refreshTokenResult.ExpiresAtUtc);
    }
}
