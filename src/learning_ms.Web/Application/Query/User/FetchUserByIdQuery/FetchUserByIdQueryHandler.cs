using learning_ms.Web.Application.Common.DTOs.User;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IUserRepository;
using learning_ms.Web.Application.Mappings.UserMapper;
namespace learning_ms.Web.Application.Query.User.FetchUserByIdQuery;
using Mediator;

public class FetchUserByIdQueryHandler
  : IRequestHandler<FetchUserByIdQuery, UserAdminResponseDto>
{
  private readonly IUserRepository _userRepository;
  private readonly UserMapper _userMapper;

  public FetchUserByIdQueryHandler(IUserRepository userRepository, UserMapper userMapper)
  {
    _userRepository = userRepository;
    _userMapper = userMapper;
  }

  public async ValueTask<UserAdminResponseDto> Handle(
    FetchUserByIdQuery request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
               ?? throw new NotFoundException("User not found.");

    return _userMapper.ToAdminResponseDto(user);
  }
}
