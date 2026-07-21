
using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Application.Interface.IFileProcessingService;
using learning_ms.Web.Application.Interface.IStudentProfileRepository;
using learning_ms.Web.Application.Mappings.StudentProfileMapper;
using Mediator;
namespace learning_ms.Web.Application.Command.StudentProfile.CreateStudentProfileCommand;

public class CreateStudentProfileCommandHandler
    : IRequestHandler<CreateStudentProfileCommand, Common.DTOs.StudentProfile.CreateStudentProfileResponseDto>
{
    private readonly IStudentProfileRepository _repository;
    private readonly StudentProfileMapper _mapper;
    private readonly IFileProcessingService _fileProcessingService;
    private readonly IEmailService _emailService;

    public CreateStudentProfileCommandHandler(
        IStudentProfileRepository repository,
        StudentProfileMapper mapper,
        IFileProcessingService fileProcessingService,
        IEmailService emailService)
    {
        _repository = repository;
        _mapper = mapper;
        _fileProcessingService = fileProcessingService;
        _emailService = emailService;
    }

    public async ValueTask<Common.DTOs.StudentProfile.CreateStudentProfileResponseDto> Handle(
        CreateStudentProfileCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        if (await _repository.ExistsByAdmissionNumberAsync(dto.AdmissionNumber, cancellationToken))
        {
            throw new BadRequestException("A student profile with this admission number already exists.");
        }

        var profile = _mapper.ToEntity(dto);
        profile.Id = Guid.NewGuid();
        profile.UserId = request.RequestingUserId;
        profile.CreatedBy = request.RequestingUserId;
        profile.UpdatedBy = request.RequestingUserId;
        profile.CreatedAt = DateTime.UtcNow;
        profile.UpdatedAt = DateTime.UtcNow;
        profile.Age = CalculateAge(profile.DateOfBirth);

        var images = dto.ProfilePictureImages?.Where(f => f.Length > 0).ToList() ?? [];
        profile.PendingImageCount = images.Count;

        await _repository.AddAsync(profile, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        if (images.Count == 0)
        {
            try
            {
                await _emailService.SendAccountActivationEmailAsync(
                    profile.Email, profile.FirstName, BuildActivationUrl(profile.Id), cancellationToken);
            }
            catch
            {
                // Non-fatal — profile creation still succeeds even if email delivery fails.
            }
        }
        else
        {
            foreach (var image in images)
            {
                await _fileProcessingService.EnqueueUploadAsync(
                    image, $"student-profiles/{profile.Id}", profile.Id, cancellationToken);
            }
        }

        return _mapper.ToResponseDto(profile);
    }

    private static string BuildActivationUrl(Guid profileId) =>
        $"http://localhost:3000/activate?studentProfileId={profileId}"; 

    private static int CalculateAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        return age;
    }
}
