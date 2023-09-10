using ExaminationsSystem.Application.Features.Exams.Commands.Create;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.Exams
{
    public class CreateExamCommandValidator : AbstractValidator<CreateExamCommand>
    {
        public CreateExamCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Duration)
               .NotEqual(0).WithMessage("{PropertyName} is required.");
        }
    }
}
