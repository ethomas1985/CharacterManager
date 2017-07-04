using System.Collections;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Model;

namespace Pathfinder.Test.Model
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
				int pTemporary)
			{
				var abilityScore =
					new AbilityScore(
						AbilityType.Strength,
						0, pAbilityModifier * 2 + 10);
				var offensiveScore =
					new OffensiveScore(
						pType,
						abilityScore,
						pBaseAttackBonus,
						(int)pSize,
						pTemporary);

				return offensiveScore.Score;
			}
		}

		public static class OffensiveScoreTestCase
		{
			private static TestCaseData MeleeTestCase(int pBaseAttackBonus, int pAbilityModifier, Size pSize, int pTemporary)
			{
				return new TestCaseData(OffensiveType.Melee, pBaseAttackBonus, pAbilityModifier, pSize, pTemporary);
			}

			private static TestCaseData RangedTestCase(int pBaseAttackBonus, int pAbilityModifier, Size pSize, int pTemporary)
			{
				return new TestCaseData(OffensiveType.Ranged, pBaseAttackBonus, pAbilityModifier, pSize, pTemporary);
			}

			private static TestCaseData CombatManeuverBonusTestCase(int pBaseAttackBonus, int pAbilityModifier, Size pSize, int pTemporary)
			{
				return new TestCaseData(OffensiveType.CombatManeuverBonus, pBaseAttackBonus, pAbilityModifier, pSize, pTemporary);
			}

			public static IEnumerable ScoreCases
			{
				get
				{
					yield return MeleeTestCase(0, 0, Size.Medium, 0).Returns(0);

					yield return MeleeTestCase(1, 0, Size.Medium, 0).Returns(1);
					yield return MeleeTestCase(0, 1, Size.Medium, 0).Returns(1);
					yield return MeleeTestCase(0, 0, Size.Small, 0).Returns(1);
					yield return MeleeTestCase(0, 0, Size.Medium, 1).Returns(1);

					yield return MeleeTestCase(1, 1, Size.Medium, 0).Returns(2);
					yield return MeleeTestCase(1, 0, Size.Small, 0).Returns(2);
					yield return MeleeTestCase(1, 0, Size.Medium, 1).Returns(2);

					yield return MeleeTestCase(1, 1, Size.Small, 0).Returns(3);
					yield return MeleeTestCase(1, 1, Size.Medium, 1).Returns(3);

					yield return MeleeTestCase(1, 1, Size.Small, 1).Returns(4);

					// RANGED TESTS

					yield return RangedTestCase(0, 0, Size.Medium, 0).Returns(0);
					yield return RangedTestCase(1, 0, Size.Medium, 0).Returns(1);
					yield return RangedTestCase(0, 1, Size.Medium, 0).Returns(1);
					yield return RangedTestCase(0, 0, Size.Small, 0).Returns(1);
					yield return RangedTestCase(0, 0, Size.Medium, 1).Returns(1);

					yield return RangedTestCase(1, 1, Size.Medium, 0).Returns(2);
					yield return RangedTestCase(1, 0, Size.Small, 0).Returns(2);
					yield return RangedTestCase(1, 0, Size.Medium, 1).Returns(2);

					yield return RangedTestCase(1, 1, Size.Small, 0).Returns(3);
					yield return RangedTestCase(1, 1, Size.Medium, 1).Returns(3);

					yield return RangedTestCase(1, 1, Size.Small, 1).Returns(4);

					// COMBAT MANEUVER BONUS TESTS

					yield return CombatManeuverBonusTestCase(0, 0, Size.Medium, 0).Returns(0);
					yield return CombatManeuverBonusTestCase(1, 0, Size.Medium, 0).Returns(1);
					yield return CombatManeuverBonusTestCase(0, 1, Size.Medium, 0).Returns(1);
					yield return CombatManeuverBonusTestCase(0, 0, Size.Small, 0).Returns(1);
					yield return CombatManeuverBonusTestCase(0, 0, Size.Medium, 1).Returns(1);

					yield return CombatManeuverBonusTestCase(1, 1, Size.Medium, 0).Returns(2);
					yield return CombatManeuverBonusTestCase(1, 0, Size.Small, 0).Returns(2);
					yield return CombatManeuverBonusTestCase(1, 0, Size.Medium, 1).Returns(2);

					yield return CombatManeuverBonusTestCase(1, 1, Size.Small, 0).Returns(3);
					yield return CombatManeuverBonusTestCase(1, 1, Size.Medium, 1).Returns(3);

					yield return CombatManeuverBonusTestCase(1, 1, Size.Small, 1).Returns(4);
				}
			}
		}
	}
}
