using learning_ms.Web.Application.Common.DTOs.StudentProfile;
namespace learning_ms.Web.Application.Command.StudentProfile.CreateStudentProfileCommand;
using Mediator;
public record CreateStudentProfileCommand(
  CreateStudentProfileRequestDto Request,
  Guid RequestingUserId) : IRequest<CreateStudentProfileResponseDto>;
