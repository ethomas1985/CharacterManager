using Pathfinder.Enums;
using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface IFeat: INamed
	{
		FeatType FeatType { get; }

		bool IsSpecialized { get; }
		string Specialization { get; }

		IEnumerable<string> Prerequisites { get; }
		string Description { get; }
		string Benefit { get; }
		string Special { get; }
	}
}