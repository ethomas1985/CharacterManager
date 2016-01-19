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
			[TestCaseSource(typeof(AbilityScoreTestCase), nameof(AbilityScoreTestCase.Cases))]
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
		}

		[TestFixture]
		public class IndexerTests : AbilityScoreTests
		{
			[TestFixture]
			public class GetterTests : IndexerTests
			{
				[Test]
				public void GetBase()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength)
					{
						Base = 10
					};

					Assert.AreEqual(10, abilityScore[nameof(IAbilityScore.Base)]);
				}

				[Test]
				public void GetEnhanced()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength)
					{
						Enhanced = 10
					};

					Assert.AreEqual(10, abilityScore[nameof(IAbilityScore.Enhanced)]);
				}

				[Test]
				public void GetInherent()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength)
					{
						Inherent = 10
					};

					Assert.AreEqual(10, abilityScore[nameof(IAbilityScore.Inherent)]);
				}

				[Test]
				public void GetPenalty()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength)
					{
						Penalty = 1
					};

					Assert.AreEqual(1, abilityScore[nameof(IAbilityScore.Penalty)]);
				}

				[Test]
				public void GetTemporary()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength)
					{
						Temporary =10,
					};

					Assert.AreEqual(10, abilityScore[nameof(IAbilityScore.Temporary)]);
				}

				[Test]
				public void GetModifier()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength)
					{
						Base = 10,
						Enhanced = 1,
						Inherent = 1,
						Temporary =1,
						Penalty = 1
					};

					Assert.AreEqual(1, abilityScore[nameof(IAbilityScore.Modifier)]);
				}

				[Test]
				public void GetScore()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength)
					{
						Base = 10,
						Enhanced = 10,
						Inherent = 10,
						Temporary =10,
						Penalty = 1
					};

					Assert.AreEqual(39, abilityScore[nameof(IAbilityScore.Score)]);
				}
			}

			[TestFixture]
			public class SetterTests : IndexerTests
			{
				[Test]
				public void SetBase()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Base)] = 10;

					Assert.AreEqual(10, abilityScore.Base);
				}

				[Test]
				public void SetEnhanced()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Enhanced)] = 10;

					Assert.AreEqual(10, abilityScore.Enhanced);

				}

				[Test]
				public void SetInherent()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Inherent)] = 10;

					Assert.AreEqual(10, abilityScore.Inherent);

				}

				[Test]
				public void SetPenalty()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Penalty)] = 10;

					Assert.AreEqual(10, abilityScore.Penalty);

				}

				[Test]
				public void SetTemporary()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Temporary)] = 10;

					Assert.AreEqual(10, abilityScore.Temporary);

				}

				[Test]
				public void SetModifier()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					Assert.That(
						() => abilityScore[nameof(IAbilityScore.Modifier)] = 10,
						Throws.TypeOf<ArgumentException>());

				}

				[Test]
				public void SetScore()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					Assert.That(
						() => abilityScore[nameof(IAbilityScore.Score)] = 10,
						Throws.TypeOf<ArgumentException>());
				}
			}
		}

		public static class AbilityScoreTestCase
		{
			public static IEnumerable Cases
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
		}
	}
}
