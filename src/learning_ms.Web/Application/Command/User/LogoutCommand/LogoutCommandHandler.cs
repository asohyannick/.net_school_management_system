using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IUserRepository;
namespace learning_ms.Web.Application.Command.User.LogoutCommand;
using Mediator;
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
{
  private readonly IUserRepository _userRepository;
  
  public LogoutCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

  public async ValueTask<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
               ?? throw new NotFoundException("User not found.");

    user.AccessToken = string.Empty;
    user.RefreshToken = string.Empty;
    user.RefreshTokenExpirationDate = DateTime.UtcNow;
    user.UpdatedAt = DateTime.UtcNow;

    _userRepository.Update(user);
    await _userRepository.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
