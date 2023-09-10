using ExaminationsSystem.Application.Models.ViewModels.Options;
using ExaminationsSystem.Domain.Common.Enums;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Models.ViewModels.Questions
{
    public class QuestionCreateViewModel
    {
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string CorrectAnswer { get; set; }
        public List<QuestionOptionViewModel> Options { get; set; }
    }
}
