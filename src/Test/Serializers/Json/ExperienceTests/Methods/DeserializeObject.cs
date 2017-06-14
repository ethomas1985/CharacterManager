using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.ExperienceTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void Empty()
		{
			var result = JsonConvert.DeserializeObject<IExperience>("[]");

			var expected = new Experience();
			Assert.That(result, Is.EqualTo(expected));
		}
		[Test]
		public void WithEvents()
		{
			const string title = "Testing Title";
			const int experiencePoints = 100;
			var stringValue =
				$"[ " +
				$"{{ " +
				$"\"{nameof(IEvent.Title)}\": \"{title} 1\", " +
				$"\"{nameof(IEvent.ExperiencePoints)}\": {experiencePoints} " +
				$"}}, " +
				$"{{ " +
				$"\"{nameof(IEvent.Title)}\": \"{title} 2\", " +
				$"\"{nameof(IEvent.ExperiencePoints)}\": {experiencePoints} " +
				$"}}, " +
				$"{{ " +
				$"\"{nameof(IEvent.Title)}\": \"{title} 3\", " +
				$"\"{nameof(IEvent.ExperiencePoints)}\": {experiencePoints} " +
				$"}} " +
				$"]";
			var result = JsonConvert.DeserializeObject<IExperience>(stringValue);

			var expected =
				new Experience()
					.Append(new Event($"{title} 1", string.Empty, experiencePoints))
					.Append(new Event($"{title} 2", string.Empty, experiencePoints))
					.Append(new Event($"{title} 3", string.Empty, experiencePoints));
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
