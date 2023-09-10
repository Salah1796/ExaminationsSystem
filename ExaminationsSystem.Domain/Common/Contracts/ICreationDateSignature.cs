#region Using ...
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace ExaminationsSystem.Domain.Common.Contracts
{
	public interface ICreationDateSignature
	{
		#region Properties
		DateTime CreationDate { get; set; }
		#endregion
	}
}
