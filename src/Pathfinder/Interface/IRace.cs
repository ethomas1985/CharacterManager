using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface IRace : INamed
	{
		Size Size { get; }
		int BaseSpeed { get; }
		IDictionary<AbilityType, int> AbilityScores { get; }
		IEnumerable<ITrait> Traits { get; }
		IEnumerable<ILanguage> Languages { get; }
		string Description { get; }
	}
}