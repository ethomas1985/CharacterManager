using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System.Collections;
using Pathfinder.Enums;

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
				return 
					new DefenseScore(
						pType,
						pArmorBonus,
						pShieldBonus,
						pDexterityScore,
						(int) pSize,
						pNatural,
						pDeflect,
						pDodge,
						pTemporary,
						pMisc)
					.Score;
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
				return 
					new DefenseScore(
						pBaseAttackBonus,
						pStrengthScore,
						pDexterityScore,
						(int) pSize,
						pDeflect,
						pDodge,
						pTemporary,
						pMisc)
					.Score;
			}

		}

		public static class DefenseScoreTestCase
		{
			public static IEnumerable ScoreCases
			{
				get
				{
					const Size size = Size.Medium;
					var dexScore = new AbilityScore(AbilityType.Dexterity, 10);

					//                                                                      A  S  N  De Do M  T
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10).SetName("ArmorClass -- All Zeros");
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 1, 0, 0, 0, 0, 0, 0).Returns(11).SetName("ArmorClass -- Armor");
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 1, 0, 0, 0, 0, 0).Returns(11).SetName("ArmorClass -- Shield");
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 1, 0, 0, 0, 0).Returns(11).SetName("ArmorClass -- Natural");
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 1, 0, 0, 0).Returns(11).SetName("ArmorClass -- Deflect");
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 1, 0, 0).Returns(11).SetName("ArmorClass -- Dodge");
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 1, 0).Returns(11).SetName("ArmorClass -- Miscellaneous");
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 0, 1).Returns(11).SetName("ArmorClass -- Temporary");

					// Touch ignores Armor (A), Shield (S), and Natural (N) 
					//                                                                 A  S  N  De Do M  T
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10).SetName("Touch -- All Zeros");
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 0, 0, 0, 0, 0, 0).Returns(10).SetName("Touch -- Armor");
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 1, 0, 0, 0, 0, 0).Returns(10).SetName("Touch -- Shield");
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 0, 1, 0, 0, 0, 0).Returns(10).SetName("Touch -- Natural");
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 0, 0, 1, 0, 0, 0).Returns(11).SetName("Touch -- Deflect");
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 0, 0, 0, 1, 0, 0).Returns(11).SetName("Touch -- Dodge");
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 0, 0, 0, 0, 1, 0).Returns(11).SetName("Touch -- Miscellaneous");
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 0, 0, 0, 0, 0, 1).Returns(11).SetName("Touch -- Temporary");

					// Flat-footed ignores Dexterity and DodgeBonus
					//                                                                      A  S  N  De Do M  T
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10).SetName("FlatFooted -- All Zeros");
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 1, 0, 0, 0, 0, 0, 0).Returns(11).SetName("FlatFooted -- Armor");
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 1, 0, 0, 0, 0, 0).Returns(11).SetName("FlatFooted -- Shield");
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 1, 0, 0, 0, 0).Returns(11).SetName("FlatFooted -- Natural");
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 1, 0, 0, 0).Returns(11).SetName("FlatFooted -- Deflect");
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 1, 0, 0).Returns(10).SetName("FlatFooted -- Dodge");
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 0, 1, 0).Returns(11).SetName("FlatFooted -- Miscellaneous");
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 0, 0, 1).Returns(11).SetName("FlatFooted -- Temporary");
				}
			}

			public static IEnumerable CombatManeuverDefenseScoreCases
			{
				get
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity, 10, 0);
					var strScore = new AbilityScore(AbilityType.Strength, 11, 0);
					var size = Size.Medium;

					// Combat Maneuver Defense uses BaseAttackBonus and Strength Modifier and ignore Natural 
					//                                                      Ba N  De Do M  T
					yield return new TestCaseData(dexScore, strScore, size, 0, 0, 0, 0, 0, 0).Returns(10).SetName("CombatManeuverDefense -- All Zeros");
					yield return new TestCaseData(dexScore, strScore, size, 1, 0, 0, 0, 0, 0).Returns(11).SetName("CombatManeuverDefense -- Base Attack Bonus");
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 0, 0, 0).Returns(10).SetName("CombatManeuverDefense -- Natural");
					yield return new TestCaseData(dexScore, strScore, size, 0, 0, 1, 0, 0, 0).Returns(11).SetName("CombatManeuverDefense -- Deflect");
					yield return new TestCaseData(dexScore, strScore, size, 0, 0, 0, 1, 0, 0).Returns(11).SetName("CombatManeuverDefense -- Dodge");
					yield return new TestCaseData(dexScore, strScore, size, 0, 0, 0, 0, 1, 0).Returns(11).SetName("CombatManeuverDefense -- Miscellaneous");
					yield return new TestCaseData(dexScore, strScore, size, 0, 0, 0, 0, 0, 1).Returns(11).SetName("CombatManeuverDefense -- Temporary");
				}
			}
		}
	}
}
