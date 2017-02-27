using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Serializers.Json;
using Pathfinder.Test.Mocks;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.Serializers.Json.Character.Methods
{
	[TestFixture]
	public class ReadJson
	{
		private const string RACE_NAME = "Test Race";

		private const string STRENGTH_ATTRIBUTE =
			"Strength: {" +
			"	Type: \"Strength\"," +
			"	Base: 12" +
			"}}";

		private const string DEXTERITY_ATTRIBUTE =
			"Dexterity: {" +
			"	Type: \"Dexterity\"," +
			"	Base: 12" +
			"}}";

		private const string CONSTITUTION_ATTRIBUTE =
			"Constitution: {" +
			"	Type: \"Constitution\"," +
			"	Base: 12" +
			"}}";

		private const string INTELLIGENCE_ATTRIBUTE =
			"Intelligence: {" +
			"	Type: \"Intelligence\"," +
			"	Base: 12" +
			"}}";

		private const string WISDOM_ATTRIBUTE =
			"Wisdom: {" +
			"	Type: \"Wisdom\"," +
			"	Base: 12" +
			"}}";

		private const string CHARISMA_ATTRIBUTE =
			"Charisma: {" +
			"	Type: \"Charisma\"," +
			"	Base: 12" +
			"}}";

		private const string CLASS =
			"{" +
			"	Class: \"Mock Class\"," +
			"	Level: 1," +
			"	IsFavored: true," +
			"	BaseAttackBonus: 0," +
			"	Fortitude: 0," +
			"	Reflex: 0," +
			"	Will: 0," +
			"	HitPoints: [" +
			"		6" +
			"	]" +
			"}";

		[Test]
		public void Fail_NotCharacter()
		{
			Assert.Throws<ArgumentException>(
				() =>
				{
					var converter = new CharacterJsonSerializer(
						new MockRaceLibrary(),
						new MockSkillLibrary(),
						new MockClassLibrary());

					object notUsedParameter = null;
					converter.ReadJson(
						new JsonTextReader(new StringReader(Resources.TestCharacter)),
						typeof(string),
						notUsedParameter,
						new JsonSerializer());
				});
		}

		[Test]
		public void NotNull()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			Assert.NotNull(result);
		}

		[Test]
		public void EmptyObject()
		{
			var result = GenerateTestCharacter("{}");
			Assert.NotNull(result);
		}

		/**
		 * Super Test
		 */

		[Test]
		public void Expected()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var testCharacter = CharacterJsonSerializerUtils.GetTestCharacter();

			Assert.AreEqual(testCharacter, result);
		}

		[Test]
		public void WithRace()
		{
			var result = GenerateTestCharacter(
				"{" +
				$"Race: \"{RACE_NAME}\"" +
				"}");
			Assert.AreEqual(RACE_NAME, result.Race.Name);
		}

		[Test]
		public void WithOneClass()
		{
			var result = GenerateTestCharacter(
				"{" +
				"Classes: [" +
				$"{CLASS}" +
				"]" +
				"}");
			Assert.AreEqual(1, result.Classes.Count());
		}

		[Test]
		public void WithClasses()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			var mockClass = CharacterJsonSerializerUtils.CreateTestingClass();

			var characterClass =
				new CharacterClass(
					mockClass, 1, true, new[] { 6 });
			Assert.AreEqual(characterClass, result.Classes.First());
		}

		[Test]
		public void WithStrength()
		{
			var result = GenerateTestCharacter(
				"{" +
				$"{STRENGTH_ATTRIBUTE}," +
				"}");
			Assert.AreEqual(new AbilityScore(AbilityType.Strength, 12), result.Strength);
		}

		[Test]
		public void WithDexterity()
		{
			var result = GenerateTestCharacter(
				"{" +
				$"{DEXTERITY_ATTRIBUTE}," +
				"}");
			Assert.AreEqual(new AbilityScore(AbilityType.Dexterity, 12), result.Dexterity);
		}

		[Test]
		public void WithConstitution()
		{
			var result = GenerateTestCharacter(
				"{" +
				$"{CONSTITUTION_ATTRIBUTE}," +
				"}");
			Assert.AreEqual(new AbilityScore(AbilityType.Constitution, 12), result.Constitution);
		}

		[Test]
		public void WithIntelligence()
		{
			var result = GenerateTestCharacter(
				"{" +
				$"{INTELLIGENCE_ATTRIBUTE}," +
				"}");
			Assert.AreEqual(new AbilityScore(AbilityType.Intelligence, 12), result.Intelligence);
		}

		[Test]
		public void WithWisdom()
		{
			var result = GenerateTestCharacter(
				"{" +
				$"{WISDOM_ATTRIBUTE}," +
				"}");
			Assert.AreEqual(new AbilityScore(AbilityType.Wisdom, 12), result.Wisdom);
		}

		[Test]
		public void WithCharisma()
		{
			var result = GenerateTestCharacter(
				"{" +
				$"{CHARISMA_ATTRIBUTE}," +
				"}");
			Assert.AreEqual(new AbilityScore(AbilityType.Charisma, 12), result.Charisma);
		}

		[Test]
		public void WithName()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			Assert.AreEqual("Unit McTesterFace", result.Name);
		}

		[Test]
		public void WithAlignment()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual(Alignment.LawfulGood, result.Alignment);
		}

		[Test]
		public void WithGender()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			Assert.AreEqual(Gender.Male, result.Gender);
		}

		[Test]
		public void WithAge()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			Assert.AreEqual(10, result.Age);
		}

		[Test]
		public void WithHomeland()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			Assert.AreEqual("Homeland", result.Homeland);
		}

		[Test]
		public void WithDeity()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			Assert.AreEqual("Deity", result.Deity?.Name);
		}

		[Test]
		public void WithEyes()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			Assert.AreEqual("Blue", result.Eyes);
		}

		[Test]
		public void WithHair()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual("Blue", result.Hair);
		}

		[Test]
		public void WithHeight()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual("9' 6\"", result.Height);
		}

		[Test]
		public void WithWeight()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual("180 lbs.", result.Weight);
		}

		[Test]
		public void WithOneLanguage()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual(2, result.Languages.Count());
		}

		[Test]
		public void WithLanguages()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.IsTrue(result.Languages.Any(x => x.Name.Equals("Test Language")));
			Assert.IsTrue(result.Languages.Any(x => x.Name.Equals("Mock Language")));
		}

		[Test]
		public void WithDamage()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual(2, result.Damage);
		}

		[Test]
		public void WithPurse()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual(new Purse(1, 2, 3, 4), result.Purse);
		}

		[Test]
		public void WithSkills()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual(1, result.SkillScores.Count());
		}

		[Test]
		public void WithExperience()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual(1, result.Experience.Count());
		}

		[Test]
		public void WithFeats()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual(2, result.Feats.Count());
		}

		[Test]
		public void WithInventory()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			Assert.AreEqual(1, result.Inventory.Count());
		}

		/**
		 * The following are derived Properties.
		 */

		[Test]
		public void WithArmorClass()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);

			var expected =
				new DefenseScore(
					DefensiveType.ArmorClass,
					0, 0, new AbilityScore(AbilityType.Dexterity, 12), 0, 0, 0, 0, 0, 0);

			Assert.AreEqual(expected, result.ArmorClass);
		}

		[Test]
		public void WithFlatFooted()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new DefenseScore(
					DefensiveType.FlatFooted,
					0, 0, new AbilityScore(AbilityType.Dexterity, 12), 0, 0, 0, 0, 0, 0);
			Assert.AreEqual(expected, result.FlatFooted);
		}

		[Test]
		public void WithTouch()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new DefenseScore(
					DefensiveType.Touch,
					0, 0, new AbilityScore(AbilityType.Dexterity, 12), 0, 0, 0, 0, 0, 0);
			Assert.AreEqual(expected, result.Touch);
		}

		[Test]
		public void WithCombatManeuverDefense()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new DefenseScore(
					1,
					new AbilityScore(AbilityType.Strength, 12),
					new AbilityScore(AbilityType.Dexterity, 12),
					0, 0, 0, 0, 0);
			Assert.AreEqual(expected, result.CombatManeuverDefense);
		}

		[Test]
		public void WithFortitude()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new SavingThrow(
					SavingThrowType.Fortitude,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0, 0);
			Assert.AreEqual(expected, result.Fortitude);
		}

		[Test]
		public void WithReflex()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new SavingThrow(
					SavingThrowType.Reflex,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0, 0);
			Assert.AreEqual(expected, result.Reflex);
		}

		[Test]
		public void WithWill()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new SavingThrow(
					SavingThrowType.Will,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0, 0);
			Assert.AreEqual(expected, result.Will);
		}

		[Test]
		public void WithMelee()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new OffensiveScore(
					OffensiveType.Melee,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0, 0);
			Assert.AreEqual(expected, result.Melee);
		}

		[Test]
		public void WithRanged()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new OffensiveScore(
					OffensiveType.Ranged,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0, 0);
			Assert.AreEqual(expected, result.Ranged);
		}

		[Test]
		public void WithCombatManeuverBonus()
		{
			var result = GenerateTestCharacter(Resources.TestCharacter);
			var expected =
				new OffensiveScore(
					OffensiveType.CombatManeuverBonus,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0, 0);
			Assert.AreEqual(expected, result.CombatManeuverBonus);
		}

		[SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
		private static ICharacter GenerateTestCharacter(string pTestCharacterJson)
		{
			var mockClassLibrary = new MockClassLibrary();
			mockClassLibrary.Store(CharacterJsonSerializerUtils.CreateTestingClass());

			var converter = new CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				mockClassLibrary);

			object notUsedParameter = null;
			var result =
				(ICharacter)converter.ReadJson(
					new JsonTextReader(new StringReader(pTestCharacterJson)),
						typeof(ICharacter),
						notUsedParameter,
						new JsonSerializer());
			return result;
		}
	}
}