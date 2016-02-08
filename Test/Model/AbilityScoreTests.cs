using System;
using NUnit.Framework;
using Pathfinder.Model;
using Pathfinder.Enum;
using Pathfinder.Interface;
using System.Collections;

namespace Test.Model
{
	[TestFixture]
	public class AbilityScoreTests
	{
		[TestFixture]
		public class ModifierPropertyTests : AbilityScoreTests
		{
			[Test]
			[TestCaseSource(typeof(AbilityScoreTestCase), nameof(AbilityScoreTestCase.ModifierCases))]
			public int Getter(
				int Base,
				int Enhanced,
				int Inherent,
				int Temporary,
				int Penalty)
			{
				return new AbilityScore(AbilityType.Strength)
				{
					Base = Base,
					Enhanced = Enhanced,
					Inherent = Inherent,
					Temporary =Temporary,
					Penalty = Penalty
				}.Modifier;
			}
		}

		[TestFixture]
		public class ScorePropertyTests : AbilityScoreTests
		{
			[Test]
			[TestCaseSource(typeof(AbilityScoreTestCase), nameof(AbilityScoreTestCase.ScoreCases))]
			public int Getter(
				int Base,
				int Enhanced,
				int Inherent,
				int Temporary,
				int Penalty)
			{
				return new AbilityScore(AbilityType.Strength)
				{
					Base = Base,
					Enhanced = Enhanced,
					Inherent = Inherent,
					Temporary = Temporary,
					Penalty = Penalty
				}.Score;
			}
		}

		public static class AbilityScoreTestCase
		{
			public static IEnumerable ModifierCases
			{
				get
				{
					// Ability Score = 1
					yield return new TestCaseData(1, 0, 0, 0, 0).Returns(-5);
					// 2 <= Ability Score <= 3
					yield return new TestCaseData(2, 0, 0, 0, 0).Returns(-4);
					// 4 <= Ability Score <= 5
					yield return new TestCaseData(4, 0, 0, 0, 0).Returns(-3);
					// 6 <= Ability Score <= 7
					yield return new TestCaseData(6, 0, 0, 0, 0).Returns(-2);
					// 8 <= Ability Score <= 9
					yield return new TestCaseData(8, 0, 0, 0, 0).Returns(-1);
					// 10 <= Ability Score <= 11
					yield return new TestCaseData(10, 0, 0, 0, 0).Returns(0);
					// 12 <= Ability Score <= 13
					yield return new TestCaseData(12, 0, 0, 0, 0).Returns(1);
					// 14 <= Ability Score <= 15
					yield return new TestCaseData(14, 0, 0, 0, 0).Returns(2);
					// 16 <= Ability Score <= 17
					yield return new TestCaseData(16, 0, 0, 0, 0).Returns(3);
					// 18 <= Ability Score <= 19
					yield return new TestCaseData(18, 0, 0, 0, 0).Returns(4);
				}
			}

			public static IEnumerable ScoreCases
			{
				get
				{
					//							 +a +b +c +d -e         =x
					yield return new TestCaseData(0, 0, 0, 0, 0).Returns(0);
					yield return new TestCaseData(0, 0, 0, 0, 1).Returns(-1);
					yield return new TestCaseData(0, 0, 0, 1, 0).Returns(1);
					yield return new TestCaseData(0, 0, 0, 1, 1).Returns(0);
					yield return new TestCaseData(0, 0, 1, 0, 0).Returns(1);
					yield return new TestCaseData(0, 0, 1, 0, 1).Returns(0);
					yield return new TestCaseData(0, 0, 1, 1, 0).Returns(2);
					yield return new TestCaseData(0, 0, 1, 1, 1).Returns(1);
					yield return new TestCaseData(0, 1, 0, 0, 0).Returns(1);
					yield return new TestCaseData(0, 1, 0, 0, 1).Returns(0);
					yield return new TestCaseData(0, 1, 0, 1, 0).Returns(2);
					yield return new TestCaseData(0, 1, 0, 1, 1).Returns(1);
					yield return new TestCaseData(0, 1, 1, 0, 0).Returns(2);
					yield return new TestCaseData(0, 1, 1, 0, 1).Returns(1);
					yield return new TestCaseData(0, 1, 1, 1, 0).Returns(3);
					yield return new TestCaseData(0, 1, 1, 1, 1).Returns(2);
					yield return new TestCaseData(1, 0, 0, 0, 0).Returns(1);
					yield return new TestCaseData(1, 0, 0, 0, 1).Returns(0);
					yield return new TestCaseData(1, 0, 0, 1, 0).Returns(2);
					yield return new TestCaseData(1, 0, 0, 1, 1).Returns(1);
					yield return new TestCaseData(1, 0, 1, 0, 0).Returns(2);
					yield return new TestCaseData(1, 0, 1, 0, 1).Returns(1);
					yield return new TestCaseData(1, 0, 1, 1, 0).Returns(3);
					yield return new TestCaseData(1, 0, 1, 1, 1).Returns(2);
					yield return new TestCaseData(1, 1, 0, 0, 0).Returns(2);
					yield return new TestCaseData(1, 1, 0, 0, 1).Returns(1);
					yield return new TestCaseData(1, 1, 0, 1, 0).Returns(3);
					yield return new TestCaseData(1, 1, 0, 1, 1).Returns(2);
					yield return new TestCaseData(1, 1, 1, 0, 0).Returns(3);
					yield return new TestCaseData(1, 1, 1, 0, 1).Returns(2);
					yield return new TestCaseData(1, 1, 1, 1, 0).Returns(4);
					yield return new TestCaseData(1, 1, 1, 1, 1).Returns(3);
				}
			}
		}
	}
}
