using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.FeatTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<IFeat>("{}"),
				Throws.Exception
						.TypeOf<JsonException>()
						.With.Message.EqualTo($"Missing Required Attribute: {nameof(IFeat.Name)}"));
		}

		[Test]
		public void RequiresFeatType()
		{
			const string name = "Testing Feat";
			var value = $"{{ \"{nameof(IFeat.Name)}\": \"{name}\" }}";
			Assert.That(
				() => JsonConvert.DeserializeObject<IFeat>(value),
				Throws.Exception
						.TypeOf<JsonException>()
						.With.Message.EqualTo($"Missing Required Attribute: {nameof(IFeat.FeatType)}"));
		}

		[Test]
		public void WithNameAndFeatType()
		{
			const string name = "Testing Feat";
			var featType = FeatType.General;
			var value = $"{{" +
						$"\"{nameof(IFeat.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeat.FeatType)}\": \"{featType.ToString().ToCamelCase()}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeat>(value);
			Assert.That(actual.Name, Is.EqualTo(name));
			Assert.That(actual.FeatType, Is.EqualTo(featType));
		}

		[Test]
		public void WithDescription()
		{
			const string name = "Testing Feat";
			var featType = FeatType.General;
			const string description = "Testing Description";
			var value = $"{{" +
						$"\"{nameof(IFeat.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeat.FeatType)}\": \"{featType}\"," +
						$"\"{nameof(IFeat.Description)}\": \"{description}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeat>(value);
			Assert.That(actual.Description, Is.EqualTo(description));
		}

		[Test]
		public void WithBenefit()
		{
			const string name = "Testing Feat";
			var featType = FeatType.General;
			const string benefit = "Testing Benefit";
			var value = $"{{" +
						$"\"{nameof(IFeat.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeat.FeatType)}\": \"{featType.ToString().ToCamelCase()}\"," +
						$"\"{nameof(IFeat.Benefit)}\": \"{benefit}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeat>(value);
			Assert.That(actual.Benefit, Is.EqualTo(benefit));
		}

		[Test]
		public void WithSpecial()
		{
			const string name = "Testing Feat";
			var featType = FeatType.General;
			const string special = "Testing Special";
			var value = $"{{" +
						$"\"{nameof(IFeat.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeat.FeatType)}\": \"{featType.ToString().ToCamelCase()}\"," +
						$"\"{nameof(IFeat.Special)}\": \"{special}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeat>(value);
			Assert.That(actual.Special, Is.EqualTo(special));
		}

		[Test]
		public void WithPrerequisites()
		{
			const string name = "Testing Feat";
			var featType = FeatType.General;
			const string prerequisite = "Testing Prerequisite";
			var value = $"{{" +
						$"\"{nameof(IFeat.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeat.FeatType)}\": \"{featType.ToString().ToCamelCase()}\"," +
						$"\"{nameof(IFeat.Prerequisites)}\": [\"{prerequisite}\"]" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeat>(value);
			Assert.That(actual.Prerequisites, Is.EquivalentTo(new List<string> { prerequisite }));
		}

		[Test]
		public void WithSpecialization()
		{
			const string name = "Testing Feat";
			var featType = FeatType.General;
			const string specialization = "Testing Specialization";
			var value = $"{{" +
						$"\"{nameof(IFeat.Name)}\": \"{name}\"," +
						$"\"{nameof(IFeat.FeatType)}\": \"{featType.ToString().ToCamelCase()}\"," +
						$"\"{nameof(IFeat.Specialization)}\": \"{specialization}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IFeat>(value);
			Assert.That(actual.Specialization, Is.EqualTo(specialization));
		}
	}
}
