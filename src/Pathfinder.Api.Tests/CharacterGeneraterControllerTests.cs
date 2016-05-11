using System;
using NUnit.Framework;
using Pathfinder.Api.Controllers;
using Pathfinder.Api.Models;
using Pathfinder.Api.Tests.Mocks;

namespace Pathfinder.Api.Tests
{
	[TestFixture]
	public class CharacterGeneraterControllerTests
	{
		[TestFixture]
		public class SetAbilityScoresMethod : CharacterGeneraterControllerTests
		{
			private const int ABILITY_SCORE = 12;

			[Test]
			public void NullParameter()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetAbilityScores(null));
			}

			[Test]
			public void ReturnsNotNull()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet());

				Assert.NotNull(result);
			}

			[Test]
			public void SetsStrength()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet {Strength = ABILITY_SCORE});

				Assert.AreEqual(ABILITY_SCORE, result.Strength.Score);
			}

			[Test]
			public void SetsDexterity()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Dexterity = ABILITY_SCORE });

				Assert.AreEqual(ABILITY_SCORE, result.Dexterity.Score);
			}

			[Test]
			public void SetsConstitution()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Constitution = ABILITY_SCORE });

				Assert.AreEqual(ABILITY_SCORE, result.Constitution.Score);
			}

			[Test]
			public void SetsIntelligence()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Intelligence = ABILITY_SCORE });

				Assert.AreEqual(ABILITY_SCORE, result.Intelligence.Score);
			}

			[Test]
			public void SetsWisdom()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Wisdom = ABILITY_SCORE });

				Assert.AreEqual(ABILITY_SCORE, result.Wisdom.Score);
			}

			[Test]
			public void SetsCharisma()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Charisma = ABILITY_SCORE});

				Assert.AreEqual(ABILITY_SCORE, result.Charisma.Score);
			}
		}

		[TestFixture]
		public class SetRaceMethod : CharacterGeneraterControllerTests
		{
			[Test]
			public void NullRace()
			{
				var charGen = new CharacterGeneraterController();

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetRace(null, null));
			}

			[Test]
			public void NullCharacter()
			{
				var charGen = new CharacterGeneraterController();

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetRace(string.Empty, null));
			}

			[Test]
			public void ReturnsNotNull()
			{
				var charGen = new CharacterGeneraterController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary());

				var result = charGen.SetRace("Test Race", new MockCharacter());

				Assert.NotNull(result);
			}
		}
	}
}
