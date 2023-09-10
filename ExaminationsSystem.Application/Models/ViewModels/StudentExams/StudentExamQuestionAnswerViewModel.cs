using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Models.ViewModels.StudentExams
{
    public class StudentExamQuestionAnswerViewModel
    {
        public Guid Id { get; set; }
        public string AnswerText { get; set; }
        public List<Guid> SelectedOptionsId { get; set; }
    }
}
