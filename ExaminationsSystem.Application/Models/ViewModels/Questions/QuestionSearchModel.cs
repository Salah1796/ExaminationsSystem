#region Using ...
using ExaminationsSystem.Application.Common.Models;
using ExaminationsSystem.Domain.Common.Enums;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels.Questions
{
    public class QuestionSearchModel : BaseFilter
    {
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
    }
}
