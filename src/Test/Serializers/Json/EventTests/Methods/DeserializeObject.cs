using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
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
				() => JsonConvert.DeserializeObject<IExperienceEvent>("{}"),
				Throws.Exception
					.TypeOf<JsonException>()
					.With.Message.EqualTo($"Missing Required Attribute: {nameof(IExperienceEvent.Title)}"));
		}

		[Test]
		public void WithTitle()
		{
			const string title = "Testing Title";
			var stringValue = $"{{ \"{nameof(IExperienceEvent.Title)}\": \"{title}\" }}";

			var result = JsonConvert.DeserializeObject<IExperienceEvent>(stringValue);

			var expected = new ExperienceEvent(title, string.Empty, 0);
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void WithDescription()
		{
			const string title = "Testing Title";
			const string description = "Testing Description";
			var stringValue = 
				$"{{ " +
				$"\"{nameof(IExperienceEvent.Title)}\": \"{title}\", " +
				$"\"{nameof(IExperienceEvent.Description)}\": \"{description}\" " +
				$"}}";

			var result = JsonConvert.DeserializeObject<IExperienceEvent>(stringValue);

			var expected = new ExperienceEvent(title, description, 0);
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void WithExperiencePoints()
		{
			const string title = "Testing Title";
			const int experiencePoints = 100;
			var stringValue =
				$"{{ " +
				$"\"{nameof(IExperienceEvent.Title)}\": \"{title}\", " +
				$"\"{nameof(IExperienceEvent.ExperiencePoints)}\": {experiencePoints} " +
				$"}}";

			var result = JsonConvert.DeserializeObject<IExperienceEvent>(stringValue);

			var expected = new ExperienceEvent(title, string.Empty, experiencePoints);
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
