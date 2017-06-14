using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.EventTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var @event = new Event("Test Title", "Test Description", 100);

			Assert.That(
				() => JsonConvert.SerializeObject(@event),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var @event = new Event("Test Title", "Test Description", 100);

			var actual = JsonConvert.SerializeObject(@event);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IEvent.Title)}\":\"{@event.Title}\",")
					.Append($"\"{nameof(IEvent.Description)}\":\"{@event.Description}\",")
					.Append($"\"{nameof(IEvent.ExperiencePoints)}\":{@event.ExperiencePoints}")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
