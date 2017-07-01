using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface.Model.Currency;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.ItemTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var item = new Item("Test Name", ItemType.None, "Test Category", new Purse(1), 1.0m, "Test Description");

			Assert.That(
				() => JsonConvert.SerializeObject(item),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var item = new Item("Test Name", ItemType.None, "Test Category", new Purse(1), 1.0m, "Test Description");
			var actual = JsonConvert.SerializeObject(item);

			var expected =
				new StringBuilder("{")
				.Append($"\"{nameof(IItem.Name)}\":\"{item.Name}\",")
				.Append($"\"{nameof(IItem.ItemType)}\":\"{item.ItemType.ToString().ToCamelCase()}\",")
				.Append($"\"{nameof(IItem.Category)}\":\"{item.Category}\",")
				.Append($"\"{nameof(IItem.Description)}\":\"{item.Description}\",")
				.Append($"\"{nameof(IItem.Weight)}\":{item.Weight},")
				.Append($"\"{nameof(IItem.Cost)}\":")
				.Append("{")
				.Append($"\"{nameof(IPurse.Copper)}\":{item.Cost.Copper.Value},")
				.Append($"\"{nameof(IPurse.Silver)}\":{item.Cost.Silver.Value},")
				.Append($"\"{nameof(IPurse.Gold)}\":{item.Cost.Gold.Value},")
				.Append($"\"{nameof(IPurse.Platinum)}\":{item.Cost.Platinum.Value}")
				.Append("}")
				.Append("}")
				.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
