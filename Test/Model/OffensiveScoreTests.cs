using NUnit.Framework;
using Pathfinder.Enum;
using Pathfinder.Model;
using System.Collections;

namespace Test.Model
{
	[TestFixture]
	public class OffensiveScoreTests
	{
		[TestFixture]
		public class ScoreProperty : OffensiveScoreTests
		{
			[Test]
			[TestCaseSource(
				typeof(OffensiveScoreTestCase),
				nameof(OffensiveScoreTestCase.ScoreCases))]
			public int Getter(
				OffensiveType pType,
				int pBaseAttackBonus,
				int pAbilityModifier,
				Size pSize,
				int pMisc,
				int pTemporary)
			{
				var abilityScore = 
					new AbilityScore(
						AbilityType.Strength,
						() => pAbilityModifier * 2 + 10,
						0);
				var offensiveScore = new OffensiveScore(
					pType,
					() => abilityScore,
					() => pBaseAttackBonus,
					() => (int) pSize,
					() => pTemporary)
				{
					MiscModifier = pMisc
				};

				return offensiveScore.Score;
			}
		}

		public static class OffensiveScoreTestCase
		{
			public static IEnumerable ScoreCases
			{
				get
				{
					yield return new TestCaseData(OffensiveType.Melee, 0, 0, Size.Medium, 0, 0).Returns(0);

					yield return new TestCaseData(OffensiveType.Melee, 1, 0, Size.Medium, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.Melee, 0, 1, Size.Medium, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.Melee, 0, 0, Size.Small, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.Melee, 0, 0, Size.Medium, 1, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.Melee, 0, 0, Size.Medium, 0, 1).Returns(1);

					yield return new TestCaseData(OffensiveType.Melee, 1, 1, Size.Medium, 0, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.Melee, 1, 0, Size.Small, 0, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.Melee, 1, 0, Size.Medium, 1, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.Melee, 1, 0, Size.Medium, 0, 1).Returns(2);

					yield return new TestCaseData(OffensiveType.Melee, 1, 1, Size.Small, 0, 0).Returns(3);
					yield return new TestCaseData(OffensiveType.Melee, 1, 1, Size.Medium, 1, 0).Returns(3);
					yield return new TestCaseData(OffensiveType.Melee, 1, 1, Size.Medium, 0, 1).Returns(3);

					yield return new TestCaseData(OffensiveType.Melee, 1, 1, Size.Small, 1, 0).Returns(4);
					yield return new TestCaseData(OffensiveType.Melee, 1, 1, Size.Small, 0, 1).Returns(4);

					yield return new TestCaseData(OffensiveType.Melee, 1, 1, Size.Small, 1, 1).Returns(5);

					yield return new TestCaseData(OffensiveType.Ranged, 0, 0, Size.Medium, 0, 0).Returns(0);
					yield return new TestCaseData(OffensiveType.Ranged, 1, 0, Size.Medium, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.Ranged, 0, 1, Size.Medium, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.Ranged, 0, 0, Size.Small, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.Ranged, 0, 0, Size.Medium, 1, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.Ranged, 0, 0, Size.Medium, 0, 1).Returns(1);

					yield return new TestCaseData(OffensiveType.Ranged, 1, 1, Size.Medium, 0, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.Ranged, 1, 0, Size.Small, 0, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.Ranged, 1, 0, Size.Medium, 1, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.Ranged, 1, 0, Size.Medium, 0, 1).Returns(2);

					yield return new TestCaseData(OffensiveType.Ranged, 1, 1, Size.Small, 0, 0).Returns(3);
					yield return new TestCaseData(OffensiveType.Ranged, 1, 1, Size.Medium, 1, 0).Returns(3);
					yield return new TestCaseData(OffensiveType.Ranged, 1, 1, Size.Medium, 0, 1).Returns(3);

					yield return new TestCaseData(OffensiveType.Ranged, 1, 1, Size.Small, 1, 0).Returns(4);
					yield return new TestCaseData(OffensiveType.Ranged, 1, 1, Size.Small, 0, 1).Returns(4);

					yield return new TestCaseData(OffensiveType.Ranged, 1, 1, Size.Small, 1, 1).Returns(5);

					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 0, 0, Size.Medium, 0, 0).Returns(0);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 0, Size.Medium, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 0, 1, Size.Medium, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 0, 0, Size.Small, 0, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 0, 0, Size.Medium, 1, 0).Returns(1);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 0, 0, Size.Medium, 0, 1).Returns(1);

					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 1, Size.Medium, 0, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 0, Size.Small, 0, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 0, Size.Medium, 1, 0).Returns(2);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 0, Size.Medium, 0, 1).Returns(2);

					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 1, Size.Small, 0, 0).Returns(3);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 1, Size.Medium, 1, 0).Returns(3);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 1, Size.Medium, 0, 1).Returns(3);

					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 1, Size.Small, 1, 0).Returns(4);
					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 1, Size.Small, 0, 1).Returns(4);

					yield return new TestCaseData(OffensiveType.CombatManeuverBonus, 1, 1, Size.Small, 1, 1).Returns(5);
				}
			}
		}
	}
}
