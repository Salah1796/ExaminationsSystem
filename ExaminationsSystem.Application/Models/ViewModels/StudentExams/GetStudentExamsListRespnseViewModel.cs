using ExaminationsSystem.Domain.Common.Enums;
using System;

namespace ExaminationsSystem.Application.Models.ViewModels.ExamQuestions
{
    public class GetStudentExamsListRespnseViewModel
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public Guid StudentId { get; set; }
        public StudentExamStatus Status { get; set; }
    }
}
