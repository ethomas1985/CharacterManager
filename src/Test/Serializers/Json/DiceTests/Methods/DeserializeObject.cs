using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.DiceTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void InvalidFormat()
		{
			const string stringValue = "\"THIS IS INVALID\"";
			Assert.That(
				() => JsonConvert.DeserializeObject<IDice>(stringValue),
				Throws.Exception
						.TypeOf<JsonException>()
						.With.Message.EqualTo($"Invalid Formatting: [{nameof(IDice)}] {stringValue}"));
		}

		[Test]
		public void InvalidCount()
		{
			const string stringValue = "\"0d6\"";
			Assert.That(
						() => JsonConvert.DeserializeObject<IDice>(stringValue),
						Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Invalid Input: The number of dice must be greater than one."));
		}

		[Test]
		public void InvalidFaces()
		{
			const string stringValue = "\"1d3\"";
			Assert.That(
						() => JsonConvert.DeserializeObject<IDice>(stringValue),
						Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Invalid Input: The number of faces on a Die must be greater than four."));
		}

		[Test]
		public void Success()
		{
			var result = JsonConvert.DeserializeObject<IDice>("\"1d6\"");
			Assert.That(result, Is.EqualTo(new Dice(1, new Die(6))));
		}
	}
}
