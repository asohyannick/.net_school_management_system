using learning_ms.Web.Application.Command.User.LoginCommand;
using learning_ms.Web.Application.Common.DTOs.User;
using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Interface.ITokenService;
using learning_ms.Web.Application.Interface.IUserRepository;
namespace learning_ms.Web.Application.Command.User.VerifyMagicLinkCommand;
using Mediator;

public class VerifyMagicLinkCommandHandler : IRequestHandler<VerifyMagicLinkCommand, LoginResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public VerifyMagicLinkCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async ValueTask<LoginResult> Handle(VerifyMagicLinkCommand request, CancellationToken cancellationToken)
    {
        var token = request.Request.VerifyMagicLinkToken;

        var user = await _userRepository.GetByMagicLinkTokenAsync(token, cancellationToken)
            ?? throw new BadRequestException("Invalid or expired sign-in link.");

        if (user.MagicLinkTokenExpirationDate < DateTime.UtcNow)
        {
            throw new BadRequestException("This sign-in link has expired. Please request a new one.");
        }

        if (user.BlockUser)
        {
            throw new BadRequestException("This account has been blocked. Please contact support.");
        }

        if (!user.IsActive)
        {
            throw new BadRequestException("Please verify your email before signing in.");
        }

        user.VerifyMagicLinkToken = token;
        user.MagicLinkToken = string.Empty;
        user.MagicLinkTokenExpirationDate = DateTime.UtcNow;
        user.FailedLoginAttempts = 0;

        var accessTokenResult = _tokenService.GenerateAccessToken(
            user.Id, user.Email, user.FirstName, user.LastName, user.Role);
        var refreshTokenResult = _tokenService.GenerateRefreshToken(user.Id);

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
