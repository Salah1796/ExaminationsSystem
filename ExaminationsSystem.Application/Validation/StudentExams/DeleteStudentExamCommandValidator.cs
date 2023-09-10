using ExaminationsSystem.Application.Features.StudentExams.Commands.Delete;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.StudentExams
{
    public class DeleteStudentExamCommandValidator : AbstractValidator<DeleteStudentExamCommand>
    {
        public DeleteStudentExamCommandValidator()
        {
            RuleFor(p => p.Id)
           .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
