using ExaminationsSystem.Domain.Common.Contracts;
using System;

namespace ExaminationsSystem.Domain.Entities
{
    public class Student : IEntityIdentity<Guid>, IDeletionSignature
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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}