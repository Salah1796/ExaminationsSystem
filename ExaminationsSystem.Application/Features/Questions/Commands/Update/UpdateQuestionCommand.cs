using ExaminationsSystem.Application.Models.ViewModels.Options;
using ExaminationsSystem.Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Features.Questions.Commands.Update
{
    public class UpdateQuestionCommand : IRequest<UpdateQuestionCommandResponse>
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string CorrectAnswer { get; set; }
        public List<QuestionOptionViewModel> Options { get; set; }
    }
}
