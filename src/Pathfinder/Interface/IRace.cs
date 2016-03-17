using System.Collections.Generic;
using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface IRace
	{
		string Name { get; }
		Size Size { get; }
		int BaseSpeed { get; }
		IDictionary<AbilityType, int> AbilityScores { get; }
		IEnumerable<ITrait> Traits { get; }
		IEnumerable<ILanguage> Languages { get; }
		string Description { get; }
	}
}