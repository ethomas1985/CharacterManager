using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface IOffensiveScore
	{
		OffensiveType Type { get; }

		int Score { get; }

		int BaseAttackBonus { get; }

		IAbilityScore Ability { get; }
		int AbilityModifier { get; }

		int SizeModifier { get; }

		int MiscModifier { get; }

		int TemporaryModifier { get; }
	}
}
