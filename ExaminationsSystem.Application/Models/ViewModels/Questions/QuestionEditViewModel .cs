#region Using ...

using System;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels.Questions
{
    public class QuestionEditViewModel : QuestionCreateViewModel
    {
        public Guid Id { get; set; }
    }
}
