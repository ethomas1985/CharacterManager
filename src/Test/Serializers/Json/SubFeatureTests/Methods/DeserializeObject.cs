using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Test.Serializers.Json.SubFeatureTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
					    () => JsonConvert.DeserializeObject<ISubFeature>("{}"),
					    Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Missing Required Attribute: {nameof(ISubFeature.Name)}"));
		}

		[Test]
		public void WithName()
		{
			const string name = "Testing SubFeature";
			var value = $"{{" +
						$"\"{nameof(ISubFeature.Name)}\": \"{name}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<ISubFeature>(value);
			Assert.That(actual.Name, Is.EqualTo(name));
		}

		[Test]
		public void WithBody()
		{
			const string name = "Testing SubFeature";
			const string body = "Testing SubFeature Body";
			var value = $"{{" +
						$"\"{nameof(ISubFeature.Name)}\": \"{name}\"," +
						$"\"{nameof(ISubFeature.Body)}\": \"{body}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<ISubFeature>(value);
			Assert.That(actual.Body, Is.EqualTo(body));
		}

		[Test]
		public void WithAbilityType()
		{
			const string name = "Testing SubFeature";
			const FeatureAbilityType abilityType = FeatureAbilityType.SpellLike;
			var value = $"{{" +
						$"\"{nameof(ISubFeature.Name)}\": \"{name}\"," +
						$"\"{nameof(ISubFeature.AbilityType)}\": \"{abilityType}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<ISubFeature>(value);
			Assert.That(actual.AbilityType, Is.EqualTo(abilityType));
		}
	}
}
