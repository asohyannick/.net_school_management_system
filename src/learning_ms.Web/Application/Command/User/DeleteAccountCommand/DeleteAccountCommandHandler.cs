using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IUserRepository;
namespace learning_ms.Web.Application.Command.User.DeleteAccountCommand;
using Mediator;
using Microsoft.Extensions.Logging;
public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
  private readonly IUserRepository _userRepository;
  private readonly IEmailService _emailService;
  private readonly ILogger<DeleteAccountCommandHandler> _logger;

  public DeleteAccountCommandHandler(
    IUserRepository userRepository, IEmailService emailService, ILogger<DeleteAccountCommandHandler> logger)
  {
    _userRepository = userRepository;
    _emailService = emailService;
    _logger = logger;
  }

  public async ValueTask<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
               ?? throw new NotFoundException("User not found.");

    _userRepository.Remove(user);
    await _userRepository.SaveChangesAsync(cancellationToken);

    try
    {
      await _emailService.SendAccountDeletionEmailAsync(user.Email, user.FirstName, cancellationToken);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to send account-deletion email to {Email}.", user.Email);
      throw new InternalServerErrorException("Failed to send account-deletion email to {Email}.", ex);
    }

    return Unit.Value;
  }
}
