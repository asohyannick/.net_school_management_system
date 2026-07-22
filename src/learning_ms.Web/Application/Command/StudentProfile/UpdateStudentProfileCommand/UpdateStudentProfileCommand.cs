using learning_ms.Web.Application.Common.DTOs.StudentProfile;
namespace learning_ms.Web.Application.Command.StudentProfile.UpdateStudentProfileCommand;
using Mediator;
public record UpdateStudentProfileCommand(
  Guid Id,
  CreateStudentProfileRequestDto Request,
  Guid RequestingUserId,
  bool IsSuperAdmin
  ) : IRequest<CreateStudentProfileResponseDto>;
