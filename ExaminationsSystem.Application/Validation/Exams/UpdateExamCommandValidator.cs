using ExaminationsSystem.Application.Features.Exams.Commands.Update;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.Exams
{
    public class UpdateExamCommandValidator : AbstractValidator<UpdateExamCommand>
    {
        public UpdateExamCommandValidator()
        {
            RuleFor(p => p.Id)
              .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Duration)
               .NotEqual(0).WithMessage("{PropertyName} is required.");
        }
    }
}
