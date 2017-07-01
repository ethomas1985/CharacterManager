using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
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
			var @event = new ExperienceEvent("Test Title", "Test Description", 100);
			var experience = new Experience().Append(@event);

			var actual = JsonConvert.SerializeObject(experience);

			var expected =
				new StringBuilder("[")
					.Append("{")
					.Append($"\"{nameof(IExperienceEvent.Title)}\":\"{@event.Title}\",")
					.Append($"\"{nameof(IExperienceEvent.Description)}\":\"{@event.Description}\",")
					.Append($"\"{nameof(IExperienceEvent.ExperiencePoints)}\":{@event.ExperiencePoints}")
					.Append("}")
					.Append("]")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
