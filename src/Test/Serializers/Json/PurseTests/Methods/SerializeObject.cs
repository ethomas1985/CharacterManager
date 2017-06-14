using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface.Currency;
using Pathfinder.Model.Currency;

namespace Pathfinder.Test.Serializers.Json.PurseTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var purse = new Purse(1, 1, 1, 1);
			Assert.That(
				() => JsonConvert.SerializeObject(purse),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var purse = new Purse(1, 1, 1, 1);
			var actual = JsonConvert.SerializeObject(purse);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IPurse.Copper)}\":{purse.Copper.Value},")
					.Append($"\"{nameof(IPurse.Silver)}\":{purse.Silver.Value},")
					.Append($"\"{nameof(IPurse.Gold)}\":{purse.Gold.Value},")
					.Append($"\"{nameof(IPurse.Platinum)}\":{purse.Platinum.Value}")
					
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
