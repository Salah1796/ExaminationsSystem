#region Using ...
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace ExaminationsSystem.Domain.Common.Contracts
{
	public interface IEntityIdentity<TPrimeryKey>
	{
	   TPrimeryKey Id { get; set; }
	}
}
