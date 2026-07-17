using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IUserRepository;
namespace learning_ms.Web.Application.Command.User.BlockUserCommand;
using Mediator;
using Microsoft.Extensions.Logging;
public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, Unit>
{
  private readonly IUserRepository _userRepository;
  private readonly IEmailService _emailService;
  private readonly ILogger<BlockUserCommandHandler> _logger;

  public BlockUserCommandHandler(
    IUserRepository userRepository, IEmailService emailService, ILogger<BlockUserCommandHandler> logger)
  {
    _userRepository = userRepository;
    _emailService = emailService;
    _logger = logger;
  }

  public async ValueTask<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
               ?? throw new NotFoundException("User not found.");

    user.BlockUser = true;
    user.UnBlockUser = false;
    user.UpdatedAt = DateTime.UtcNow;

    _userRepository.Update(user);
    await _userRepository.SaveChangesAsync(cancellationToken);

    try
    {
      await _emailService.SendAccountBlockedEmailAsync(
        user.Email, user.FirstName, request.Request.Reason, cancellationToken);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to send account-blocked email to {Email}.", user.Email);
      throw new InternalServerErrorException("Failed to send account-blocked email to {Email}.", ex);
    }

    return Unit.Value;
  }
}
