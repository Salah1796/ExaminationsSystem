using ExaminationsSystem.Application.Features.Exams.Queries.GetById;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.Exams
{
    public class GetExamByIdQueryValidator : AbstractValidator<GetExamByIdQuery>
    {
        public GetExamByIdQueryValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
