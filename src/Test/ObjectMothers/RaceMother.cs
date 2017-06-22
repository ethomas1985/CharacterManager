using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class RaceMother
	{
		public static IRace Create()
		{
			return new Race(
				"Test Race",
				"Test-ish",
				"This is a Test Race Description.",
				Size.Medium,
				30,
				new Dictionary<AbilityType, int>(),
				new List<ITrait>(),
				new List<ILanguage>
				{
					new Language("Test Language")
				});
		}
	}
}
