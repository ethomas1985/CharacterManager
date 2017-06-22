using Moq;
using NUnit.Framework;
using Pathfinder.Api.Controllers;
using Pathfinder.Api.Models;
using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Model;
using System;
using System.Collections.Generic;
using Pathfinder.Test;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Api.Tests
{
	[TestFixture]
	public class CharacterGeneraterControllerTests
	{
		private static readonly Lazy<ILibrary<IClass>> LazyClassLibrary
			= new Lazy<ILibrary<IClass>>(() =>
			{
				IClass iClass;
				var testClass = ClassMother.Create();
				var mockClassLibrary = new Mock<ILibrary<IClass>>();

				mockClassLibrary.Setup(foo => foo.Values).Returns(new List<IClass> { testClass });
				mockClassLibrary.Setup(foo => foo[testClass.Name]).Returns(testClass);
				mockClassLibrary
					.Setup(foo => foo.TryGetValue(testClass.Name, out iClass))
					.OutCallback((string t, out IClass r) => r = testClass)
					.Returns(true);
				return mockClassLibrary.Object;
			});

		internal static ILibrary<IClass> ClassLibrary => LazyClassLibrary.Value;

		private static readonly Lazy<ILibrary<IRace>> LazyRaceLibrary
			= new Lazy<ILibrary<IRace>>(() =>
			{
				IRace race;
				var testRace = RaceMother.Create();
				var mockRaceLibrary = new Mock<ILibrary<IRace>>();

				mockRaceLibrary.Setup(foo => foo.Values).Returns(new List<IRace> { testRace });
				mockRaceLibrary.Setup(foo => foo[testRace.Name]).Returns(testRace);
				mockRaceLibrary
					.Setup(foo => foo.TryGetValue(testRace.Name, out race))
					.OutCallback((string t, out IRace r) => r = testRace)
					.Returns(true);
					
				return mockRaceLibrary.Object;
			});

		internal static ILibrary<IRace> RaceLibrary => LazyRaceLibrary.Value;

		private static readonly Lazy<ILibrary<ISkill>> LazySkillLibrary
			= new Lazy<ILibrary<ISkill>>(() =>
			{
				ISkill race;
				var testSkill =SkillMother.Create();
				var mockRaceLibrary = new Mock<ILibrary<ISkill>>();

				mockRaceLibrary.Setup(foo => foo.Values).Returns(new List<ISkill> { testSkill });
				mockRaceLibrary.Setup(foo => foo[testSkill.Name]).Returns(testSkill);
				mockRaceLibrary
					.Setup(foo => foo.TryGetValue(testSkill.Name, out race))
					.OutCallback((string t, out ISkill r) => r = testSkill)
					.Returns(true);
					
				return mockRaceLibrary.Object;
			});

		internal static ILibrary<ISkill> SkillLibrary => LazySkillLibrary.Value;

		protected static CharacterGeneratorController createCharacterGeneratorController()
		{
			var mockCharacterLibrary = new Mock<ILibrary<ICharacter>>();
			
			var mockFeatLibrary = new Mock<ILibrary<IFeat>>();
			var mockItemLibrary = new Mock<ILibrary<IItem>>();
			var mockSkillLibrary = new Mock<ILibrary<ISkill>>();

			var charGen =
				new CharacterGeneratorController(
					mockCharacterLibrary.Object,
					RaceLibrary,
					mockSkillLibrary.Object,
					ClassLibrary,
					mockFeatLibrary.Object,
					mockItemLibrary.Object);
			return charGen;
		}

		[TestFixture]
		public class SetAbilityScoresMethod : CharacterGeneraterControllerTests
		{
			private const int ABILITY_SCORE = 12;

			[Test]
			public void NullParameter()
			{
				var charGen = createCharacterGeneratorController();

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetAbilityScores(null));
			}

			[Test]
			public void ReturnsNotNull()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetAbilityScores(
					new AbilityScoreSet
					{
						Strength = 1,
						Dexterity = 1,
						Intelligence = 1,
						Constitution = 1,
						Wisdom = 1,
						Charisma = 1
					});

				Assert.NotNull(result);
			}

			[Test]
			public void SetsStrength()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetAbilityScores(
					new AbilityScoreSet
					{
						Strength = ABILITY_SCORE,
						Dexterity = 1,
						Intelligence = 1,
						Constitution = 1,
						Wisdom = 1,
						Charisma = 1
					});

				Assert.AreEqual(ABILITY_SCORE, result.Strength.Score);
			}

			[Test]
			public void SetsDexterity()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetAbilityScores(
					new AbilityScoreSet
					{
						Strength = 1,
						Dexterity = ABILITY_SCORE,
						Intelligence = 1,
						Constitution = 1,
						Wisdom = 1,
						Charisma = 1
					});

				Assert.AreEqual(ABILITY_SCORE, result.Dexterity.Score);
			}

			[Test]
			public void SetsConstitution()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetAbilityScores(
					new AbilityScoreSet
					{
						Strength = 1,
						Dexterity = 1,
						Constitution = ABILITY_SCORE,
						Intelligence = 1,
						Wisdom = 1,
						Charisma = 1
					});

				Assert.AreEqual(ABILITY_SCORE, result.Constitution.Score);
			}

			[Test]
			public void SetsIntelligence()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetAbilityScores(
					new AbilityScoreSet
					{
						Strength = 1,
						Dexterity = 1,
						Constitution = 1,
						Intelligence = ABILITY_SCORE,
						Wisdom = 1,
						Charisma = 1
					});

				Assert.AreEqual(ABILITY_SCORE, result.Intelligence.Score);
			}

			[Test]
			public void SetsWisdom()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetAbilityScores(
					new AbilityScoreSet
					{
						Strength = 1,
						Dexterity = 1,
						Intelligence = 1,
						Constitution = 1,
						Wisdom = ABILITY_SCORE,
						Charisma = 1
					});

				Assert.AreEqual(ABILITY_SCORE, result.Wisdom.Score);
			}

			[Test]
			public void SetsCharisma()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetAbilityScores(
					new AbilityScoreSet
					{
						Strength = 1,
						Dexterity = 1,
						Intelligence = 1,
						Constitution = 1,
						Wisdom = 1,
						Charisma = ABILITY_SCORE
					});

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
				var charGen = createCharacterGeneratorController();

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetRace(null, null));
			}

			[Test]
			public void NullCharacter()
			{
				var charGen = createCharacterGeneratorController();

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetRace(string.Empty, null));
			}

			[Test]
			public void ReturnsNotNull()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetRace(TEST_RACE, new Character(SkillLibrary));

				Assert.NotNull(result);
			}
		}

		[TestFixture]
		public class SetClassMethod : CharacterGeneraterControllerTests
		{
			[Test]
			public void NullRace()
			{
				var charGen = createCharacterGeneratorController();

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetClass(null, null));
			}

			[Test]
			public void NullCharacter()
			{
				var charGen = createCharacterGeneratorController();

				Assert.Throws<ArgumentNullException>(
					() => charGen.SetClass(string.Empty, null));
			}

			[Test]
			public void ReturnsNotNull()
			{
				var charGen = createCharacterGeneratorController();

				var result = charGen.SetClass("Test Class", new Character(SkillLibrary));

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
