using ExaminationsSystem.Application.Models.ViewModels.ExamQuestions;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Models.ViewModels.Exams
{
    public class ExamCreateViewModel
    {
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<ExamQuestionCreateViewModel> Questions { get; set; }
    }
}
