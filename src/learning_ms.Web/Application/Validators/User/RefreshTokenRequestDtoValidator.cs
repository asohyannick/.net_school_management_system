using learning_ms.Web.Application.Common.DTOs.User;
namespace learning_ms.Web.Application.Validators.User;
using FluentValidation;
public class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenRequestDto>
{
    public RefreshTokenRequestDtoValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required.");
    }
}
