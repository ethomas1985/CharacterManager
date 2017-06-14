using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.ExperienceTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var experience = new Experience();

			Assert.That(
				() => JsonConvert.SerializeObject(experience),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var @event = new Event("Test Title", "Test Description", 100);
			var experience = new Experience().Append(@event);

			var actual = JsonConvert.SerializeObject(experience);

			var expected =
				new StringBuilder("[")
					.Append("{")
					.Append($"\"{nameof(IEvent.Title)}\":\"{@event.Title}\",")
					.Append($"\"{nameof(IEvent.Description)}\":\"{@event.Description}\",")
					.Append($"\"{nameof(IEvent.ExperiencePoints)}\":{@event.ExperiencePoints}")
					.Append("}")
					.Append("]")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
