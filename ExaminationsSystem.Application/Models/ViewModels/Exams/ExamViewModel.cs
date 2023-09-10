#region Using ...

using System;
using System.Collections.Generic;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels.Exams
{
    public class ExamViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<ExamQuestionViewModel> Questions { get; set; }
    }
}
