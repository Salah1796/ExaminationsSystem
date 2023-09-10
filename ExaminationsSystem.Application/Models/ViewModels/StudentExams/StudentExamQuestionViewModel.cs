using ExaminationsSystem.Application.Models.ViewModels.StudentExams;
using ExaminationsSystem.Domain.Common.Enums;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Models.ViewModels.ExamQuestions
{
    public class StudentExamQuestionViewModel
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public List<StudentExamQuestionOptionViewModel> Options { get; set; }
        public decimal Points { get; set; }
        public StudentExamQuestionAnswerViewModel? StudentAnswer { get; set; }
    }
}
