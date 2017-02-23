using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Pathfinder.Model.Items
{
	internal class Inventory : IInventory
	{
		private readonly ImmutableList<IInventoryItem> _inventory;
		private readonly Lazy<decimal> _load;

		public Inventory() : this(new List<IInventoryItem>())
		{ }

		private Inventory(IEnumerable<IInventoryItem> pList)
		{
			_inventory = pList.ToImmutableList();
			_load = new Lazy<decimal>(() => _inventory.Sum(x => x.Item.Weight * x.Quantity));
		}

		public IInventory Add(IItem pItem, int pQuantity = 1)
		{
			Assert.ArgumentNotNull(pItem, nameof(pItem));

			var invItem = _inventory.FirstOrDefault(x => pItem.Name.Equals(x.Item.Name));
			var inventoryItems =
				invItem == null
					? _inventory.Add(new InventoryItem(pItem, pQuantity))
					: _inventory.Replace(invItem, new InventoryItem(pItem, pQuantity));

			return new Inventory(inventoryItems);
		}

		public IInventory Remove(IItem pItem, int pQuantity = 1)
		{
			Assert.ArgumentNotNull(pItem, nameof(pItem));

			var invItem = _inventory.FirstOrDefault(x => pItem.Name.Equals(x.Item.Name));
			if (invItem == null)
			{
				return this;
			}

			var inventoryItems =
				invItem.Quantity <= pQuantity
					? _inventory.Remove(invItem)
					: _inventory.Replace(invItem, new InventoryItem(pItem, pQuantity));

			return new Inventory(inventoryItems);
		}

		public decimal Load => _load.Value;

		public IEnumerator<IInventoryItem> GetEnumerator()
		{
			return _inventory.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
