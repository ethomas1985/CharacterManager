using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.FeatureTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<IFeature>("{}"),
				Throws.Exception
						.TypeOf<JsonException>()
						.With.Message.EqualTo($"Missing Required Attribute: {nameof(IFeature.Name)}"));
		}

		[Test]
		public void WithName()
		{
			const string name = "Testing Feature";
			var value = $"{{" +
						$"\"{nameof(IFeature.Name)}\": \"{name}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeature>(value);
			Assert.That(actual.Name, Is.EqualTo(name));
		}

		[Test]
		public void WithBody()
		{
			const string name = "Testing Feature";
			const string body = "Testing Feature Body";
			var value = $"{{" +
						$"\"{nameof(IFeature.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeature.Body)}\": \"{body}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeature>(value);
			Assert.That(actual.Body, Is.EqualTo(body));
		}

		[Test]
		public void WithAbilityType()
		{
			const string name = "Testing Feature";
			const FeatureAbilityType abilityType = FeatureAbilityType.SpellLike;
			var value = $"{{" +
						$"\"{nameof(IFeature.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeature.AbilityType)}\": \"{abilityType}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeature>(value);
			Assert.That(actual.AbilityType, Is.EqualTo(abilityType));
		}

		[Test]
		public void WithSubFeatures()
		{
			const string name = "Testing Feature";
			var subFeature = new SubFeature("Testing Subfeature", "Body", FeatureAbilityType.Normal);
			var value = $"{{" +
						$"\"{nameof(IFeature.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeature.SubFeatures)}\": [ {JsonConvert.SerializeObject(subFeature)} ]" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeature>(value);
			Assert.That(actual.SubFeatures, Is.EquivalentTo(new[] { subFeature }));
		}
	}
}
