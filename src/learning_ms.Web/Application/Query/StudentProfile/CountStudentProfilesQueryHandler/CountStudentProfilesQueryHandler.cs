using learning_ms.Web.Application.Interface.IStudentProfileRepository;
namespace learning_ms.Web.Application.Query.StudentProfile.CountStudentProfilesQueryHandler;
using Mediator;
public class CountStudentProfilesQueryHandler : IRequestHandler<CountStudentProfilesQuery.CountStudentProfilesQuery, int>
{
    private readonly IStudentProfileRepository _repository;
    public CountStudentProfilesQueryHandler(IStudentProfileRepository repository) => _repository = repository;

    public async ValueTask<int> Handle(CountStudentProfilesQuery.CountStudentProfilesQuery request, CancellationToken cancellationToken) =>
        await _repository.CountAsync(cancellationToken);
}
