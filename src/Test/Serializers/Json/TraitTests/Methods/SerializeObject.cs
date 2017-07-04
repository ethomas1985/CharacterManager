using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.TraitTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var trait =
				new Trait(
					"Test Trait",
					"Test Text",
					true,
					new Dictionary<string, int>());

			Assert.That(
						() => JsonConvert.SerializeObject(trait),
						Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var trait =
				new Trait(
					"Test Trait",
					"Test Text",
					true,
					new Dictionary<string, int>());

			var actual = JsonConvert.SerializeObject(trait);

			var propertyModifiersString = string.Join(",", trait.PropertyModifiers.Select(x => $"\"{x.Key}\":{x.Value}"));
			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(ITrait.Name)}\":\"{trait.Name}\",")
					.Append($"\"{nameof(ITrait.Text)}\":\"{trait.Text}\",")
					.Append($"\"{nameof(ITrait.Conditional)}\":{trait.Conditional.ToString().ToLower()},")
					.Append($"\"{nameof(ITrait.PropertyModifiers)}\":{{")
					.Append($"{propertyModifiersString}")
					.Append($"}}")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
