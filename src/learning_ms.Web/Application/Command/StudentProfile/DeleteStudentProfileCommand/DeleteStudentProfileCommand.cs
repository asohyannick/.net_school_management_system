namespace learning_ms.Web.Application.Command.StudentProfile.DeleteStudentProfileCommand;
using Mediator;
public record DeleteStudentProfileCommand(
  Guid Id, Guid RequestingUserId, bool IsSuperAdmin) : IRequest<Unit>;
