using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.DieTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void InvalidFormat()
		{
			const string stringValue = "\"THIS IS INVALID\"";
			Assert.That(
					    () => JsonConvert.DeserializeObject<IDie>(stringValue),
					    Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Invalid Formatting: [{nameof(IDie)}] {stringValue}"));
		}

		[Test]
		public void InvalidFaces()
		{
			const string stringValue = "\"d3\"";
			Assert.That(
					    () => JsonConvert.DeserializeObject<IDie>(stringValue),
					    Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Invalid Input: The number of faces on a Die must be greater than four."));
		}

		[Test]
		public void Success()
		{
			var result = JsonConvert.DeserializeObject<IDie>("\"d6\"");
			Assert.That(result, Is.EqualTo(new Die(6)));
		}
	}
}
