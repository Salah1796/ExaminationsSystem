using ExaminationsSystem.Application.Models.ViewModels.Options;
using ExaminationsSystem.Domain.Common.Enums;
using MediatR;
using System.Collections.Generic;

namespace QuestioninationsSystem.Application.Features.Questions.Commands.Create
{
    public class CreateQuestionCommand : IRequest<CreateQuestionCommandResponse>
    {
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string CorrectAnswer { get; set; }
        public List<QuestionOptionViewModel> Options { get; set; }
    }
}
