using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class SkillMother
	{
		public static ISkill Create()
		{
			return new Skill(
				"Test Skill",
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
