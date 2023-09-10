#region Using ...

using System;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels.StudentExams
{
    public class StudentExamEditViewModel : StudentExamCreateViewModel
    {
        public Guid Id { get; set; }
    }
}
