using System.Collections;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Model;

namespace Pathfinder.Test.Model
{
	[TestFixture]
	public class AbilityScoreTests
	{
		[TestFixture]
		public class ModifierPropertyTests : AbilityScoreTests
		{
			[Test]
			public void WithMaximumBound()
			{
				var abilityScore = new AbilityScore(AbilityType.Strength, 18, 0, 0, 0, 0, 1);

				Assert.That(abilityScore.Modifier, Is.EqualTo(1));
			}

			[Test]
			[TestCaseSource(typeof(AbilityScoreTestCase), nameof(AbilityScoreTestCase.ModifierCases))]
			public int Getter(
					int pBase,
					int pEnhanced,
					int pInherent,
					int pTemporary,
					int pPenalty,
					int pMaximumBound = -1)
			{
				return new AbilityScore(
					AbilityType.Strength,
					pBase,
					pEnhanced: pEnhanced,
					pInherent: pInherent,
					pTemporaryModifier: pTemporary,
					pPenalty: pPenalty,
					pMaximumBound: pMaximumBound)
				.Modifier;
			}
		}

		[TestFixture]
		public class ScorePropertyTests : AbilityScoreTests
		{
			[Test]
			[TestCaseSource(typeof(AbilityScoreTestCase), nameof(AbilityScoreTestCase.ScoreCases))]
			public int Getter(
				int pBase,
				int pEnhanced,
				int pInherent,
				int pTemporary,
				int pPenalty)
			{
				return new AbilityScore(
					AbilityType.Strength,
					pBase,
					pEnhanced: pEnhanced,
					pInherent: pInherent,
					pTemporaryModifier: pTemporary,
					pPenalty: pPenalty)
				.Score;
			}
		}

		public static class AbilityScoreTestCase
		{
			public static IEnumerable ModifierCases
			{
				get
				{
					// Ability Score = 1
					yield return new TestCaseData(1, 0, 0, 0, 0, -1).Returns(-5).SetName("0 <= Score <= 1 := -5 Modifier");
					// 2 <= Ability Score <= 3
					yield return new TestCaseData(2, 0, 0, 0, 0, -1).Returns(-4).SetName("1 <= Score <= 3 := -4 Modifier");
					// 4 <= Ability Score <= 5
					yield return new TestCaseData(4, 0, 0, 0, 0, -1).Returns(-3).SetName("3 <= Score <= 5 := -3 Modifier");
					// 6 <= Ability Score <= 7
					yield return new TestCaseData(6, 0, 0, 0, 0, -1).Returns(-2).SetName("5 <= Score <= 7 := -2 Modifier");
					// 8 <= Ability Score <= 9
					yield return new TestCaseData(8, 0, 0, 0, 0, -1).Returns(-1).SetName("7 <= Score <= 9 := -1 Modifier");
					// 10 <= Ability Score <= 11
					yield return new TestCaseData(10, 0, 0, 0, 0, -1).Returns(0).SetName("9 <= Score <= 11 := 0 Modifier");
					// 12 <= Ability Score <= 13
					yield return new TestCaseData(12, 0, 0, 0, 0, -1).Returns(1).SetName("11 <= Score <= 13 := 1 Modifier");
					// 14 <= Ability Score <= 15
					yield return new TestCaseData(14, 0, 0, 0, 0, -1).Returns(2).SetName("13 <= Score <= 15 := 2 Modifier");
					// 16 <= Ability Score <= 17
					yield return new TestCaseData(16, 0, 0, 0, 0, -1).Returns(3).SetName("15 <= Score <= 17 := 3 Modifier");
					// 18 <= Ability Score <= 19
					yield return new TestCaseData(18, 0, 0, 0, 0, -1).Returns(4).SetName("17 <= Score <= 19 := 4 Modifier");
				}
			}

