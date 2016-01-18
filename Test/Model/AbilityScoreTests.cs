using System;
using NUnit.Framework;
using Pathfinder.Model;
using Pathfinder.Enum;
using Pathfinder.Interface;

namespace Test.Model
{
	[TestFixture]
	public class AbilityScoreTests
	{
		[TestFixture]
		public class BasePropertyTests : AbilityScoreTests
		{
			[Test]
			public void Test()
			{

			}
		}

		[TestFixture]
		public class EnhancedPropertyTests : AbilityScoreTests
		{
		}

		[TestFixture]
		public class InherentPropertyTests : AbilityScoreTests
		{
		}

		[TestFixture]
		public class ModifierPropertyTests : AbilityScoreTests
		{
		}

		[TestFixture]
		public class PenaltyPropertyTests : AbilityScoreTests
		{
		}

		[TestFixture]
		public class TemporaryPropertyTests : AbilityScoreTests
		{
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
			}

			[TestFixture]
			public class SetterTests : IndexerTests
			{
				[Test]
				public void SetBase()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Temporary)] = 10;

					Assert.AreEqual(10, abilityScore.Temporary);
				}

				[Test]
				public void SetEnhanced()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Temporary)] = 10;

					Assert.AreEqual(10, abilityScore.Temporary);

				}

				[Test]
				public void SetInherent()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Temporary)] = 10;

					Assert.AreEqual(10, abilityScore.Temporary);

				}

				[Test]
				public void SetModifier()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Temporary)] = 10;

					Assert.AreEqual(10, abilityScore.Temporary);

				}

				[Test]
				public void SetPenalty()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Temporary)] = 10;

					Assert.AreEqual(10, abilityScore.Temporary);

				}

				[Test]
				public void SetTemporary()
				{
					var abilityScore = new AbilityScore(AbilityType.Strength);

					abilityScore[nameof(IAbilityScore.Temporary)] = 10;

					Assert.AreEqual(10, abilityScore.Temporary);

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
	}
}
