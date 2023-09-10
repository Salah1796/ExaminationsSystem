using System;

namespace ExaminationsSystem.Application.Models.ViewModels.StudentExams
{
    public class StudentExamCreateViewModel
    {
        public Guid ExamId { get; set; }
        public Guid StudentId { get; set; }
    }
}
