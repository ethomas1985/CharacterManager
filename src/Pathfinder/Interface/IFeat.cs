using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface IFeat
	{
		string Name { get; }
		FeatType FeatType { get; }
		IEnumerable<string> Prerequisites { get; }
		string Description { get; }
		string Benefit { get; }
		string Special { get; }
	}
}