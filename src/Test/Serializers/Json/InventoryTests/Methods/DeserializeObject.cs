﻿using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;

namespace Pathfinder.Test.Serializers.Json.InventoryTests.Methods
{
    [TestFixture]
    public class DeserializeObject
    {
        [Test]
        public void Empty()
        {
            var result = JsonConvert.DeserializeObject<IInventory>("[]");

            var expected = new Inventory();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void WithItems()
        {
            var item1 = new Item("Item 1", ItemType.None, string.Empty, new Purse(1), 1, new[] {string.Empty});
            var item2 = new Item("Item 2", ItemType.None, string.Empty, new Purse(1), 10, new[] {string.Empty});
            var stringValue =
                $"[ " +
                $"{{\"{nameof(IInventoryItem.Item)}\":{JsonConvert.SerializeObject(item1)},\"{nameof(IInventoryItem.Quantity)}\":1}}, " +
                $"{{\"{nameof(IInventoryItem.Item)}\":{JsonConvert.SerializeObject(item2)},\"{nameof(IInventoryItem.Quantity)}\":1}} " +
                $"]";
            var result = JsonConvert.DeserializeObject<IInventory>(stringValue);

            var expected =
                new Inventory()
                    .Add(item1, 1)
                    .Add(item2, 1);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
