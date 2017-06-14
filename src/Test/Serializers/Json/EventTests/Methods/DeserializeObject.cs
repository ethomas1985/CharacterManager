using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.EventTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresTitle()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<IEvent>("{}"),
				Throws.Exception
					.TypeOf<JsonException>()
					.With.Message.EqualTo($"Missing Required Attribute: {nameof(IEvent.Title)}"));
		}

		[Test]
		public void WithTitle()
		{
			const string title = "Testing Title";
			var stringValue = $"{{ \"{nameof(IEvent.Title)}\": \"{title}\" }}";

			var result = JsonConvert.DeserializeObject<IEvent>(stringValue);

			var expected = new Event(title, string.Empty, 0);
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void WithDescription()
		{
			const string title = "Testing Title";
			const string description = "Testing Description";
			var stringValue = 
				$"{{ " +
				$"\"{nameof(IEvent.Title)}\": \"{title}\", " +
				$"\"{nameof(IEvent.Description)}\": \"{description}\" " +
				$"}}";

			var result = JsonConvert.DeserializeObject<IEvent>(stringValue);

			var expected = new Event(title, description, 0);
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void WithExperiencePoints()
		{
			const string title = "Testing Title";
			const int experiencePoints = 100;
			var stringValue =
				$"{{ " +
				$"\"{nameof(IEvent.Title)}\": \"{title}\", " +
				$"\"{nameof(IEvent.ExperiencePoints)}\": {experiencePoints} " +
				$"}}";

			var result = JsonConvert.DeserializeObject<IEvent>(stringValue);

			var expected = new Event(title, string.Empty, experiencePoints);
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
