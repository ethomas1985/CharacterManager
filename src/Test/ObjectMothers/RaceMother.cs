using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class RaceMother
	{
		public static IRace Create()
		{
			return
				new Race(
					"Test Race",
					"Testy",
					"This is a Test Race",
					Size.Medium,
					30,
					new Dictionary<AbilityType, int>(),
					new List<ITrait>
					{
						TraitMother.PlusOneStrength(),

						TraitMother.PlusFiveStrengthConditional()
					},
					new List<ILanguage>
					{
						LanguageMother.OldTestese(),
						LanguageMother.OldTestist()
					});
		}
	}
}
