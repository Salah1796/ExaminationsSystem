using FluentValidation;
using QuestioninationsSystem.Application.Features.Questions.Commands.Delete;

namespace QuestioninationsSystem.Application.Validation.Questions
{
    public class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionCommandValidator()
        {
            RuleFor(p => p.Id)
           .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
