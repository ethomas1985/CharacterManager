using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface ISkillScore
	{
		ISkill Skill { get; }
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
