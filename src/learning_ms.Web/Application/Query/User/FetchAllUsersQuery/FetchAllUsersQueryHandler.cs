using learning_ms.Web.Application.Interface.IUserRepository;
using learning_ms.Web.Application.Mappings.UserMapper;
namespace learning_ms.Web.Application.Query.User.FetchAllUsersQuery;
using Mediator;

public class FetchAllUsersQueryHandler
  : IRequestHandler<FetchAllUsersQuery, PagedResult<Common.DTOs.User.UserAdminResponseDto>>
{
  private readonly IUserRepository _userRepository;
  private readonly UserMapper _userMapper;

  public FetchAllUsersQueryHandler(IUserRepository userRepository, UserMapper userMapper)
  {
    _userRepository = userRepository;
    _userMapper = userMapper;
  }

  public async ValueTask<PagedResult<Common.DTOs.User.UserAdminResponseDto>> Handle(
    FetchAllUsersQuery request, CancellationToken cancellationToken)
  {
    var page = request.Page < 1 ? 1 : request.Page;
    var perPage = request.PerPage is < 1 or > 100 ? 20 : request.PerPage;

    var (items, totalCount) = await _userRepository.GetPagedAsync(page, perPage, cancellationToken);

    var dtoItems = items.Select(_userMapper.ToAdminResponseDto).ToList();
    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);

    return new PagedResult<Common.DTOs.User.UserAdminResponseDto>(
      dtoItems, page, perPage, totalCount, totalPages);
  }
}
