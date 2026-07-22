using learning_ms.Web.Application.Common.DTOs.StudentProfile;
using learning_ms.Web.Application.Exceptions.ForbiddenAccessException;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IStudentProfileRepository;
namespace learning_ms.Web.Application.Command.StudentProfile.UpdateStudentProfileCommand;
using Mediator;
public class UpdateStudentProfileCommandHandler
    : IRequestHandler<UpdateStudentProfileCommand, CreateStudentProfileResponseDto>
{
    private readonly IStudentProfileRepository _repository;
    private readonly Mappings.StudentProfileMapper.StudentProfileMapper _mapper;

    public UpdateStudentProfileCommandHandler(
        IStudentProfileRepository repository,
        Mappings.StudentProfileMapper.StudentProfileMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async ValueTask<CreateStudentProfileResponseDto> Handle(
      UpdateStudentProfileCommand request, CancellationToken cancellationToken)
    {
      var profile = await _repository.GetByIdAsync(request.Id, cancellationToken)
                    ?? throw new NotFoundException("Student profile not found.");

      if (!request.IsSuperAdmin && profile.UserId != request.RequestingUserId)
      {
        throw new ForbiddenAccessException("You can only update your own student profile.");
      }
      var dto = request.Request;
      _mapper.ToEntity(dto, profile);
      profile.UpdatedBy = request.RequestingUserId;
      profile.UpdatedAt = DateTime.UtcNow;
      profile.Age = today_minus(dto.DateOfBirth);
      await _repository.SaveChangesAsync(cancellationToken);
      return _mapper.ToResponseDto(profile);
    }

    private static int today_minus(DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dob.Year;
        if (dob > today.AddYears(-age)) age--;
        return age;
    }
}
