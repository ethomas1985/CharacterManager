using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class FeatMother
	{
		public static IFeat CreateTestingFeat1()
		{
			return new Feat(
				"Feat 1",
				FeatType.General,
				new List<string>(),
				"Testing Description",
				"Testing Benefit",
				"Testing Special");;
		}

		public static IFeat CreateTestingFeat2()
		{
			return new Feat(
					"Feat 2",
					FeatType.General,
					new List<string> { "Feat 1" },
					"Testing Description",
					"Testing Benefit",
					"Testing Special");
		}
	}
}
