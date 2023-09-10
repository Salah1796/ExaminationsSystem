using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Models.ViewModels.StudentExams
{
    public class StudentAnswerViewModel
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid StudentExamId { get; set; }
        public string AnswerText { get; set; }
        public bool IsGraded { get; set; }
        public List<Guid> SelectedOptionsId { get; set; }
    }
}
