using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Model.Currency;

namespace Pathfinder.Test.Serializers.Json.CurrencyTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var copper = new Copper(5);

			Assert.That(
				() => JsonConvert.SerializeObject(copper),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var copper = new Copper(5);
			var actual = JsonConvert.SerializeObject(copper);

			var expected =
				$"\"{copper.Value} {copper.Denomination}\"";

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
