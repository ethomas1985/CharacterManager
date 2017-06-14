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
			if (_inventory.TryGetKey(pItem, out IItem keyItem))
			{
				return new Inventory(_inventory.SetItem(keyItem, _inventory[keyItem] + quantity));
			}

			return new Inventory(_inventory.SetItem(pItem, quantity));
		}

		public IInventory Remove(IItem pItem, int pQuantity)
		{
			Assert.ArgumentNotNull(pItem, nameof(pItem));

			if (!_inventory.TryGetKey(pItem, out IItem keyItem))
			{
				throw new ArgumentException("Item not in inventory.");
			}

			var quantity = _inventory[keyItem] - Math.Max(pQuantity, 1);
			if (quantity < 1)
			{
				return new Inventory(_inventory.Remove(keyItem));
			}

			return new Inventory(_inventory.SetItem(keyItem, quantity));
		}

		public decimal Load => _load.Value;

		public bool ContainsKey(IItem pKey)
		{
			return _inventory.ContainsKey(pKey);
		}

		void IDictionary<IItem, int>.Add(IItem pKey, int pValue)
		{
			throw new NotSupportedException();
		}

		public bool Remove(IItem pKey)
		{
			throw new NotSupportedException();
		}

		public bool TryGetValue(IItem pKey, out int pValue)
		{
			return _inventory.TryGetValue(pKey, out pValue);
		}

		public int this[IItem pKey]
		{
			get { return AsDictionary[pKey]; }
			set { AsDictionary[pKey] = value; }
		}

		public ICollection<IItem> Keys => AsDictionary.Keys;

		public ICollection<int> Values => AsDictionary.Values;

		public IEnumerator<KeyValuePair<IItem, int>> GetEnumerator()
		{
			return _inventory.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _inventory).GetEnumerator();
		}

		public void Add(KeyValuePair<IItem, int> pItem)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(KeyValuePair<IItem, int> pItem)
		{
			return _inventory.Contains(pItem);
		}

		public void CopyTo(KeyValuePair<IItem, int>[] pArray, int pArrayIndex)
		{
			AsDictionary.CopyTo(pArray, pArrayIndex);
		}

		public bool Remove(KeyValuePair<IItem, int> pItem)
		{
			throw new NotSupportedException();
		}

		public int Count => _inventory.Count;

		public bool IsReadOnly => ((ICollection<KeyValuePair<IItem, int>>) _inventory).IsReadOnly;
	}
}
