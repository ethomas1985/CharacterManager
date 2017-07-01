using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.RaceTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var race =
				new Race(
					"Testing Race",
					"Testing Adjective",
					"Testing Description",
					Size.Medium,
					30,
					new Dictionary<AbilityType, int>
					{
						[AbilityType.Dexterity] = 5
					},
					new List<ITrait>(),
					new List<ILanguage>());

			Assert.That(
				() => JsonConvert.SerializeObject(race),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var race =
				new Race(
						 "Testing Race",
						 "Testing Adjective",
						 "Testing Description",
						 Size.Medium,
						 30,
						 new Dictionary<AbilityType, int>(),
						 new List<ITrait>(),
						 new List<ILanguage>());
			var actual = JsonConvert.SerializeObject(race);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IRace.Name)}\":\"{race.Name}\",")
					.Append($"\"{nameof(IRace.Adjective)}\":\"{race.Adjective}\",")
					.Append($"\"{nameof(IRace.Description)}\":\"{race.Description}\",")
					.Append($"\"{nameof(IRace.Size)}\":\"{race.Size.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IRace.BaseSpeed)}\":{race.BaseSpeed}")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
