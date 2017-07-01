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

namespace Pathfinder.Test.Serializers.Json.FeatTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var feat =
				new Feat(
					"Test Feat",
					FeatType.General,
					new List<string>(),
					"Test Description",
					"Test Benefit",
					"Test Special");

			Assert.That(
				() => JsonConvert.SerializeObject(feat),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var feat =
				new Feat(
						 "Test Feat",
						 FeatType.General,
						 new List<string> { "Test Prerequisite" },
						 "Test Description",
						 "Test Benefit",
						 "Test Special");

			var actual = JsonConvert.SerializeObject(feat);

			var prerequisites = string.Join(",", feat.Prerequisites.Select(x => $"\"{x}\""));
			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IFeat.Name)}\":\"{feat.Name}\",")
					.Append($"\"{nameof(IFeat.FeatType)}\":\"{feat.FeatType.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IFeat.Description)}\":\"{feat.Description}\",")
					.Append($"\"{nameof(IFeat.Benefit)}\":\"{feat.Benefit}\",")
					.Append($"\"{nameof(IFeat.Special)}\":\"{feat.Special}\",")
					.Append($"\"{nameof(IFeat.Prerequisites)}\":[{prerequisites}]")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void ExpectedSpecialized()
		{
			var feat =
				new Feat(
					"Test Feat",
					FeatType.General,
					new List<string> { "Test Prerequisite" },
					"Test Description",
					"Test Benefit",
					"Test Special",
					"Test Specialization");

			var actual = JsonConvert.SerializeObject(feat);

			var prerequisites = string.Join(",", feat.Prerequisites.Select(x => $"\"{x}\""));
			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IFeat.Name)}\":\"{feat.Name}\",")
					.Append($"\"{nameof(IFeat.FeatType)}\":\"{feat.FeatType.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IFeat.Description)}\":\"{feat.Description}\",")
					.Append($"\"{nameof(IFeat.Benefit)}\":\"{feat.Benefit}\",")
					.Append($"\"{nameof(IFeat.Special)}\":\"{feat.Special}\",")
					.Append($"\"{nameof(IFeat.Prerequisites)}\":[{prerequisites}],")
					.Append($"\"{nameof(IFeat.Specialization)}\":\"{feat.Specialization}\"")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
