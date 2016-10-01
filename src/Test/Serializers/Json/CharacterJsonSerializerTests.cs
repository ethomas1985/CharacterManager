using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Serializers.Json;
using System;
using System.IO;
using System.Linq;
using Test.Mocks;

// ReSharper disable ExpressionIsAlwaysNull

namespace Test.Serializers.Json
{
	[TestFixture]
	public class CharacterJsonSerializerTests
	{
		[TestFixture]
		public class CanConvertMethod : CharacterJsonSerializerTests
		{
			[Test]
			public void False()
			{
				var converter = new CharacterJsonSerializer(
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				Assert.IsFalse(converter.CanConvert(typeof(string)));
			}

			[Test]
			public void CanConvertCharacter()
			{
				var converter = new CharacterJsonSerializer(
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				Assert.IsTrue(converter.CanConvert(typeof(Character)));
			}

			[Test]
			public void CanConvertICharacter()
			{
				var converter = new CharacterJsonSerializer(
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				Assert.IsTrue(converter.CanConvert(typeof(ICharacter)));
			}
		}

		[TestFixture]
		public class WriteJsonMethod : CharacterJsonSerializerTests
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
					new Character(null),
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
					new Character(null),
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
					getTestCharacter(),
					JsonSerializer.CreateDefault());

				var result = JObject.Parse(stringWriter.ToString());
				File.WriteAllText("result.json", result.ToString());

				var expected = JObject.Parse(Resources.TestCharacter);
				File.WriteAllText("expected.json", expected.ToString());


				Assert.IsTrue(JToken.DeepEquals(expected, result));
			}
		}

		[TestFixture]
		public class ReadJsonMethod : CharacterJsonSerializerTests
		{
			[Test]
			public void Fail_NotCharacter()
			{
				var converter = new CharacterJsonSerializer(
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				object notUsedParameter = null;
				Assert.Throws<ArgumentException>(
					() => converter.ReadJson(
						new JsonTextReader(new StringReader(Resources.TestCharacter)),
						typeof(string),
						notUsedParameter,
						new JsonSerializer()));
			}

			[Test]
			public void NotNull()
			{
				var converter = new CharacterJsonSerializer(
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				object notUsedParameter = null;
				var result = 
					converter.ReadJson(
						new JsonTextReader(new StringReader(Resources.TestCharacter)),
						typeof (ICharacter),
						notUsedParameter,
						new JsonSerializer());

				Assert.NotNull(result);
			}

			[Test]
			public void Expected()
			{
				var converter = new CharacterJsonSerializer(
					new MockRaceLibrary(),
					new MockSkillLibrary(),
					new MockClassLibrary());

				object notUsedParameter = null;
				var testCharacter = getTestCharacter();
				var result =
					(ICharacter) converter.ReadJson(
						new JsonTextReader(new StringReader(Resources.TestCharacter)),
						typeof(ICharacter),
						notUsedParameter,
						new JsonSerializer());

				Assert.AreEqual(testCharacter, result);
			}
		}

		protected static ICharacter getTestCharacter()
		{
			var character = new Character(new MockSkillLibrary());
			var withStrength = character.SetStrength(12);
			var withDexterity = withStrength.SetDexterity(12);
			var withConstitution = withDexterity.SetConstitution(12);
			var withIntelligence = withConstitution.SetIntelligence(12);
			var withWisdom = withIntelligence.SetWisdom(12);
			var withCharisma = withWisdom.SetCharisma(12);
			var withAge = withCharisma.SetAge(10);
			var withRace = withAge.SetRace(new MockRaceLibrary().Values.First());
			var withClass = withRace.AddClass(new MockClassLibrary().Values.First());

			return withClass;
		}
	}
}
