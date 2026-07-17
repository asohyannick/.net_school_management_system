using System.Security.Claims;
using learning_ms.Web.Application.Command.User;
using learning_ms.Web.Application.Command.User.BlockUserCommand;
using learning_ms.Web.Application.Command.User.DeleteAccountCommand;
using learning_ms.Web.Application.Command.User.LoginCommand;
using learning_ms.Web.Application.Command.User.LogoutCommand;
using learning_ms.Web.Application.Command.User.UnBlockUserCommand;
using learning_ms.Web.Application.Common.DTOs.User;
using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Query.User.FetchAllUsersQuery;
using learning_ms.Web.Application.Query.User.FetchUserByIdQuery;
using learning_ms.Web.Domain.Enums.UserRole;
using learning_ms.Web.Infrastructure.ApiResponse;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace learning_ms.Web.Presentation.Controllers.AuthController;
/// <summary>
/// Handles user authentication and authorization — registration, OTP verification,
/// login/logout, session management, and admin-level account moderation.
/// </summary>
[ApiController]
[Route("auth")]
[Tags("Authentication and Authorization of Users")]
public class AuthController : ControllerBase
{
  private readonly ISender _sender;

  public AuthController(ISender sender)
  {
    _sender = sender;
  }

  /// <summary>
  /// Registers a new student account.
  /// </summary>
  /// <remarks>
  /// Creates an inactive account with the <c>Student</c> role by default and sends a
  /// 6-digit OTP verification code to the provided email. The account cannot log in
  /// until the OTP is verified via <c>POST /auth/verify-otp</c>.
  /// </remarks>
  /// <param name="request">First name, last name, email, and password.</param>
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [HttpPost("register")]
  [ProducesResponseType(typeof(ApiResponse<RegisterUserResult>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register(
    [FromBody] CreateRegisterUserRequestDto request,
    CancellationToken cancellationToken)
  {
    var result = await _sender.Send(new RegisterUserCommand(request), cancellationToken);

    return Ok(ApiResponse<RegisterUserResult>.SuccessResponse(
      result,
      "Registration successful. Please check your email for the verification code."));
  }

  /// <summary>
  /// Verifies a registered account using its OTP code.
  /// </summary>
  /// <remarks>
  /// On success, activates the account so it can log in. The OTP is looked up
  /// directly (no email required) and is single-use — it is cleared after verification.
  /// </remarks>
  /// <param name="request">The 6-digit OTP code sent to the user's email.</param>
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [HttpPost("verify-otp")]
  [ProducesResponseType(typeof(ApiResponse<VerifyOtpResult>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> VerifyOtp(
      [FromBody] CreateVerifyOTPCodeRequestDto request,
      CancellationToken cancellationToken)
  {
      var result = await _sender.Send(new VerifyOtpCommand(request), cancellationToken);
  
      return Ok(ApiResponse<VerifyOtpResult>.SuccessResponse(
          result, "Account verified successfully. You can now log in."));
  }

  /// <summary>
  /// Logs a verified user in and issues session tokens.
  /// </summary>
  /// <remarks>
  /// Access and refresh tokens are set as HttpOnly cookies — they are never returned
  /// in the response body. Accounts are automatically blocked after 5 consecutive
  /// failed login attempts.
  /// </remarks>
  /// <param name="request">Email and password.</param>
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [HttpPost("login")]
  [ProducesResponseType(typeof(ApiResponse<CreateUserLoginResponseDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
  public async Task<IActionResult> Login(
      [FromBody] CreateUserLoginRequestDto request,
      CancellationToken cancellationToken)
  {
      var result = await _sender.Send(new LoginCommand(request), cancellationToken);
  
      SetAuthCookies(result);
  
      return Ok(ApiResponse<CreateUserLoginResponseDto>.SuccessResponse(
          result.User, "Login successful."));
  }

  /// <summary>
  /// Logs the current user out and clears their session cookies.
  /// </summary> 
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [Authorize]
  [HttpPost("logout")]
  [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<IActionResult> Logout(CancellationToken cancellationToken)
  {
      var userId = GetCurrentUserId();
      await _sender.Send(new LogoutCommand(userId), cancellationToken);
  
      Response.Cookies.Delete("accessToken");
      Response.Cookies.Delete("refreshToken");
  
      return Ok(ApiResponse<object>.SuccessResponse("Logged out successfully."));
  }

  /// <summary>
  /// Permanently deletes the current user's own account.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [Authorize]
  [HttpDelete("account")]
  [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
  {
      var userId = GetCurrentUserId();
      await _sender.Send(new DeleteAccountCommand(userId), cancellationToken);
  
      Response.Cookies.Delete("accessToken");
      Response.Cookies.Delete("refreshToken");
  
      return Ok(ApiResponse<object>.SuccessResponse("Account deleted successfully."));
  }

  /// <summary>
  /// Fetches a paginated list of all user accounts. SuperAdmin only.
  /// </summary>
  /// <param name="page">1-based page number. Defaults to 1.</param>
  /// <param name="perPage">Items per page (1–100). Defaults to 20.</param>
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [Authorize(Roles = nameof(UserRole.SuperAdmin))]
  [HttpGet("users")]
  [ProducesResponseType(typeof(ApiResponse<PagedResult<UserAdminResponseDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  public async Task<IActionResult> FetchAllUsers(
      [FromQuery] int page = 1, [FromQuery] int perPage = 20, CancellationToken cancellationToken = default)
  {
      var result = await _sender.Send(new FetchAllUsersQuery(page, perPage), cancellationToken);
      return Ok(ApiResponse<PagedResult<UserAdminResponseDto>>.SuccessResponse(result));
  }

  /// <summary>
  /// Fetches a single user account by ID. SuperAdmin only.
  /// </summary>
  /// <param name="userId">The target user's unique identifier.</param>
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [Authorize(Roles = nameof(UserRole.SuperAdmin))]
  [HttpGet("users/{userId:guid}")]
  [ProducesResponseType(typeof(ApiResponse<UserAdminResponseDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> FetchUserById(Guid userId, CancellationToken cancellationToken)
  {
      var result = await _sender.Send(new FetchUserByIdQuery(userId), cancellationToken);
      return Ok(ApiResponse<UserAdminResponseDto>.SuccessResponse(result));
  }

  /// <summary>
  /// Blocks a user account, preventing further login. SuperAdmin only.
  /// </summary>
  /// <param name="userId">The target user's unique identifier.</param>
  /// <param name="request">Reason for blocking, included in the notification email.</param>
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [Authorize(Roles = nameof(UserRole.SuperAdmin))]
  [HttpPatch("users/{userId:guid}/block")]
  [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> BlockAccount(
      Guid userId, [FromBody] CreateBlockUserRequestDto request, CancellationToken cancellationToken)
  {
      await _sender.Send(new BlockUserCommand(userId, request), cancellationToken);
      return Ok(ApiResponse<object>.SuccessResponse("User account blocked successfully."));
  }

  /// <summary>
  /// Restores a blocked user account and resets its failed-login counter. SuperAdmin only.
  /// </summary>
  /// <param name="userId">The target user's unique identifier.</param>
  /// <param name="request">Reason for unblocking, included in the notification email.</param>
  /// <param name="cancellationToken">Cancellation token for the request.</param>
  [Authorize(Roles = nameof(UserRole.SuperAdmin))]
  [HttpPatch("users/{userId:guid}/unblock")]
  [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UnBlockAccount(
      Guid userId, [FromBody] CreateUnBlockUserRequestDto request, CancellationToken cancellationToken)
  {
      await _sender.Send(new UnBlockUserCommand(userId, request), cancellationToken);
      return Ok(ApiResponse<object>.SuccessResponse("User account unblocked successfully."));
  }

  private Guid GetCurrentUserId()
  {
      var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
      return Guid.TryParse(idClaim, out var id)
          ? id
          : throw new BadRequestException("Invalid or missing user identity.");
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
