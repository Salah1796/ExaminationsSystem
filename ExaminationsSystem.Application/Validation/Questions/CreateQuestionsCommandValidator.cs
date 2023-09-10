using FluentValidation;
using QuestioninationsSystem.Application.Features.Questions.Commands.Create;

namespace QuestioninationsSystem.Application.Validation.Questions
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
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
