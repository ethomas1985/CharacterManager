using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Pathfinder.Commands;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Serializers.Json;
using Pathfinder.Test.Mocks;

namespace Pathfinder.Test.Serializers.Json.Character.Methods
{
	[TestFixture]
	public class WriteJson
	{
		[Test]
		public void Fail_NotCharacter()
		{
			var converter = new CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				new MockClassLibrary());

			Assert.Throws<Exception>(
					() => converter.WriteJson(
						new JsonTextWriter(new StringWriter()),
						"This is not a Character",
						new JsonSerializer()))
				;
		}

		[Test]
		public void NotNull()
		{
			var converter = new CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				new MockClassLibrary());

			var stringWriter = new StringWriter();
			var jsonTextWriter = new JsonTextWriter(stringWriter);

			converter.WriteJson(
				jsonTextWriter,
				CharacterJsonSerializerUtils.CreateNewCharacter(),
				JsonSerializer.CreateDefault());

			Assert.IsNotNull(stringWriter.ToString());
		}

		[Test]
		public void NotEmpty()
		{
			var converter = new CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				new MockClassLibrary());

			var stringWriter = new StringWriter();
			var jsonTextWriter = new JsonTextWriter(stringWriter);

			converter.WriteJson(
				jsonTextWriter,
				CharacterJsonSerializerUtils.CreateNewCharacter(),
				JsonSerializer.CreateDefault());

			var str = stringWriter.ToString();
			Assert.IsFalse(string.IsNullOrEmpty(str));
		}

		[Test]
		public void Expected()
		{
			var converter = new CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				new MockClassLibrary());

			var stringWriter = new StringWriter();
			var jsonTextWriter = new JsonTextWriter(stringWriter);

			converter.WriteJson(
				jsonTextWriter,
				CharacterJsonSerializerUtils.getTestCharacter(),
				JsonSerializer.CreateDefault());

			var result = JObject.Parse(stringWriter.ToString());
			File.WriteAllText("result.json", result.ToString());

			var expected = JObject.Parse(Resources.TestCharacter);
			File.WriteAllText("expected.json", expected.ToString());

			Assert.IsTrue(JToken.DeepEquals(expected, result));
		}

		[Test]
		public void WithAge()
		{
			const int expectedAge = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetAge(expectedAge);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Age)]?.Value<int>();

			Assert.AreEqual(expectedAge, result);
		}

		[Test]
		public void WithHomeland()
		{
			const string expectedHomeland = "Homeland";
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetHomeland(expectedHomeland);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Homeland)]?.Value<string>();

			Assert.AreEqual(expectedHomeland, result);
		}

		[Test]
		public void WithDeity()
		{
			const string expectedDeity = "Deity";
			var mock = new Mock<IDeity>();
			mock.SetupGet(x => x.Name).Returns(expectedDeity);

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetDeity(mock.Object);

			var result = SerializeAndParseToJson(testCharacter)
				.SelectToken($"{nameof(ICharacter.Deity)}.{nameof(IDeity.Name)}")?.Value<string>();

			Assert.AreEqual(expectedDeity, result);
		}

		[Test]
		public void WithGender()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetGender(Gender.Male);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Gender)]?.Value<string>();

			Assert.AreEqual(Gender.Male.ToString(), result);
		}

		[Test]
		public void WithEyes()
		{
			var expectedEyes = "Mauve";
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetEyes(expectedEyes);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Eyes)]?.Value<string>();

			Assert.AreEqual(expectedEyes, result);
		}

		[Test]
		public void WithHair()
		{
			var expected = "Mauve";
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetHair(expected);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Hair)]?.Value<string>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithHeight()
		{
			var expected = "9' 6\"";
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetHeight(expected);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Height)]?.Value<string>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithWeight()
		{
			var expected = "180 lbs.";
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetWeight(expected);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Weight)]?.Value<string>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithAlignment()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetAlignment(Alignment.LawfulGood);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Alignment)]?.Value<string>();

			Assert.AreEqual(Alignment.LawfulGood.ToString(), result);
		}

		[Test]
		public void WithRace()
		{
			var race = new MockRaceLibrary().Values.First();

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetRace(race);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Race)]?.Value<string>();

			Assert.AreEqual(race.Name, result);
		}

		[Test]
		public void WithSizeFromRace()
		{
			var race = new MockRaceLibrary().Values.First();

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetRace(race);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Size)]?.Value<string>();

			Assert.AreEqual(race.Size.ToString(), result);
		}

		[Test]
		public void WithBaseSpeedFromRace()
		{
			var race = new MockRaceLibrary().Values.First();

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetRace(race);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.BaseSpeed)]?.Value<int>();

			Assert.AreEqual(race.BaseSpeed, result);
		}

		[Test]
		public void WithLanguagesFromRace()
		{
			var race = new MockRaceLibrary().Values.First();

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetRace(race);

			var json = SerializeAndParseToJson(testCharacter);
			var jsonLanguages = json[nameof(ICharacter.Languages)];
			var result = jsonLanguages?.Values<string>();

			Assert.That(result, new CollectionEquivalentConstraint(race.Languages.Select(x => x.Name)));
		}

		[Test]
		public void WithLanguages()
		{
			const string languageName = "Mock Language";
			var mockLanguage = new Mock<ILanguage>();
			mockLanguage.SetupGet(x => x.Name).Returns(languageName);
			mockLanguage.Setup(x => x.ToString()).Returns(languageName);

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.AddLanguage(mockLanguage.Object);

			var json = SerializeAndParseToJson(testCharacter);
			var jsonLanguages = json[nameof(ICharacter.Languages)];
			var result = jsonLanguages?.Values<string>();

			Assert.That(result, new CollectionEquivalentConstraint(new[] { languageName }));
		}

		[Test]
		public void WithClass()
		{
			var mockClass = CreateMockClass();

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.AddClass(mockClass.Object);

			var json = SerializeAndParseToJson(testCharacter);
			var jsonClasses = json[nameof(ICharacter.Classes)];
			var children = jsonClasses?.Children();
			var result = children?[nameof(ICharacterClass.Class)].Select(x => x.Value<string>());

			Assert.That(result, new CollectionEquivalentConstraint(new[] { mockClass.Object.Name }));
		}

		[Test]
		public void WithMaxHealthPointsFromClass()
		{
			var mockClass = CreateMockClass();

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.AddClass(mockClass.Object);

			var json = SerializeAndParseToJson(testCharacter);
			var result = json[nameof(ICharacter.MaxHealthPoints)].Value<int>();

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void WithHealthPointsFromClass()
		{
			var mockClass = CreateMockClass();

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.AddClass(mockClass.Object);

			var json = SerializeAndParseToJson(testCharacter);
			var result = json[nameof(ICharacter.HealthPoints)].Value<int>();

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void WithDamage()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetDamage(expected);

			var result = SerializeAndParseToJson(testCharacter)[nameof(ICharacter.Damage)]?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithStrength()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetStrength(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.Strength)}.{nameof(IAbilityScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithDexterity()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetDexterity(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.Dexterity)}.{nameof(IAbilityScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithInitiative()
		{
			var dexterityBase = 12;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetDexterity(dexterityBase);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.Initiative)}")
					?.Value<int>();

			Assert.AreEqual(1, result);
		}

		[Test]
		public void WithConstitution()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetConstitution(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.Constitution)}.{nameof(IAbilityScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithIntelligence()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetIntelligence(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.Intelligence)}.{nameof(IAbilityScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithWisdom()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetWisdom(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.Wisdom)}.{nameof(IAbilityScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithCharisma()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetCharisma(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.Charisma)}.{nameof(IAbilityScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithArmorClass()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetDexterity(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.ArmorClass)}.{nameof(IDefenseScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithFlatFooted()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetDexterity(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.FlatFooted)}.{nameof(IDefenseScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithTouch()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetDexterity(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.Touch)}.{nameof(IDefenseScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithCombatManeuverDefense()
		{
			var expected = 10;
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetStrength(expected)
					.SetDexterity(expected);

			var result =
				SerializeAndParseToJson(testCharacter)
					.SelectToken($"{nameof(ICharacter.CombatManeuverDefense)}.{nameof(IDefenseScore.Score)}")
					?.Value<int>();

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void WithFortitude()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter();

			var json = SerializeAndParseToJson(testCharacter);
			var result = json.SelectToken($"{nameof(ICharacter.Fortitude)}.{nameof(ISavingThrow.Score)}").Value<int>();

			Assert.That(result, Is.EqualTo(-5));
		}

		[Test]
		public void WithReflex()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter();

			var json = SerializeAndParseToJson(testCharacter);
			var result = json.SelectToken($"{nameof(ICharacter.Reflex)}.{nameof(ISavingThrow.Score)}").Value<int>();

			Assert.That(result, Is.EqualTo(-5));
		}

		[Test]
		public void WithWill()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter();

			var json = SerializeAndParseToJson(testCharacter);
			var result = json.SelectToken($"{nameof(ICharacter.Will)}.{nameof(ISavingThrow.Score)}").Value<int>();

			Assert.That(result, Is.EqualTo(-5));
		}

		[Test]
		public void WithMelee()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter();

			var json = SerializeAndParseToJson(testCharacter);
			var result = json.SelectToken($"{nameof(ICharacter.Melee)}.{nameof(IOffensiveScore.Score)}").Value<int>();

			Assert.That(result, Is.EqualTo(-5));
		}

		[Test]
		public void WithRanged()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter();

			var json = SerializeAndParseToJson(testCharacter);
			var result = json.SelectToken($"{nameof(ICharacter.Ranged)}.{nameof(IOffensiveScore.Score)}").Value<int>();

			Assert.That(result, Is.EqualTo(-5));
		}

		[Test]
		public void WithCombatManeuverBonus()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter();

			var json = SerializeAndParseToJson(testCharacter);
			var result = json.SelectToken($"{nameof(ICharacter.CombatManeuverBonus)}.{nameof(IOffensiveScore.Score)}").Value<int>();

			Assert.That(result, Is.EqualTo(-5));
		}

		[Test]
		public void WithPurse()
		{
			const int copperValue = 1;
			const int silverValue = 2;
			const int goldValue = 3;
			const int platinumValue = 4;

			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.SetPurse(copperValue, silverValue, goldValue, platinumValue);

			var json = SerializeAndParseToJson(testCharacter);
			var jsonPurse = json[$"{nameof(ICharacter.Purse)}"];

			var copper = jsonPurse[$"{nameof(IPurse.Copper)}"]?.Value<int>();
			Assert.That(copper, Is.EqualTo(copperValue));

			var silver = jsonPurse[$"{nameof(IPurse.Silver)}"]?.Value<int>();
			Assert.That(silver, Is.EqualTo(silverValue));

			var gold = jsonPurse[$"{nameof(IPurse.Gold)}"]?.Value<int>();
			Assert.That(gold, Is.EqualTo(goldValue));

			var platinum = jsonPurse[$"{nameof(IPurse.Platinum)}"]?.Value<int>();
			Assert.That(platinum, Is.EqualTo(platinumValue));
		}

		[Test]
		public void WithSkills()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter();

			var json = SerializeAndParseToJson(testCharacter);
			var jsonSkillScores = json[nameof(ICharacter.SkillScores)];
			var children = jsonSkillScores?.Children();

			var result = children?.Select(x => x.SelectToken($"{nameof(ISkillScore.Skill)}.{nameof(ISkill.Name)}").Value<string>());

			var expected = new MockSkillLibrary().Values.Select(x => x.Name);
			Assert.That(result, new CollectionEquivalentConstraint(expected));
		}

		[Test]
		public void WithExperience()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter();
			var withExperience = AddExperienceCommand.Execute(testCharacter, "Event 1", "Freebie", 100);

			var json = SerializeAndParseToJson(withExperience);
			var jsonExperience = json[nameof(ICharacter.Experience)];
			var children = jsonExperience?.Children();

			var result = children?.Select(x => x.SelectToken($"{nameof(IEvent.Title)}").Value<string>());

			var expected = withExperience.Experience.Select(x => x.Title);
			Assert.That(result, new CollectionEquivalentConstraint(expected));
		}

		[Test]
		public void WithFeats()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.CreateNewCharacter()
					.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat());

			var json = SerializeAndParseToJson(testCharacter);
			var jsonFeats = json[nameof(ICharacter.Feats)];
			var children = jsonFeats?.Children();

			var result = children?.Select(x => x.SelectToken($"{nameof(IFeat.Name)}").Value<string>());

			var expected = testCharacter.Feats.Count();
			Assert.That(result?.Count(), Is.EqualTo(expected));
		}

		private static Mock<IClass> CreateMockClass()
		{
			const string className = "Mock Class";

			var mockHitDie = new Mock<IDie>();
			mockHitDie.SetupGet(x => x.Faces).Returns(6);

			var mockClassLevel = new Mock<IClassLevel>();
			mockClassLevel.SetupGet(x => x.Level).Returns(1);
			mockClassLevel.SetupGet(x => x.Fortitude).Returns(1);
			mockClassLevel.SetupGet(x => x.Reflex).Returns(1);
			mockClassLevel.SetupGet(x => x.Will).Returns(1);

			var mockClass = new Mock<IClass>();
			mockClass.SetupGet(x => x.Name).Returns(className);
			mockClass.SetupGet(x => x.HitDie).Returns(mockHitDie.Object);
			mockClass.SetupGet(x => x.ClassLevels).Returns(new[] { mockClassLevel.Object });
			mockClass.SetupGet(x => x.Skills).Returns(new HashSet<string>());
			mockClass.Setup(x => x.ToString()).Returns(className);
			return mockClass;
		}

		private static string Serialize(ICharacter testCharacter)
		{
			var converter =
				new CharacterJsonSerializer(
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

			var stringWriter = new StringWriter();
			var jsonTextWriter = new JsonTextWriter(stringWriter);

			converter.WriteJson(
				jsonTextWriter,
				testCharacter,
				JsonSerializer.CreateDefault());

			var result = stringWriter.ToString();

			//Console.WriteLine($@"Result: {result}");

			return result;
		}

		private static JObject ParseJson(string text)
		{
			var jsonTextReader = new JsonTextReader(new StringReader(text));
			var jObject = JObject.Load(jsonTextReader);
			return jObject;
		}

		private static JObject SerializeAndParseToJson(ICharacter pCharacter)
		{
			return ParseJson(Serialize(pCharacter));
		}
	}
}