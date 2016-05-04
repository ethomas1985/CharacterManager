using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface ISkill : INamed
	{
		AbilityType AbilityType { get; }

		bool TrainedOnly { get; }
		bool ArmorCheckPenalty { get; }
		string Description { get; }
		string Check { get; }
		string Action { get; }
		string TryAgain { get; }
		string Special { get; }
		string Restriction { get; }
		string Untrained { get; }
	}
}