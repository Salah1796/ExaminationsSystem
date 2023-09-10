using ExaminationsSystem.Application.Features.StudentExams.Queries.GetById;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.StudentExams
{
    public class GetStudentExamByIdQueryValidator : AbstractValidator<GetStudentExamByIdQuery>
    {
        public GetStudentExamByIdQueryValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
