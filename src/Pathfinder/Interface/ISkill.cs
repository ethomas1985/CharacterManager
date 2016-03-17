using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface ISkill
	{
		string Name { get; }

		AbilityType AbilityType { get; }

		string KeyAbility { get; }
		string TrainedOnly { get; }
		string ArmorCheckPenalty { get; }
		string Description { get; }
		string Check { get; }
		string Action { get; }
		string TryAgain { get; }
		string Special { get; }
		string Restriction { get; }
		string Untrained { get; }
	}
}