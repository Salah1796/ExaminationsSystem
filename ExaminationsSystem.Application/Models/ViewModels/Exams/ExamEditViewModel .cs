#region Using ...

using System;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels.Exams
{
    public class ExamEditViewModel : ExamCreateViewModel
    {
        public Guid Id { get; set; }
    }
}
