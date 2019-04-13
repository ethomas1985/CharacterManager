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
        private readonly ImmutableDictionary<string, InventoryItem> _inventory;
        private readonly Lazy<decimal> _load;

        private IDictionary<string, InventoryItem> AsDictionary => _inventory;

        public Inventory() : this(ImmutableDictionary<string, InventoryItem>.Empty) { }

        private Inventory(IDictionary<string, InventoryItem> pList)
        {
            _inventory = pList.ToImmutableDictionary();
            _load = new Lazy<decimal>(() => _inventory.Sum(x => x.Value.Item.Weight * x.Value.Quantity));
        }

        public IInventory Add(IItem pItem, int pQuantity)
        {
            Assert.ArgumentNotNull(pItem, nameof(pItem));

            var quantity = Math.Max(pQuantity, 1);
            return _inventory.TryGetValue(pItem.Name, out InventoryItem inventoryItem)
                ? new Inventory(_inventory.SetItem(inventoryItem.Item.Name, inventoryItem.Add(quantity)))
                : new Inventory(_inventory.SetItem(pItem.Name, new InventoryItem(pItem, quantity)));
        }

        public IInventory Remove(IItem pItem, int pQuantity)
        {
            Assert.ArgumentNotNull(pItem, nameof(pItem));

            if (!_inventory.TryGetValue(pItem.Name, out InventoryItem inventoryItem))
            {
                throw new ArgumentException("Item not in inventory.");
            }

            inventoryItem = inventoryItem.Remove(Math.Max(pQuantity, 1));
            return inventoryItem.Quantity < 1
                ? new Inventory(_inventory.Remove(pItem.Name))
                : new Inventory(_inventory.SetItem(pItem.Name, inventoryItem));
        }

        public decimal Load => _load.Value;

        public bool Contains(IItem pKey)
        {
            return _inventory.ContainsKey(pKey.Name);
        }

        public int this[IItem pKey] => AsDictionary.TryGetValue(pKey.Name, out var value) ? value.Quantity : 0;

        public IEnumerator<IInventoryItem> GetEnumerator()
        {
            return _inventory.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_inventory.Values).GetEnumerator();
        }
    }

    internal class InventoryItem : IInventoryItem, IEquatable<IInventoryItem>
    {
        public InventoryItem(IItem pItem, int pQuantity)
        {
            Item = pItem;
            Quantity = pQuantity;
        }

        public IItem Item { get; }
        public int Quantity { get; }

        public InventoryItem Add(int pToAdd)
        {
            return new InventoryItem(Item, Quantity + pToAdd);
        }

        public InventoryItem Remove(int pToRemove)
        {
            return new InventoryItem(Item, Quantity - pToRemove);
        }

        public override string ToString()
        {
            return $"{Item}[{Quantity}]";
        }

        public bool Equals(IInventoryItem pOther)
        {
            if (ReferenceEquals(null, pOther))
            {
                return false;
            }

            if (ReferenceEquals(this, pOther))
            {
                return true;
            }

            if (pOther.GetType() != this.GetType())
            {
                return false;
            }

            return Equals(Item, pOther.Item) && Quantity == pOther.Quantity;
        }

        public override bool Equals(object pOther)
        {
            return Equals(pOther as IInventoryItem);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Item != null ? Item.GetHashCode() : 0) * 397) ^ Quantity;
            }
        }
    }
}