			private const string ONE_BASE = "1 Base";
			private const string ONE_ENHANCED = "1 Enhanced";
			private const string ONE_INHERENT = "1 Inherent";
			private const string ONE_TEMPORARY = "1 Temporary";
			private const string ONE_PENALTY = "1 Penalty";
			public static IEnumerable ScoreCases
			{
				get
				{
					/*
					 * B := Base
					 * E := Enhanced
					 * I := Inherent
					 * T := {oneTemporary}
					 * P := Penalty
					 * S := Score
					 */
					//							 +B +E +I +T -P         = S
					yield return new TestCaseData(0, 0, 0, 0, 0).Returns(0).SetName($"All Zero");
					yield return new TestCaseData(0, 0, 0, 0, 1).Returns(-1).SetName($"{ONE_PENALTY}");
					yield return new TestCaseData(0, 0, 0, 1, 0).Returns(1).SetName($"{ONE_TEMPORARY}");
					yield return new TestCaseData(0, 0, 0, 1, 1).Returns(0).SetName($"{ONE_TEMPORARY} & {ONE_PENALTY}");
					yield return new TestCaseData(0, 0, 1, 0, 0).Returns(1).SetName($"{ONE_INHERENT}");
					yield return new TestCaseData(0, 0, 1, 0, 1).Returns(0).SetName($"{ONE_INHERENT} & {ONE_PENALTY}");
					yield return new TestCaseData(0, 0, 1, 1, 0).Returns(2).SetName($"{ONE_INHERENT} & {ONE_TEMPORARY}");
					yield return new TestCaseData(0, 0, 1, 1, 1).Returns(1).SetName($"{ONE_INHERENT} & {ONE_TEMPORARY} & {ONE_PENALTY}");
					yield return new TestCaseData(0, 1, 0, 0, 0).Returns(1).SetName($"{ONE_ENHANCED}");
					yield return new TestCaseData(0, 1, 0, 0, 1).Returns(0).SetName($"{ONE_ENHANCED} & {ONE_PENALTY}");
					yield return new TestCaseData(0, 1, 0, 1, 0).Returns(2).SetName($"{ONE_ENHANCED} & {ONE_TEMPORARY}");
					yield return new TestCaseData(0, 1, 0, 1, 1).Returns(1).SetName($"{ONE_ENHANCED} & {ONE_TEMPORARY} & {ONE_PENALTY}");
					yield return new TestCaseData(0, 1, 1, 0, 0).Returns(2).SetName($"{ONE_ENHANCED} & {ONE_INHERENT}");
					yield return new TestCaseData(0, 1, 1, 0, 1).Returns(1).SetName($"{ONE_ENHANCED} & {ONE_INHERENT} & {ONE_PENALTY}");
					yield return new TestCaseData(0, 1, 1, 1, 0).Returns(3).SetName($"{ONE_ENHANCED} & {ONE_INHERENT} & {ONE_TEMPORARY}");
					yield return new TestCaseData(0, 1, 1, 1, 1).Returns(2).SetName($"{ONE_ENHANCED} & {ONE_INHERENT} & {ONE_TEMPORARY} & {ONE_PENALTY}");
					yield return new TestCaseData(1, 0, 0, 0, 0).Returns(1).SetName($"{ONE_BASE}");
					yield return new TestCaseData(1, 0, 0, 0, 1).Returns(0).SetName($"{ONE_BASE} & {ONE_PENALTY}");
					yield return new TestCaseData(1, 0, 0, 1, 0).Returns(2).SetName($"{ONE_BASE} & {ONE_TEMPORARY}");
					yield return new TestCaseData(1, 0, 0, 1, 1).Returns(1).SetName($"{ONE_BASE} & {ONE_TEMPORARY} & {ONE_PENALTY}");
					yield return new TestCaseData(1, 0, 1, 0, 0).Returns(2).SetName($"{ONE_BASE} & {ONE_INHERENT}");
					yield return new TestCaseData(1, 0, 1, 0, 1).Returns(1).SetName($"{ONE_BASE} & {ONE_INHERENT} & {ONE_PENALTY}");
					yield return new TestCaseData(1, 0, 1, 1, 0).Returns(3).SetName($"{ONE_BASE} & {ONE_INHERENT} & {ONE_TEMPORARY}");
					yield return new TestCaseData(1, 0, 1, 1, 1).Returns(2).SetName($"{ONE_BASE} & {ONE_INHERENT} & {ONE_TEMPORARY}");
					yield return new TestCaseData(1, 1, 0, 0, 0).Returns(2).SetName($"{ONE_BASE} & {ONE_ENHANCED}");
					yield return new TestCaseData(1, 1, 0, 0, 1).Returns(1).SetName($"{ONE_BASE} & {ONE_ENHANCED} & {ONE_PENALTY}");
					yield return new TestCaseData(1, 1, 0, 1, 0).Returns(3).SetName($"{ONE_BASE} & {ONE_ENHANCED} & {ONE_TEMPORARY}");
					yield return new TestCaseData(1, 1, 0, 1, 1).Returns(2).SetName($"{ONE_BASE} & {ONE_ENHANCED} & {ONE_TEMPORARY} & {ONE_PENALTY}");
					yield return new TestCaseData(1, 1, 1, 0, 0).Returns(3).SetName($"{ONE_BASE} & {ONE_ENHANCED} & {ONE_INHERENT}");
					yield return new TestCaseData(1, 1, 1, 0, 1).Returns(2).SetName($"{ONE_BASE} & {ONE_ENHANCED} & {ONE_INHERENT} & {ONE_PENALTY}");
					yield return new TestCaseData(1, 1, 1, 1, 0).Returns(4).SetName($"{ONE_BASE} & {ONE_ENHANCED} & {ONE_INHERENT} & {ONE_TEMPORARY}");
					yield return new TestCaseData(1, 1, 1, 1, 1).Returns(3).SetName($"{ONE_BASE} & {ONE_ENHANCED} & {ONE_INHERENT} & {ONE_TEMPORARY} & {ONE_PENALTY}");
				}
			}
		}
	}
}
