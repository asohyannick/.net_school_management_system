using System.Security.Claims;
using learning_ms.Web.Application.Command.StudentProfile.CreateStudentProfileCommand;
using learning_ms.Web.Application.Command.StudentProfile.DeleteStudentProfileCommand;
using learning_ms.Web.Application.Command.StudentProfile.UpdateStudentProfileCommand;
using learning_ms.Web.Application.Common.DTOs.StudentProfile;
using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Query.StudentProfile.CountStudentProfilesQuery;
using learning_ms.Web.Application.Query.StudentProfile.FetchStudentProfileByIdQuery;
using learning_ms.Web.Application.Query.StudentProfile.FetchStudentProfilesQuery;
using learning_ms.Web.Application.Query.StudentProfile.SearchStudentProfilesQuery;
using learning_ms.Web.Domain.Enums.UserRole;
using learning_ms.Web.Infrastructure.ApiResponse;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace learning_ms.Web.Presentation.Controllers.StudentProfileController;

/// <summary>
/// Manages student profile records, including profile picture uploads processed
/// asynchronously in the background via Hangfire and stored in MinIO.
/// </summary>
[ApiController]
[Route("student-profiles")]
[Tags("Student Profile Management")]
public class StudentProfileController : ControllerBase
{
    private readonly ISender _sender;

    public StudentProfileController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Creates a new student profile. Only SuperAdmin or Student roles may create one.
    /// </summary>
    /// <remarks>
    /// Uploaded profile pictures are queued for background processing via Hangfire and
    /// uploaded to MinIO without blocking this request. An account-activation email is
    /// sent once all images finish processing (or immediately if none were provided).
    /// </remarks>
    /// <param name="request">Student profile fields and optional profile picture files.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    [Authorize(Roles = $"{nameof(UserRole.SuperAdmin)},{nameof(UserRole.Student)}")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<CreateStudentProfileResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromForm] CreateStudentProfileRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CreateStudentProfileCommand(request, GetCurrentUserId()), cancellationToken);

        return Ok(ApiResponse<CreateStudentProfileResponseDto>.SuccessResponse(
            result, "Student profile created. Profile pictures are processing in the background."));
    }

    /// <summary>
    /// Fetches a paginated list of student profiles.
    /// </summary>
    /// <param name="page">1-based page number. Defaults to 1.</param>
    /// <param name="perPage">Items per page (1–100). Defaults to 20.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CreateStudentProfileResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> FetchAll(
        [FromQuery] int page = 1, [FromQuery] int perPage = 20, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new FetchStudentProfilesQuery(page, perPage), cancellationToken);
        return Ok(ApiResponse<PagedResult<CreateStudentProfileResponseDto>>.SuccessResponse(result, "Student profiles have been fetched successfully."));
    }

    /// <summary>
    /// Fetches a single student profile by ID.
    /// </summary>
    /// <param name="id">The student profile's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    [Authorize]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CreateStudentProfileResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FetchById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new FetchStudentProfileByIdQuery(id), cancellationToken);
        return Ok(ApiResponse<CreateStudentProfileResponseDto>.SuccessResponse(result, "Student profile has been fetched successfully"));
    }

    /// <summary>
    /// Updates an existing student profile. Students may only update their own profile.
    /// </summary>
    /// <param name="id">The student profile's unique identifier.</param>
    /// <param name="request">Updated student profile fields.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    [Authorize(Roles = $"{nameof(UserRole.SuperAdmin)},{nameof(UserRole.Student)}")]
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<CreateStudentProfileResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id, [FromForm] CreateStudentProfileRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new UpdateStudentProfileCommand(id, request, GetCurrentUserId(), User.IsInRole(nameof(UserRole.SuperAdmin))),
            cancellationToken);

        return Ok(ApiResponse<CreateStudentProfileResponseDto>.SuccessResponse(
            result, "Student profile updated successfully."));
    }

    /// <summary>
    /// Deletes a student profile and all associated images. Students may only delete
    /// their own profile; SuperAdmin may delete any.
    /// </summary>
    /// <param name="id">The student profile's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    [Authorize(Roles = $"{nameof(UserRole.SuperAdmin)},{nameof(UserRole.Student)}")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _sender.Send(
            new DeleteStudentProfileCommand(id, GetCurrentUserId(), User.IsInRole(nameof(UserRole.SuperAdmin))),
            cancellationToken);

        return Ok(ApiResponse<object>.SuccessResponse(
            "Student profile and all associated images deleted successfully."));
    }

    /// <summary>
    /// Returns the total count of student profiles.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    [HttpGet("count")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Count(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new CountStudentProfilesQuery(), cancellationToken);
        return Ok(ApiResponse<int>.SuccessResponse(result, "Total number of student profiles have been fetched successfully"));
    }

    /// <summary>
    /// Searches student profiles with filtering, sorting, and pagination.
    /// </summary>
    /// <param name="searchTerm">Matches against first name, last name, admission number, or email.</param>
    /// <param name="currentClass">Filter by current class.</param>
    /// <param name="academicYear">Filter by academic year.</param>
    /// <param name="isActive">Filter by active status.</param>
    /// <param name="isGraduated">Filter by graduation status.</param>
    /// <param name="sortBy">Field to sort by: firstName, lastName, admissionNumber, admissionDate.</param>
    /// <param name="sortDescending">Sort direction.</param>
    /// <param name="page">1-based page number. Defaults to 1.</param>
    /// <param name="perPage">Items per page (1–100). Defaults to 20.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    [HttpGet("search")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CreateStudentProfileResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search(
        [FromQuery] string? searchTerm,
        [FromQuery] string? currentClass,
        [FromQuery] string? academicYear,
        [FromQuery] bool? isActive,
        [FromQuery] bool? isGraduated,
        [FromQuery] string? sortBy,
        [FromQuery] bool sortDescending = false,
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(
            new SearchStudentProfilesQuery(
                searchTerm, currentClass, academicYear, isActive, isGraduated,
                sortBy, sortDescending, page, perPage),
            cancellationToken);

        return Ok(ApiResponse<PagedResult<CreateStudentProfileResponseDto>>.SuccessResponse(result));
    }

    private Guid GetCurrentUserId()
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(idClaim, out var id)
            ? id
            : throw new BadRequestException("Invalid or missing user identity.");
    }
}
