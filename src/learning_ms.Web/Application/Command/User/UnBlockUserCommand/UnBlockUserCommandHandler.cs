using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IUserRepository;
namespace learning_ms.Web.Application.Command.User.UnBlockUserCommand;
using Mediator;
using Microsoft.Extensions.Logging;
public class UnBlockUserCommandHandler : IRequestHandler<UnBlockUserCommand, Unit>
{
  private readonly IUserRepository _userRepository;
  private readonly IEmailService _emailService;
  private readonly ILogger<UnBlockUserCommandHandler> _logger;

  public UnBlockUserCommandHandler(
    IUserRepository userRepository, IEmailService emailService, ILogger<UnBlockUserCommandHandler> logger)
  {
    _userRepository = userRepository;
    _emailService = emailService;
    _logger = logger;
  }

  public async ValueTask<Unit> Handle(UnBlockUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
               ?? throw new NotFoundException("User not found.");

    user.BlockUser = false;
    user.UnBlockUser = true;
    user.FailedLoginAttempts = 0;
    user.UpdatedAt = DateTime.UtcNow;

    _userRepository.Update(user);
    await _userRepository.SaveChangesAsync(cancellationToken);

    try
    {
      await _emailService.SendAccountUnblockedEmailAsync(user.Email, user.FirstName, cancellationToken);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to send account-unblocked email to {Email}.", user.Email);
    }

    return Unit.Value;
  }
}
