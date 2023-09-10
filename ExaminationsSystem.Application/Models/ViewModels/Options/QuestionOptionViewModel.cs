using System;

namespace ExaminationsSystem.Application.Models.ViewModels.Options
{
    public class QuestionOptionViewModel
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
    }
}
