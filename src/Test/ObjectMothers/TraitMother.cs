using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class TraitMother
	{
		public static ITrait PlusOneStrength()
		{
			return new Trait("+1 Strength", "+1 Strength", false, new Dictionary<string, int> { [nameof(AbilityType.Strength)] = 1 });
		}

		public static ITrait PlusFiveStrengthConditional()
		{
			return new Trait("+5 Strength", "+5 Strength", true, new Dictionary<string, int> { [nameof(AbilityType.Strength)] = 5 });
		}
	}
}