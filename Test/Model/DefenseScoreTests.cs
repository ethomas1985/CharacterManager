using NUnit.Framework;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Model;
using System.Collections;

namespace Test.Model
{
	[TestFixture]
	public class DefenseScoreTests
	{
		[TestFixture]
		public class ScorePropertyTests : DefenseScoreTests
		{
			[Test]
			[TestCaseSource(
				typeof(DefenseScoreTestCase),
				nameof(DefenseScoreTestCase.ScoreCases))]
			public int Getter(
				DefensiveType pType,
				IAbilityScore pDexterityScore,
				Size pSize,
				int pArmorBonus,
				int pShieldBonus,
				int pNatural,
				int pDeflect,
				int pDodge,
				int pMisc,
				int pTemporary)
			{
				return new DefenseScore(
					pType,
					() => pArmorBonus,
					() => pShieldBonus,
					() => pDexterityScore,
					() => (int) pSize,
					() => pNatural,
					() => pDeflect,
					() => pDodge,
					() => pTemporary)
				{
					MiscModifier = pMisc
				}.Score;
			}

			[Test]
			[TestCaseSource(
				typeof(DefenseScoreTestCase),
				nameof(DefenseScoreTestCase.CombatManeuverDefenseScoreCases))]
			public int Getter_CombatManueverDefense(
				IAbilityScore pDexterityScore,
				IAbilityScore pStrengthScore,
				Size pSize,
				int pBaseAttackBonus,
				int pNatural,
				int pDeflect,
				int pDodge,
				int pMisc,
				int pTemporary)
			{
				return new DefenseScore(
					() => pBaseAttackBonus,
					() => pStrengthScore,
					() => pDexterityScore,
					() => (int) pSize,
					() => pDeflect,
					() => pDodge,
					() => pTemporary)
				{
					MiscModifier = pMisc
				}.Score;
			}

		}

		public static class DefenseScoreTestCase
		{
			public static IEnumerable ScoreCases
			{
				get
				{
					const Size size = Size.Medium;
					var dexScore = new AbilityScore(AbilityType.Dexterity, () => 0, 10);

					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 1, 0, 0, 0, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 1, 0, 0, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 1, 0, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 1, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 1, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 0, 1).Returns(11);

					// Touch ignores Armor, Shield, and Natural 
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 1, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 1, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 0, 1).Returns(11);

					// Flat-footed ignores Dexterity and Dodge
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 1, 0, 0, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 1, 0, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 1, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 1, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 1, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 1, 1, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 1, 0, 1).Returns(11);
				}
			}

			public static IEnumerable CombatManeuverDefenseScoreCases
			{
				get
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity, () => 0, 10);
					var strScore = new AbilityScore(AbilityType.Strength, () => 0, 11);
					var size = Size.Medium;

					// Combat Maneuver Defense uses BaseAttackBonus and Strength Modifier and ignore Natural 
					yield return new TestCaseData(dexScore, strScore, size, 0, 0, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(dexScore, strScore, size, 1, 1, 0, 0, 0, 0).Returns(11);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 1, 0, 0, 0).Returns(11);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 0, 1, 0).Returns(11);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 0, 0, 1).Returns(11);
				}
			}
		}
	}
}
