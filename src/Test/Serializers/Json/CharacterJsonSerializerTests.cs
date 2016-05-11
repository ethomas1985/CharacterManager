using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Serializers.Json;
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
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

				Assert.IsFalse(converter.CanConvert(typeof(string)));
			}

			[Test]
			public void CanConvertCharacter()
			{
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

				Assert.IsTrue(converter.CanConvert(typeof(Character)));
			}

			[Test]
			public void CanConvertICharacter()
			{
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

				Assert.IsTrue(converter.CanConvert(typeof(ICharacter)));
			}
		}

		[TestFixture]
		public class WriteJsonMethod : CharacterJsonSerializerTests
		{
			[Test]
			public void Fail_NotCharacter()
			{
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

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
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

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
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

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
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

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
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

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
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

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
				var converter = new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary());

				object notUsedParameter = null;
				var testCharacter = getTestCharacter();
				var result =
					converter.ReadJson(
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
				.SetCharisma(12);
		}
	}

	public class MockSkillLibrary : ILibrary<ISkill>
	{
		private Dictionary<string, ISkill> _library;

		public IEnumerator<ISkill> GetEnumerator()
		{
			return Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private Dictionary<string, ISkill> Library
		{
			get
			{
				if (_library == null)
				{
					_library = new Dictionary<string, ISkill>
					{
						["Testing Skill"]
							= new Skill(
								"Testing Skill",
								AbilityType.Strength,
								true,
								true,
								"Testing Description",
								"Testing Check",
								"Testing Action",
								"Testing Try Again",
								"Testing Special",
								"Testing Restriction",
								"Testing Untrained"),
					};
				}
				return _library;
			}
		}

		public IEnumerable<string> Keys => Library.Keys;
		public IEnumerable<ISkill> Values => Library.Values;

		public ISkill this[string pKey] => Library[pKey];

		public bool TryGetValue(string pKey, out ISkill pValue)
		{
			return Library.TryGetValue(pKey, out pValue);
		}

		public void Store(ISkill pValue)
		{
			throw new NotImplementedException();
		}
	}

	public class MockRaceLibrary : ILibrary<IRace>
	{
		private Dictionary<string, IRace> _library;

		public IEnumerator<IRace> GetEnumerator()
		{
			return Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private Dictionary<string, IRace> Library
		{
			get
			{
				if (_library == null)
				{
					_library = new Dictionary<string, IRace>
					{
						["Testing Skill"]
							= new Race(
								"Testing Race",
								"Testing Race",
								"Testing Description",
								Size.Medium, 
								10,
								null,
								null,
								null),
					};
				}
				return _library;
			}
		}

		public IEnumerable<string> Keys => Library.Keys;
		public IEnumerable<IRace> Values => Library.Values;

		public IRace this[string pKey] => Library[pKey];

		public bool TryGetValue(string pKey, out IRace pValue)
		{
			return Library.TryGetValue(pKey, out pValue);
		}

		public void Store(IRace pValue)
		{
			throw new NotImplementedException();
		}
	}
}
