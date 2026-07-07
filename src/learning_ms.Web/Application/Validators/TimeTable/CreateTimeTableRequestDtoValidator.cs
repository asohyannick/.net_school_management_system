using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.TimeTable;
namespace learning_ms.Web.Application.Validators.TimeTable;
public class CreateTimeTableRequestDtoValidator : AbstractValidator<CreateTimeTableRequestDto>
{
    public CreateTimeTableRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1,000 characters.");

        RuleFor(x => x.AcademicYearId)
            .NotEmpty().WithMessage("AcademicYearId is required.");

        RuleFor(x => x.TermId)
            .NotEmpty().WithMessage("TermId is required.");

        RuleFor(x => x.ClassId)
            .NotEmpty().WithMessage("ClassId is required.");

        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("SectionId is required.");

        RuleFor(x => x.DayOfWeek)
            .IsInEnum().WithMessage("A valid day of week is required.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be after start time.");

        RuleFor(x => x.SubjectId)
            .NotEmpty()
            .When(x => x.IsBreak != true)
            .WithMessage("SubjectId is required for non-break periods.");

        RuleFor(x => x.TeacherId)
            .NotEmpty()
            .When(x => x.IsBreak != true)
            .WithMessage("TeacherId is required for non-break periods.");

        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId is required.");

        RuleFor(x => x.PeriodName)
            .NotEmpty().WithMessage("Period name is required.")
            .MaximumLength(100).WithMessage("Period name must not exceed 100 characters.");

        RuleFor(x => x.PeriodNumber)
            .GreaterThan(0).WithMessage("Period number must be greater than 0.");
    }
}
