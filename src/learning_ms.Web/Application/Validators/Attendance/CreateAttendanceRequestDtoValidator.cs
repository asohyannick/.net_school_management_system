using learning_ms.Web.Application.Common.DTOs.Attendance;
namespace learning_ms.Web.Application.Validators.Attendance;
using FluentValidation;
public class CreateAttendanceRequestDtoValidator : AbstractValidator<CreateAttendanceRequestDto>
{
  public CreateAttendanceRequestDtoValidator()
  {
    RuleFor(x => x.StudentId)
      .NotEqual(Guid.Empty).WithMessage("A valid student must be specified.");

    RuleFor(x => x.CourseId)
      .NotEqual(Guid.Empty).WithMessage("A valid course must be specified.");

    RuleFor(x => x.TutorId)
      .NotEqual(Guid.Empty).WithMessage("A valid tutor must be specified.");

    RuleFor(x => x.ClassroomId)
      .NotEqual(Guid.Empty).WithMessage("Classroom ID must be a valid identifier.")
      .When(x => x.ClassroomId.HasValue);

    RuleFor(x => x.AttendanceDate)
      .NotEmpty().WithMessage("Attendance date is required.")
      .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
      .WithMessage("Attendance date cannot be in the future.");

    RuleFor(x => x.Status)
      .IsInEnum().WithMessage("Attendance status must be a valid value.");

    RuleFor(x => x.CheckOutTime)
      .GreaterThan(x => x.CheckInTime)
      .WithMessage("Check-out time must be after check-in time.")
      .When(x => x.CheckInTime.HasValue && x.CheckOutTime.HasValue);

    RuleFor(x => x.Reason)
      .MaximumLength(500).WithMessage("Reason must not exceed 500 characters.")
      .When(x => !string.IsNullOrEmpty(x.Reason));

    RuleFor(x => x.Remarks)
      .MaximumLength(500).WithMessage("Remarks must not exceed 500 characters.")
      .When(x => !string.IsNullOrEmpty(x.Remarks));
  }
}
