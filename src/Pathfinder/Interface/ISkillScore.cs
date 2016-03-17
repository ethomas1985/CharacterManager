using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface ISkillScore
	{
		string Name { get; }

		string Type { get; }

		AbilityType Ability { get; }

		int Total { get; }
		int Ranks { get; }
		int AbilityModifier { get; }
		int ClassModifier { get; }
		int MiscModifier { get; }
		int TemporaryModifier { get; }
		int ArmorClassPenalty { get; }
	}
}
