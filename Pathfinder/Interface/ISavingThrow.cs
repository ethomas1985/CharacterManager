using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface ISavingThrow
	{
		SavingThrowType Type { get; }
		IAbilityScore Ability { get; }

		int Score { get; }

		int Base { get; }

		int AbilityModifier { get; }
		int Resist { get; }

		int Misc { get; }
		int Temporary { get; }
	}
}
