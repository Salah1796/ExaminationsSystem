using ExaminationsSystem.Domain.Common.Contracts;
using System;

namespace ExaminationsSystem.Domain.Entities
{
    public class StudentSelectedOption : IEntityIdentity<Guid>
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

        public Guid StudentAnswerId { get; set; }
        public Guid OptionId { get; set; }
        public virtual StudentAnswer StudentAnswer { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }
    }
}