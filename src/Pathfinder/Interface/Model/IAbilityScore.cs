using Pathfinder.Enums;

namespace Pathfinder.Interface.Model
{
	public interface IAbilityScore
	{
		AbilityType Type { get; }
		
		int Score { get; }
		int Modifier { get; }

		int Base { get; }
		int Enhanced { get; }
		int Inherent { get; }
		int Temporary { get; }
		int Penalty { get; }

		int MaximumBound { get; }
	}
}
