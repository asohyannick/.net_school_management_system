using learning_ms.Web.Application.Common.DTOs.StudentProfile;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IStudentProfileRepository;
namespace learning_ms.Web.Application.Query.StudentProfile.FetchStudentProfileByIdQueryHandler;
using Mediator;

public class FetchStudentProfileByIdQueryHandler
    : IRequestHandler<FetchStudentProfileByIdQuery.FetchStudentProfileByIdQuery, CreateStudentProfileResponseDto>
{
    private readonly IStudentProfileRepository _repository;
    private readonly Mappings.StudentProfileMapper.StudentProfileMapper _mapper;

    public FetchStudentProfileByIdQueryHandler(
        IStudentProfileRepository repository, Mappings.StudentProfileMapper.StudentProfileMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async ValueTask<CreateStudentProfileResponseDto> Handle(
        FetchStudentProfileByIdQuery.FetchStudentProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var profile = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Student profile not found.");

        return _mapper.ToResponseDto(profile);
    }
}
