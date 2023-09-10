#region Using ...
using ExaminationsSystem.Application.Common.Models;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels.Exams
{
    public class ExamSearchModel : BaseFilter
    {
        public string Name { get; set; }
    }
}
