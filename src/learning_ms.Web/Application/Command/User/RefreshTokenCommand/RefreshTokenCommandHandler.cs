using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Exceptions.ForbiddenAccessException;
using learning_ms.Web.Application.Interface.ITokenService;
using learning_ms.Web.Application.Interface.IUserRepository;

namespace learning_ms.Web.Application.Command.User.RefreshTokenCommand;
using System.IdentityModel.Tokens.Jwt;
using Mediator;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async ValueTask<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        var principal = _tokenService.ValidateRefreshToken(dto.RefreshToken);
        if (principal is null)
        {
            throw new BadRequestException("Invalid or expired refresh token.");
        }

        var userIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new BadRequestException("Invalid refresh token payload.");
        }

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new BadRequestException("Invalid refresh token.");

        if (user.BlockUser)
        {
            throw new ForbiddenAccessException("This account has been blocked. Please contact support.");
        }
        
        if (!string.Equals(user.RefreshToken, dto.RefreshToken, StringComparison.Ordinal))
        {
            throw new BadRequestException("Refresh token has been revoked. Please log in again.");
        }

        if (user.RefreshTokenExpirationDate <= DateTime.UtcNow)
        {
            throw new BadRequestException("Refresh token has expired. Please log in again.");
        }
        
        var accessTokenResult = _tokenService.GenerateAccessToken(
            user.Id, user.Email, user.FirstName, user.LastName, user.Role);
        var refreshTokenResult = _tokenService.GenerateRefreshToken(user.Id);

        user.AccessToken = accessTokenResult.Token;
        user.RefreshToken = refreshTokenResult.Token;
        user.RefreshTokenExpirationDate = refreshTokenResult.ExpiresAtUtc;
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return new RefreshTokenResult(
            accessTokenResult.Token,
            refreshTokenResult.Token,
            accessTokenResult.ExpiresAtUtc,
            refreshTokenResult.ExpiresAtUtc);
    }
}
