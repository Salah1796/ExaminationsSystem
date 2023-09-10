#region Using ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace ExaminationsSystem.Domain.Common.Contracts
{
	public interface IEntityCreatedUserSignature
	{
		#region Properties
		Guid CreatedByUserId { get; set; }
		#endregion
	}
}
