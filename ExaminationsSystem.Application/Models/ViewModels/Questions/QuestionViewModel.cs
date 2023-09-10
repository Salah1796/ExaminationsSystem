#region Using ...

using ExaminationsSystem.Application.Models.ViewModels.Options;
using ExaminationsSystem.Domain.Common.Enums;
using System;
using System.Collections.Generic;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels.Exams
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public List<QuestionOptionViewModel> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
