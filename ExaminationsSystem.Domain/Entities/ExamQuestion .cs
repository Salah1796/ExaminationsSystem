using ExaminationsSystem.Domain.Common.Contracts;
using System;

namespace ExaminationsSystem.Domain.Entities
{
    public class ExamQuestion : IEntityIdentity<Guid>
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
        public Guid ExamId { get; set; }
        public decimal Points { get; set; }
        public virtual Question Question { get; set; }
        public virtual Exam Exam { get; set; }
    }
}