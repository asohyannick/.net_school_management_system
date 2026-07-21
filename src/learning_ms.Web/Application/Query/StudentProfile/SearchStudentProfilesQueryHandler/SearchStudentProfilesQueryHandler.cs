using learning_ms.Web.Application.Common.DTOs.StudentProfile;
using learning_ms.Web.Application.Interface.IStudentProfileRepository;
using learning_ms.Web.Application.Mappings.StudentProfileMapper; 
namespace learning_ms.Web.Application.Query.StudentProfile.SearchStudentProfilesQueryHandler;
using Mediator;
public class SearchStudentProfilesQueryHandler
  : IRequestHandler<SearchStudentProfilesQuery.SearchStudentProfilesQuery, PagedResult<CreateStudentProfileResponseDto>>
{
  private readonly IStudentProfileRepository _repository;
  private readonly StudentProfileMapper _mapper;

  public SearchStudentProfilesQueryHandler(
    IStudentProfileRepository repository, StudentProfileMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async ValueTask<PagedResult<CreateStudentProfileResponseDto>> Handle(
    SearchStudentProfilesQuery.SearchStudentProfilesQuery request, CancellationToken cancellationToken)
  {
    var page = request.Page < 1 ? 1 : request.Page;
    var perPage = request.PerPage is < 1 or > 100 ? 20 : request.PerPage;

    var filter = new StudentProfileSearchFilter(
      request.SearchTerm, request.CurrentClass, request.AcademicYear,
      request.IsActive, request.IsGraduated, request.SortBy, request.SortDescending,
      page, perPage);

    var (items, totalCount) = await _repository.SearchAsync(filter, cancellationToken);
    var dtoItems = items.Select(_mapper.ToResponseDto).ToList();
    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);

    return new PagedResult<CreateStudentProfileResponseDto>(
      dtoItems, page, perPage, totalCount, totalPages);
  }
}
