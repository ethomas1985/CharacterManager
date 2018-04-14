using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class SkillMother
	{
		public const string SKILL_NAME = "Test Skill";

		public static ISkill Create()
		{
			return new Skill(
				SKILL_NAME,
				AbilityType.Strength,
				false,
				false,
				"Testing Description",
				"Testing Check",
				"Testing Action",
				"Testing Try Again",
				"Testing Special",
				"Testing Restriction",
				"Testing Untrained");
		}
	}
}
