using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.BookLoan;
namespace learning_ms.Web.Application.Validators.BookLoan;
public class CreateBookLoanRequestDtoValidator : AbstractValidator<CreateBookLoanRequestDto>
{
    public CreateBookLoanRequestDtoValidator()
    {
        RuleFor(x => x.BookId)
            .NotEqual(Guid.Empty).WithMessage("A valid book must be specified.");

        RuleFor(x => x.LibraryId)
            .NotEqual(Guid.Empty).WithMessage("A valid library must be specified.");

        RuleFor(x => x.StudentId)
            .NotEqual(Guid.Empty).WithMessage("A valid student must be specified.");

        RuleFor(x => x.LoanDate)
            .NotEmpty().WithMessage("Loan date is required.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Loan date cannot be in the future.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Due date is required.")
            .GreaterThan(x => x.LoanDate)
            .WithMessage("Due date must be after the loan date.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500).WithMessage("Remarks must not exceed 500 characters.");
    }
}
