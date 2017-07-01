using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Serializers.Json.TraitTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
						() => JsonConvert.DeserializeObject<ITrait>("{}"),
						Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Missing Required Attribute: {nameof(ITrait.Name)}"));
		}

		[Test]
		public void WithName()
		{
			const string name = "Testing Trait";
			var value = $"{{" +
						$"\"{nameof(ITrait.Name)}\": \"{name}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<ITrait>(value);
			Assert.That(actual.Name, Is.EqualTo(name));
		}

		[Test]
		public void WithText()
		{
			const string name = "Testing Trait";
			const string text = "Testing Text";
			var value = $"{{" +
						$"\"{nameof(ITrait.Name)}\": \"{name}\"," +
						$"\"{nameof(ITrait.Text)}\": \"{text}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<ITrait>(value);
			Assert.That(actual.Text, Is.EqualTo(text));
		}

		[Test]
		public void WithPropertyModifiers()
		{
			const string name = "Testing Trait";
			var value = $"{{" +
						$"\"{nameof(ITrait.Name)}\": \"{name}\"," +
						$"\"{nameof(ITrait.PropertyModifiers)}\": {{" +
						$"\"{nameof(AbilityType.Strength)}\": 1" +
						$"}}" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<ITrait>(value);
			var expected = new Dictionary<string, int>
			{
				[nameof(AbilityType.Strength)] = 1
			};

			Assert.That(actual.PropertyModifiers,
				Is.EquivalentTo(
					expected));
		}
	}
}
