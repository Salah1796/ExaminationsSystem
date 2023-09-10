#region Using ...
using ExaminationsSystem.Domain.Common.Enums;
using System;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels.StudentExams
{
    public class StudentExamLightViewModel
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public Guid StudentId { get; set; }
        public StudentExamStatus Status { get; set; }
    }
}
