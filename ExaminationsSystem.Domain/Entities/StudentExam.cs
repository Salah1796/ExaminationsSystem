using ExaminationsSystem.Domain.Common.Contracts;
using ExaminationsSystem.Domain.Common.Enums;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Domain.Entities
{
    public class StudentExam : IEntityIdentity<Guid>, IDeletionSignature
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
        #region IDeletionSignature

        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public bool? MustDeletedPhysical { get; set; }

        #endregion

        public Guid ExamId { get; set; }
        public Guid StudentId { get; set; }
        public decimal AutomaticGradeScore { get; set; }
        public decimal ManualGradeScore { get; set; }
        public decimal TotalScore { get; set; }
        public StudentExamStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAutomaticGraded { get; set; }
        public DateTime? AutomaticGradeDate { get; set; }
        public bool IsManualGraded { get; set; }
        public DateTime? ManualGradeDate { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<StudentAnswer> Answers { get; set; }
    }
}