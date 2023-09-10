using ExaminationsSystem.Domain.Common.Contracts;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Domain.Entities
{
    public class StudentAnswer : IEntityIdentity<Guid>
        , IModificatioDateSignature, ICreationDateSignature
    {
        #region IEntityIdentity

        public Guid Id { get; set; }

        #endregion

        #region ICreationDateSignature

        public DateTime CreationDate { get; set; }

        #endregion

        #region IModificatioDateSignature

        public DateTime? FirstModificationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

        #endregion

        public Guid QuestionId { get; set; }
        public Guid StudentExamId { get; set; }
        public string? AnswerText { get; set; }
        public bool IsGraded { get; set; }

        public virtual Question Question { get; set; }
        public virtual StudentExam StudentExam { get; set; }
        public virtual ICollection<StudentSelectedOption> SelectedOptions { get; set; }

    }
}