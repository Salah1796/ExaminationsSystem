using ExaminationsSystem.Application.Features.Questions.Commands.Update;
using FluentValidation;

namespace QuestioninationsSystem.Application.Validation.Questions
{
    public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            RuleFor(p => p.QuestionText)
                 .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Type)
               .IsInEnum().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.DifficultyLevel)
              .IsInEnum().WithMessage("{PropertyName} is required.");
        }
    }
}
