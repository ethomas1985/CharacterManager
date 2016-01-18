using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface IAbilityScore
	{
		AbilityType Ability { get; }
		
		int Score { get; }
		int Modifier { get; }

		int Base { get; }
		int Enhanced { get; }
		int Inherent { get; }
		int Temporary { get; }
		int Penalty { get; }
	}
}
