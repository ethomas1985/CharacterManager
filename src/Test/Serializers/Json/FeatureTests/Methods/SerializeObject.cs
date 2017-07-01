using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.FeatureTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var feature =
				new Feature(
					"Test Feature",
					"Test Body",
					FeatureAbilityType.Normal,
					new List<ISubFeature>());

			Assert.That(
				() => JsonConvert.SerializeObject(feature),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var feature =
				new Feature(
					"Test Feature",
					"Test Body",
					FeatureAbilityType.Normal,
					new List<ISubFeature>
					{
						new SubFeature(
									   "Test SubFeature",
									   "Test SubFeature Body",
									   FeatureAbilityType.Normal)
					});

			var actual = JsonConvert.SerializeObject(feature);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IFeature.Name)}\":\"{feature.Name}\",")
					.Append($"\"{nameof(IFeature.Body)}\":\"{feature.Body}\",")
					.Append($"\"{nameof(IFeature.AbilityType)}\":\"{feature.AbilityType.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IFeature.SubFeatures)}\":[")
					.Append($"{{")
					.Append($"\"{nameof(IFeature.Name)}\":\"{feature.SubFeatures.First().Name}\",")
					.Append($"\"{nameof(IFeature.Body)}\":\"{feature.SubFeatures.First().Body}\",")
					.Append($"\"{nameof(IFeature.AbilityType)}\":\"{feature.SubFeatures.First().AbilityType.ToString().ToCamelCase()}\"")
					.Append($"}}")
					.Append($"]")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
