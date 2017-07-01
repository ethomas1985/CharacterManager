using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.InventoryTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var inventory = new Inventory();

			Assert.That(
				() => JsonConvert.SerializeObject(inventory),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var item = new Item("Test Name", ItemType.None, "Test Category", new Purse(1), 1.0m, "Test Description");
			var inventory = new Inventory().Add(item, 1);

			var actual = JsonConvert.SerializeObject(inventory);

			var itemJson = JsonConvert.SerializeObject(item);
			var expected =
				new StringBuilder("[")
					.Append("{")
					.Append($"\"{nameof(KeyValuePair<IItem, int>.Key)}\":{itemJson},") // Closing Item
					.Append($"\"{nameof(KeyValuePair<IItem, int>.Value)}\":1")
					.Append("}")
				.Append("]")
				.ToString();

			//File.WriteAllText($"{nameof(InventoryJsonSerializer)}.Test.result.json", actual);
			//File.WriteAllText($"{nameof(InventoryJsonSerializer)}.Test.expected.json", expected);

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
