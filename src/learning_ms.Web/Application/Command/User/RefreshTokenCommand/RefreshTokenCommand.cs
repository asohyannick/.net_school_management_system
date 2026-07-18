using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Command.User.RefreshTokenCommand;
using Mediator;
public record RefreshTokenCommand(RefreshTokenRequestDto Request) : IRequest<RefreshTokenResult>;
public record RefreshTokenResult(
  string AccessToken,
  string RefreshToken,
  DateTime AccessTokenExpiresAtUtc,
  DateTime RefreshTokenExpiresAtUtc
);
