#region Using ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
namespace ExaminationsSystem.Domain.Common.Contracts
{
	public interface IEntityModifiedUserSignature : IEntityCreatedUserSignature
	{
		#region Properties
		Guid? FirstModifiedByUserId { get; set; }
		Guid? LastModifiedByUserId { get; set; }
		#endregion
	}
}
