using ExaminationsSystem.Application.Models.ViewModels.Identity;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.Identity
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
