using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Serializers.Json.CharacterClassTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresClassAttribute()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<ICharacterClass>("{}"),
				Throws.Exception.TypeOf<JsonException>()
					.With.Message.EqualTo($"Missing Required Attribute: {nameof(ICharacterClass.Class)}"));
		}

		[Test]
		public void WithClass()
		{
			var testClass = SetupTestFixtureForJsonSerializers.ClassRepository.Values.First();

			string value = 
				"{" +
				$"	Class: \"{testClass.Name}\"" +
				"}";
			var result = JsonConvert.DeserializeObject<ICharacterClass>(value);
			Assert.That(result.Class, Is.EqualTo(testClass));
		}

		[Test]
		public void WithLevel()
		{
			var testClass = SetupTestFixtureForJsonSerializers.ClassRepository.Values.First();

			string value =
				"{" +
				$"	Class: \"{testClass.Name}\"," +
				$"	Level: 12" +
				"}";
			var result = JsonConvert.DeserializeObject<ICharacterClass>(value);
			Assert.That(result.Level, Is.EqualTo(12));
		}

		[Test]
		public void WithIsFavored()
		{
			var testClass = SetupTestFixtureForJsonSerializers.ClassRepository.Values.First();

			string value =
				"{" +
				$"	Class: \"{testClass.Name}\"," +
				$"	IsFavored: true" +
				"}";
			var result = JsonConvert.DeserializeObject<ICharacterClass>(value);
			Assert.That(result.IsFavored, Is.EqualTo(true));
		}

		[Test]
		public void WithHitPoints()
		{
			var testClass = SetupTestFixtureForJsonSerializers.ClassRepository.Values.First();

			string value =
				"{" +
				$"	Class: \"{testClass.Name}\"," +
				$"	HitPoints: [ 8 ]" +
				"}";
			var result = JsonConvert.DeserializeObject<ICharacterClass>(value);
			Assert.That(result.HitPoints, Is.EqualTo(new [] { 8 }));
		}
	}
}
