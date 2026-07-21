using learning_ms.Web.Application.Exceptions.ForbiddenAccessException;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IFileStorageService;
using learning_ms.Web.Application.Interface.IStudentProfileRepository;

namespace learning_ms.Web.Application.Command.StudentProfile.DeleteStudentProfileCommand;
using Mediator;
using Microsoft.Extensions.Logging;
public class DeleteStudentProfileCommandHandler : IRequestHandler<DeleteStudentProfileCommand, Unit>
{
    private readonly IStudentProfileRepository _repository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IEmailService _emailService;
    private readonly ILogger<DeleteStudentProfileCommandHandler> _logger;

    public DeleteStudentProfileCommandHandler(
        IStudentProfileRepository repository,
        IFileStorageService fileStorageService,
        IEmailService emailService,
        ILogger<DeleteStudentProfileCommandHandler> logger)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
        _emailService = emailService;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(DeleteStudentProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Student profile not found.");

        if (!request.IsSuperAdmin && profile.UserId != request.RequestingUserId)
        {
            throw new ForbiddenAccessException("You can only delete your own student profile.");
        }

        foreach (var imageUrl in profile.ProfilePictureUrl ?? [])
        {
            try
            {
                var objectName = ExtractObjectNameFromUrl(imageUrl);
                if (!string.IsNullOrWhiteSpace(objectName))
                {
                    await _fileStorageService.DeleteAsync(objectName, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex,
                    "Failed to delete MinIO object for StudentProfile {Id}, url {Url}.", profile.Id, imageUrl);
            }
        }

        _repository.Remove(profile);
        await _repository.SaveChangesAsync(cancellationToken);

        try
        {
            await _emailService.SendAccountDeletionEmailAsync(
                profile.Email, profile.FirstName, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send deletion email for StudentProfile {Id}.", profile.Id);
        }

        return Unit.Value;
    }
    
    private static string? ExtractObjectNameFromUrl(string url)
    {
        var uri = new Uri(url);
        var segments = uri.AbsolutePath.TrimStart('/').Split('/', 2);
        return segments.Length == 2 ? segments[1] : null;
    }
}
