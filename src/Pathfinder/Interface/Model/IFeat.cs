using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface.Model
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