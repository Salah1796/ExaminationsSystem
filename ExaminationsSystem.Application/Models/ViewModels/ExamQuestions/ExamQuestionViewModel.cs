using System;

namespace ExaminationsSystem.Application.Models.ViewModels.ExamQuestions
{
    public class ExamQuestionViewModel
    {
        public Guid QuestionId { get; set; }
        public Guid ExamId { get; set; }
        public decimal Points { get; set; }
    }
}
