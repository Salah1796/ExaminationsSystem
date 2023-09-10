using ExaminationsSystem.Application.Features.Exams.Commands.Delete;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.Exams
{
    public class DeleteExamCommandValidator : AbstractValidator<DeleteExamCommand>
    {
        public DeleteExamCommandValidator()
        {
            RuleFor(p => p.Id)
           .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
