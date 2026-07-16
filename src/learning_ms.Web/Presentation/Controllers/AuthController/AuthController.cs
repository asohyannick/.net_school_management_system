using learning_ms.Web.Application.Command.User;
using learning_ms.Web.Application.Command.User.LoginCommand;
using learning_ms.Web.Application.Common.DTOs.User;
using learning_ms.Web.Infrastructure.ApiResponse;
using Mediator;
using Microsoft.AspNetCore.Mvc;
namespace learning_ms.Web.Presentation.Controllers.AuthController;
[ApiController]
[Route("api/auth")]
[Tags("Authentication and Authorization of Users")]
public class AuthController : ControllerBase
{
  private readonly ISender _sender;

  public AuthController(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(
    [FromBody] CreateRegisterUserRequestDto request,
    CancellationToken cancellationToken)
  {
    var result = await _sender.Send(new RegisterUserCommand(request), cancellationToken);

    return Ok(ApiResponse<RegisterUserResult>.SuccessResponse(
      result,
      "Registration successful. Please check your email for the verification code."));
  }
  
  [HttpPost("verify-otp")]
  public async Task<IActionResult> VerifyOtp(
      [FromBody] CreateVerifyOTPCodeRequestDto request,
      CancellationToken cancellationToken)
  {
      var result = await _sender.Send(new VerifyOtpCommand(request), cancellationToken);
  
      return Ok(ApiResponse<VerifyOtpResult>.SuccessResponse(
          result, "Account verified successfully. You can now log in."));
  }
  
  [HttpPost("login")]
  public async Task<IActionResult> Login(
      [FromBody] CreateUserLoginRequestDto request,
      CancellationToken cancellationToken)
  {
      var result = await _sender.Send(new LoginCommand(request), cancellationToken);
  
      SetAuthCookies(result);
  
      return Ok(ApiResponse<CreateUserLoginResponseDto>.SuccessResponse(
          result.User, "Login successful."));
  }
  
  private void SetAuthCookies(LoginResult result)
  {
      Response.Cookies.Append("accessToken", result.AccessToken, new CookieOptions
      {
          HttpOnly = true,
          Secure = true,
          SameSite = SameSiteMode.Strict,
          Expires = result.AccessTokenExpiresAtUtc
      });
  
      Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
      {
          HttpOnly = true,
          Secure = true,
          SameSite = SameSiteMode.Strict,
          Expires = result.RefreshTokenExpiresAtUtc
      });
  }
}
