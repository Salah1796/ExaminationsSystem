using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Models.ViewModels.StudentExams
{
    public class StudentExamAnswerViewModel
    {
        public Guid StudentExamId { get; set; }
        public Guid QuestionId { get; set; }
        public List<Guid> SelectedOptionsId { get; set; }
        public string AnswerText { get; set; }
    }
}
