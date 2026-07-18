using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IPasswordHasher;
using learning_ms.Web.Application.Interface.IUserRepository;
namespace learning_ms.Web.Application.Command.User.ResetPasswordCommand;
using Mediator;
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;

  public ResetPasswordCommandHandler(
    IUserRepository userRepository, IPasswordHasher passwordHasher)
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
  }

  public async ValueTask<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
  {
    var dto = request.Request;

    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
               ?? throw new NotFoundException("User not found.");

    user.Password = _passwordHasher.HashPassword(dto.NewPassword);
    user.ForgotPassword = string.Empty;
    user.ForgotPasswordExpirationDate = DateTime.UtcNow;

    user.FailedLoginAttempts = 0;
    user.BlockUser = false;

    user.UpdatedAt = DateTime.UtcNow;

    _userRepository.Update(user);
    await _userRepository.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
