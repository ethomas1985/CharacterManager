using Pathfinder.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Model.Items
{
	internal class Inventory : IInventory
	{
		private readonly ImmutableDictionary<IItem, int> _inventory;
		private readonly Lazy<decimal> _load;

		private IDictionary<IItem, int> AsDictionary => _inventory;

		public Inventory() : this(ImmutableDictionary<IItem, int>.Empty)
		{ }

		private Inventory(IDictionary<IItem, int> pList)
		{
			_inventory = pList.ToImmutableDictionary();
			_load = new Lazy<decimal>(() => _inventory.Sum(x => x.Key.Weight * x.Value));
		}

		public IInventory Add(IItem pItem, int pQuantity)
		{
			Assert.ArgumentNotNull(pItem, nameof(pItem));

			var quantity = Math.Max(pQuantity, 1);
			return _inventory.TryGetKey(pItem, out IItem keyItem)
				? new Inventory(_inventory.SetItem(keyItem, _inventory[keyItem] + quantity))
				: new Inventory(_inventory.SetItem(pItem, quantity));
		}

		public IInventory Remove(IItem pItem, int pQuantity)
		{
			Assert.ArgumentNotNull(pItem, nameof(pItem));

			if (!_inventory.TryGetKey(pItem, out IItem keyItem))
			{
				throw new ArgumentException("Item not in inventory.");
			}

			var quantity = _inventory[keyItem] - Math.Max(pQuantity, 1);
			return quantity < 1 
				? new Inventory(_inventory.Remove(keyItem))
				: new Inventory(_inventory.SetItem(keyItem, quantity));
		}

		public decimal Load => _load.Value;

		public bool Contains(IItem pKey)
		{
			return _inventory.ContainsKey(pKey);
		}

		public bool TryGetValue(IItem pKey, out int pValue)
		{
			return _inventory.TryGetValue(pKey, out pValue);
		}

		public int this[IItem pKey] => AsDictionary.TryGetValue(pKey, out int value) ? value : 0;

		public IEnumerator<KeyValuePair<IItem, int>> GetEnumerator()
		{
			return _inventory.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _inventory).GetEnumerator();
		}
	}
}
