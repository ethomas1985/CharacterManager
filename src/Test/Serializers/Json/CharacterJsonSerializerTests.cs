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
				var expected = JObject.Parse(Resources.TestCharacter);
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
			return new Character(new MockSkillLibrary())
				.SetStrength(12)
				.SetDexterity(12)
				.SetConstitution(12)
				.SetIntelligence(12)
				.SetWisdom(12)
				.SetCharisma(12)
				.SetAge(10)
				.SetRace(new MockRaceLibrary().Values.First())
				.AddClass(new MockClassLibrary().Values.First());
		}
	}
}
