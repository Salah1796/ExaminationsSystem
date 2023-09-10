using ExaminationsSystem.Domain.Common.Contracts;
using ExaminationsSystem.Domain.Common.Enums;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Domain.Entities
{
    public class Question : IEntityIdentity<Guid>, IDeletionSignature
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

        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public virtual ICollection<QuestionOption> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}