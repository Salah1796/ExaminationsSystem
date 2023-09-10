using System;

namespace ExaminationsSystem.Application.Models.ViewModels.Exams
{
    public class CreateExamResponseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
