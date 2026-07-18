using learning_ms.Web.Application.Command.User.LoginCommand;
using learning_ms.Web.Application.Common.DTOs.User;
using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Interface.IFirebaseAuthService;
using learning_ms.Web.Application.Interface.ITokenService;
using learning_ms.Web.Application.Interface.IUserRepository;
using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Application.Command.User.VerifyFirebaseTokenCommand; 
using Mediator;
public class VerifyFirebaseTokenCommandHandler
    : IRequestHandler<VerifyFirebaseTokenCommand, LoginResult>
{
    private readonly IFirebaseAuthService _firebaseAuthService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<VerifyFirebaseTokenCommandHandler> _logger;

    public VerifyFirebaseTokenCommandHandler(
        IFirebaseAuthService firebaseAuthService,
        IUserRepository userRepository,
        ITokenService tokenService,
        ILogger<VerifyFirebaseTokenCommandHandler> logger)
    {
        _firebaseAuthService = firebaseAuthService;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async ValueTask<LoginResult> Handle(
        VerifyFirebaseTokenCommand request, CancellationToken cancellationToken)
    {
        var firebaseToken = await _firebaseAuthService.VerifyIdTokenAsync(
            request.Request.VerifyFirebaseIdToken, cancellationToken);

        _logger.LogInformation("Firebase UID verified: {FirebaseUid}", firebaseToken.Uid);

        if (string.IsNullOrWhiteSpace(firebaseToken.Email))
        {
            throw new BadRequestException(
                "This Firebase account has no associated email and cannot be used to sign in.");
        }

        var user = await _userRepository.GetByFirebaseUidAsync(firebaseToken.Uid, cancellationToken)
            ?? await _userRepository.GetByEmailAsync(firebaseToken.Email, cancellationToken);

        var isNewUser = user is null;

        if (isNewUser)
        {
            var nameParts = (firebaseToken.Name ?? firebaseToken.Email.Split('@')[0])
                .Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

            user = new Domain.Entities.User.User
            {
                Id = Guid.NewGuid(),
                FirstName = nameParts.Length > 0 ? nameParts[0] : "Firebase",
                LastName = nameParts.Length > 1 ? nameParts[1] : "User",
                Email = firebaseToken.Email,
                Role = UserRole.Student,
                IsActive = firebaseToken.EmailVerified,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _userRepository.AddAsync(user, cancellationToken);
        }

        if (user!.BlockUser)
        {
            throw new BadRequestException("This account has been blocked. Please contact support.");
        }

        user.FirebaseUid = firebaseToken.Uid;
        user.FirebaseProvider = firebaseToken.ProviderId;
        user.IsFirebaseEmailVerified = firebaseToken.EmailVerified;
        user.FirebaseDisplayName = firebaseToken.Name;
        user.PhotoUrl ??= firebaseToken.PhotoUrl;
        user.PhoneNumber ??= firebaseToken.PhoneNumber;
        user.FirebaseCreatedAt ??= DateTime.UtcNow;
        user.FirebaseLastLoginAt = DateTime.UtcNow;

        if (firebaseToken.EmailVerified && !user.IsActive)
        {
            user.IsActive = true;
        }

        user.FailedLoginAttempts = 0;

        var accessTokenResult = _tokenService.GenerateAccessToken(
            user.Id, user.Email, user.FirstName, user.LastName, user.Role);
        var refreshTokenResult = _tokenService.GenerateRefreshToken(user.Id);

        user.AccessToken = accessTokenResult.Token;
        user.RefreshToken = refreshTokenResult.Token;
        user.RefreshTokenExpirationDate = refreshTokenResult.ExpiresAtUtc;
        user.UpdatedAt = DateTime.UtcNow;
        
        if (!isNewUser)
        {
            _userRepository.Update(user);
        }

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
