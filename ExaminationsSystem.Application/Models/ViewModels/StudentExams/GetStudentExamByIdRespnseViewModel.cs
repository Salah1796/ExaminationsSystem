using ExaminationsSystem.Application.Models.ViewModels.ExamQuestions;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Models.ViewModels.StudentExams
{
    public class GetStudentExamByIdRespnseViewModel : StudentExamResponseViewModelBase
    {
        public List<StudentExamQuestionViewModel> Questions { get; set; }
    }
}