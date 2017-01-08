using System;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Test.Mocks;

namespace Pathfinder.Test.Serializers.Json.Character.Methods
{
	[TestFixture]
	public class ReadJson
	{
		[Test]
		public void Fail_NotCharacter()
		{
			var converter = new Pathfinder.Serializers.Json.CharacterJsonSerializer(
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
			var converter = new Pathfinder.Serializers.Json.CharacterJsonSerializer(
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
			var converter = new Pathfinder.Serializers.Json.CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				new MockClassLibrary());

			object notUsedParameter = null;
			var testCharacter = CharacterJsonSerializerUtils.getTestCharacter();
			var result =
				(ICharacter) converter.ReadJson(
					new JsonTextReader(new StringReader(Resources.TestCharacter)),
					typeof(ICharacter),
					notUsedParameter,
					new JsonSerializer());

			Assert.AreEqual(testCharacter, result);
		}
	}
}