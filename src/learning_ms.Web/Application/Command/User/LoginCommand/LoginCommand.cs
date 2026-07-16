using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.LoginCommand;
using Mediator;
public record LoginCommand(CreateUserLoginRequestDto Request) : IRequest<LoginResult>;

public record LoginResult(
  CreateUserLoginResponseDto User,
  string AccessToken,
  string RefreshToken,
  DateTime AccessTokenExpiresAtUtc,
  DateTime RefreshTokenExpiresAtUtc);
