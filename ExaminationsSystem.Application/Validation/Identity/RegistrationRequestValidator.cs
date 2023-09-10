using ExaminationsSystem.Application.Models.ViewModels.Identity;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.Identity
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Name)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .MaximumLength(200);

            RuleFor(p => p.Email)
              .MaximumLength(320);
        }
    }
}
