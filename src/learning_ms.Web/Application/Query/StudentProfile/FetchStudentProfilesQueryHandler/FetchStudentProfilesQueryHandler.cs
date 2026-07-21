using learning_ms.Web.Application.Common.DTOs.StudentProfile;
using learning_ms.Web.Application.Interface.IStudentProfileRepository;
namespace learning_ms.Web.Application.Query.StudentProfile.FetchStudentProfilesQueryHandler;
using Mediator;
public class FetchStudentProfilesQueryHandler
    : IRequestHandler<FetchStudentProfilesQuery.FetchStudentProfilesQuery, PagedResult<CreateStudentProfileResponseDto>>
{
    private readonly IStudentProfileRepository _repository;
    private readonly Mappings.StudentProfileMapper.StudentProfileMapper _mapper;

    public FetchStudentProfilesQueryHandler(
        IStudentProfileRepository repository, Mappings.StudentProfileMapper.StudentProfileMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async ValueTask<PagedResult<CreateStudentProfileResponseDto>> Handle(
        FetchStudentProfilesQuery.FetchStudentProfilesQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var perPage = request.PerPage is < 1 or > 100 ? 20 : request.PerPage;

        var (items, totalCount) = await _repository.GetPagedAsync(page, perPage, cancellationToken);
        var dtoItems = items.Select(_mapper.ToResponseDto).ToList();
        var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);

        return new PagedResult<CreateStudentProfileResponseDto>(
            dtoItems, page, perPage, totalCount, totalPages);
    }
}
