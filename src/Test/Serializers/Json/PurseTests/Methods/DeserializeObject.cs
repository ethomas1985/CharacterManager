using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface.Model.Currency;
using Pathfinder.Model.Currency;

namespace Pathfinder.Test.Serializers.Json.PurseTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void Empty()
		{
			var result = JsonConvert.DeserializeObject<IPurse>("{}");

			var expected = new Purse(0);
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void WithItems()
		{
			var stringValue = 
				$"{{" +
				$"\"{nameof(IPurse.Copper)}\": 1," +
				$"\"{nameof(IPurse.Silver)}\": 2," +
				$"\"{nameof(IPurse.Gold)}\": 3," +
				$"\"{nameof(IPurse.Platinum)}\": 4" +
				$"}}";
			var result = JsonConvert.DeserializeObject<IPurse>(stringValue);

			var expected = new Purse(1, 2, 3, 4);
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
