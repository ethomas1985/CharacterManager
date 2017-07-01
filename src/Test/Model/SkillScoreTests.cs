using System;
using System.Collections;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Model
{
	[TestFixture]
	public class SkillScoreTests
	{
		[TestFixture]
		public class TotalPropertyTests : SkillScoreTests
		{
			[Test]
			public void TestThrows()
			{
				Assert.That(
					() => new SkillScore(TestCase.Skill, TestCase.BadAbilityScore, 0, 0, 0, 0, 0),
					Throws.Exception.TypeOf(typeof(Exception)));
			}

			[Test]
			[TestCaseSource(typeof(TestCase), nameof(TestCase.Cases))]
			public int Test(
				ISkill pSkill,
				IAbilityScore pAbilityScore,
				int pRanks,
				int pClassModifier,
				int pMiscModifier,
				int pTemporaryModifier,
				int pArmorClassPenalty)
			{
				return new SkillScore(
					pSkill,
					pAbilityScore,
					pRanks,
					pClassModifier,
					pMiscModifier,
					pTemporaryModifier,
					pArmorClassPenalty)
				.Total;
			}
		}
	}

	internal static class TestCase
	{
		public static readonly Skill Skill =
			new Skill("Test",
				AbilityType.Strength,
				true,
				true,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty);

		internal static readonly AbilityScore AbilityScore = new AbilityScore(
																			  AbilityType.Strength,
																			  10);

		internal static readonly AbilityScore BadAbilityScore = new AbilityScore(
																				 AbilityType.Dexterity,
																				 10);

		public static IEnumerable Cases
		{
			get
			{
				yield return new TestCaseData(Skill, AbilityScore, 0, 0, 0, 0, 0).Returns(0).SetName("All Zero");

				yield return new TestCaseData(Skill, AbilityScore, 1, 0, 0, 0, 0).Returns(1).SetName("1 Rank");
				yield return new TestCaseData(Skill, AbilityScore, 0, 1, 0, 0, 0).Returns(1).SetName("1 Class Modifier");
				yield return new TestCaseData(Skill, AbilityScore, 0, 0, 1, 0, 0).Returns(1).SetName("1 Miscellaneous");
				yield return new TestCaseData(Skill, AbilityScore, 0, 0, 0, 1, 0).Returns(1).SetName("1 Temporary");
				yield return new TestCaseData(Skill, AbilityScore, 0, 0, 0, 0, -1).Returns(-1).SetName("-1 Armor Class Penalty");

				yield return new TestCaseData(Skill, AbilityScore, 1, 1, 0, 0, 0).Returns(2).SetName("1 Rank & 1 Class Modifier");
				yield return new TestCaseData(Skill, AbilityScore, 1, 0, 1, 0, 0).Returns(2).SetName("1 Rank & 1 Miscellaneous");
				yield return new TestCaseData(Skill, AbilityScore, 1, 0, 0, 1, 0).Returns(2).SetName("1 Rank & 1 Temporary");
				yield return new TestCaseData(Skill, AbilityScore, 1, 0, 0, 0, -1).Returns(0).SetName("1 Rank & -1 Armor Class Penalty");

				yield return new TestCaseData(Skill, AbilityScore, 1, 1, 1, 0, 0).Returns(3).SetName("1 Rank & 1 Class Modifier & 1 Miscellaneous");
				yield return new TestCaseData(Skill, AbilityScore, 1, 1, 0, 1, 0).Returns(3).SetName("1 Rank & 1 Class Modifier & 1 Temporary");
				yield return new TestCaseData(Skill, AbilityScore, 1, 1, 0, 0, -1).Returns(1).SetName("1 Rank & 1 Class Modifier & -1 Armor Class Penalty");

				yield return new TestCaseData(Skill, AbilityScore, 1, 1, 1, 1, 0).Returns(4).SetName("1 Rank & 1 Class Modifier & 1 Miscellaneous & 1 Temporary");
				yield return new TestCaseData(Skill, AbilityScore, 1, 1, 1, 0, -1).Returns(2).SetName("1 Rank & 1 Class Modifier & 1 Miscellaneous & -1 Armor Class Penalty");

				yield return new TestCaseData(Skill, AbilityScore, 1, 1, 1, 1, -1).Returns(3).SetName("1 Rank & 1 Class Modifier & 1 Miscellaneous & 1 Temporary & -1 Armor Class Penalty");
			}
		}
	}
}
