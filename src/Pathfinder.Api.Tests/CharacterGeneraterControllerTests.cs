using System;
using System.Linq;
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
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetAbilityScores(null));
			}

			[Test]
			public void ReturnsNotNull()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet());

				Assert.NotNull(result);
			}

			[Test]
			public void SetsStrength()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet {Strength = ABILITY_SCORE});

				Assert.AreEqual(ABILITY_SCORE, result.Strength.Score);
			}

			[Test]
			public void SetsDexterity()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Dexterity = ABILITY_SCORE });

				Assert.AreEqual(ABILITY_SCORE, result.Dexterity.Score);
			}

			[Test]
			public void SetsConstitution()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Constitution = ABILITY_SCORE });

				Assert.AreEqual(ABILITY_SCORE, result.Constitution.Score);
			}

			[Test]
			public void SetsIntelligence()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Intelligence = ABILITY_SCORE });

				Assert.AreEqual(ABILITY_SCORE, result.Intelligence.Score);
			}

			[Test]
			public void SetsWisdom()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Wisdom = ABILITY_SCORE });

				Assert.AreEqual(ABILITY_SCORE, result.Wisdom.Score);
			}

			[Test]
			public void SetsCharisma()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetAbilityScores(new AbilityScoreSet { Charisma = ABILITY_SCORE});

				Assert.AreEqual(ABILITY_SCORE, result.Charisma.Score);
			}
		}

		[TestFixture]
		public class SetRaceMethod : CharacterGeneraterControllerTests
		{
			private const string TEST_RACE = "Test Race";

			[Test]
			public void NullRace()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetRace(null, null));
			}

			[Test]
			public void NullCharacter()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetRace(string.Empty, null));
			}

			[Test]
			public void ReturnsNotNull()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetRace(TEST_RACE, new MockCharacter());

				Assert.NotNull(result);
			}

			/*
			 * This is an integration level test that is dependent on the ICharacter
			 * class's implementation of ICharacter.SetRace(). I don't feel like implementing
			 * the method on the MockCharacter class nor do I think I should use the Character
			 * class for this test. 
			 */
			//[Test]
			//public void Expected()
			//{
			//	var mockRaceLibrary = new MockRaceLibrary();
			//	var charGen = new CharacterGeneratorController(
			//		new MockCharacterLibrary(),
			//		mockRaceLibrary,
			//		new MockSkillLibrary(),
			//		new MockClassLibrary());

			//	var result = charGen.SetRace(TEST_RACE, new MockCharacter());

			//	var expected = mockRaceLibrary[TEST_RACE];

			//	Assert.AreEqual(expected, result.Race);
			//}
		}

		[TestFixture]
		public class SetClassMethod : CharacterGeneraterControllerTests
		{
			[Test]
			public void NullRace()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetClass(null, null));
			}

			[Test]
			public void NullCharacter()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetClass(string.Empty, null));
			}

			[Test]
			public void ReturnsNotNull()
			{
				var charGen = new CharacterGeneratorController(
					new MockCharacterLibrary(),
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				var result = charGen.SetClass("Test Class", new MockCharacter());

				Assert.NotNull(result);
			}

			/*
			 * This is an integration level test that is dependent on the ICharacter
			 * class's implementation of ICharacter.SetRace(). I don't feel like implementing
			 * the method on the MockCharacter class nor do I think I should use the Character
			 * class for this test. 
			 */
			//[Test]
			//public void Expected()
			//{
			//	var mockClassLibrary = new MockClassLibrary();

			//	var charGen = new CharacterGeneratorController(
			//		new MockCharacterLibrary(),
			//		new MockRaceLibrary(),
			//		new MockSkillLibrary(),
			//		mockClassLibrary);

			//	var result = charGen.SetRace("Test Class", new MockCharacter());

			//	var expected = mockClassLibrary["Test Class"];

			//	Assert.AreEqual(expected, result.Classes.FirstOrDefault());
			//}
		}
	}
}
