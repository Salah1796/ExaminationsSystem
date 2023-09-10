using ExaminationsSystem.Domain.Common.Contracts;
using System;

namespace ExaminationsSystem.Domain.Entities
{
    public class QuestionOption : IEntityIdentity<Guid>
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

        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}