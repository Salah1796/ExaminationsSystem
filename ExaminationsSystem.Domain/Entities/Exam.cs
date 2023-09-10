using ExaminationsSystem.Domain.Common.Contracts;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Domain.Entities
{
    public class Exam : IEntityIdentity<Guid>, IDeletionSignature
        , IModificatioDateSignature, ICreationDateSignature
    {
        #region IEntityIdentity

        public Guid Id { get; set; }

        #endregion

        #region IDeletionSignature

        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public bool? MustDeletedPhysical { get; set; }

        #endregion

        #region ICreationDateSignature

        public DateTime CreationDate { get; set; }

        #endregion

        #region IModificatioDateSignature

        public DateTime? FirstModificationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

        #endregion

        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public virtual ICollection<ExamQuestion> Questions { get; set; }
        public virtual ICollection<StudentExam> Students { get; set; }

    }
}