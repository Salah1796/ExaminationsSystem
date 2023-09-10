using ExaminationsSystem.Application.Features.StudentExams.Commands.Update;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.StudentExams
{
    public class UpdateStudentExamCommandValidator : AbstractValidator<UpdateStudentExamCommand>
    {
        public UpdateStudentExamCommandValidator()
        {
            RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
