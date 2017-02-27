using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Utilities;
using System;

namespace Pathfinder.Model.Items
{
	internal class InventoryItem : IInventoryItem, IEquatable<IInventoryItem>
	{
		public InventoryItem(IItem pItem, int pQuantity)
		{
			Item = pItem;
			Quantity = pQuantity;
		}

		public IItem Item { get; }
		public int Quantity { get; }

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IInventoryItem);
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

			return ComparisonUtilities.Compare(GetType().Name, Item, pOther.Item, nameof(Item))
				&& ComparisonUtilities.Compare(GetType().Name, Quantity, pOther.Quantity, nameof(Quantity));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Item?.GetHashCode() ?? 0) * 397) ^ Quantity;
			}
		}
	}
}