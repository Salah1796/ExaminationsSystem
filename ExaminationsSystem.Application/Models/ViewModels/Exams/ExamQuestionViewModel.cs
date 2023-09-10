using ExaminationsSystem.Application.Models.ViewModels.ExamQuestions;
using System;

namespace ExaminationsSystem.Application.Models.ViewModels.Exams
{
    public class ExamQuestionViewModel : ExamQuestionCreateViewModel
    {
        public Guid Id { get; set; }
    }
}
