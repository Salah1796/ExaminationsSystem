using ExaminationsSystem.Domain.Common.Enums;
using System;

namespace ExaminationsSystem.Application.Models.ViewModels.StudentExams
{
    public abstract class StudentExamResponseViewModelBase
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public Guid StudentId { get; set; }
        public StudentExamStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal AutomaticGradeScore { get; set; }
        public decimal ManualGradeScore { get; set; }
        public decimal TotalScore { get; set; }
        public bool IsAutomaticGraded { get; set; }
        public DateTime? AutomaticGradeDate { get; set; }
        public bool IsManualGraded { get; set; }
        public DateTime? ManualGradeDate { get; set; }
    }
}