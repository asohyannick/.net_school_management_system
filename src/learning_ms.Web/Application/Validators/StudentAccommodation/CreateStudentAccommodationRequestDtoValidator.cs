using FluentValidation;
using learning_ms.Web.Application.Common.DTOs.StudentAccommodation;

namespace learning_ms.Web.Application.Validators.StudentAccommodation;

public class CreateStudentAccommodationRequestDtoValidator : AbstractValidator<CreateStudentAccommodationRequestDto>
{
  public CreateStudentAccommodationRequestDtoValidator()
  {
    RuleFor(x => x.StudentId)
      .NotEmpty().WithMessage("StudentId is required.");

    RuleFor(x => x.AccommodationId)
      .NotEmpty().WithMessage("AccommodationId is required.");

    RuleFor(x => x.BedNumber)
      .NotEmpty().WithMessage("Bed number is required.")
      .MaximumLength(20).WithMessage("Bed number must not exceed 20 characters.");

    RuleFor(x => x.CheckInDate)
      .NotEmpty().WithMessage("Check-in date is required.");

    RuleFor(x => x.ExpectedCheckOutDate)
      .GreaterThan(x => x.CheckInDate)
      .When(x => x.ExpectedCheckOutDate.HasValue)
      .WithMessage("Expected check-out date must be after check-in date.");

    RuleFor(x => x.CheckOutDate)
      .GreaterThanOrEqualTo(x => x.CheckInDate)
      .When(x => x.CheckOutDate.HasValue)
      .WithMessage("Check-out date cannot be before check-in date.");

    RuleFor(x => x.AccommodationFee)
      .GreaterThanOrEqualTo(0).WithMessage("Accommodation fee cannot be negative.");

    RuleFor(x => x.AmountPaid)
      .GreaterThanOrEqualTo(0)
      .When(x => x.AmountPaid.HasValue)
      .WithMessage("Amount paid cannot be negative.")
      .LessThanOrEqualTo(x => x.AccommodationFee)
      .When(x => x.AmountPaid.HasValue)
      .WithMessage("Amount paid cannot exceed the accommodation fee.");

    RuleFor(x => x.Remarks)
      .MaximumLength(1000).WithMessage("Remarks must not exceed 1,000 characters.");
  }
}
