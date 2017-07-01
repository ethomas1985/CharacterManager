using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.SubFeatureTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var subFeature =
				new SubFeature(
					"Test Feature",
					"Test Body",
					FeatureAbilityType.Normal);
			Assert.That(
						() => JsonConvert.SerializeObject(subFeature),
						Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var subFeature =
				new SubFeature(
					"Test Feature",
					"Test Body",
					FeatureAbilityType.Normal);

			var actual = JsonConvert.SerializeObject(subFeature);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(ISubFeature.Name)}\":\"{subFeature.Name}\",")
					.Append($"\"{nameof(ISubFeature.Body)}\":\"{subFeature.Body}\",")
					.Append($"\"{nameof(ISubFeature.AbilityType)}\":\"{subFeature.AbilityType.ToString().ToCamelCase()}\"")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
