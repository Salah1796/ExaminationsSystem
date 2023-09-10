#region Using ...
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace ExaminationsSystem.Domain.Common.Contracts
{
	public interface IDeletionSignature
	{
		#region Properties
		 bool IsDeleted { get; set; }
		 DateTime? DeletionDate { get; set; }
		 Guid? DeletedByUserId { get; set; }
		bool? MustDeletedPhysical { get; set; }
		#endregion
	}
}
