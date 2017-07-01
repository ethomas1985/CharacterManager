using System;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Events.Character
{
	internal class ItemRemovedFromInventory : AbstractEvent, IEquatable<ItemRemovedFromInventory>
	{
		public ItemRemovedFromInventory(Guid pId, int pVersion, IItem pItem)
			: base(pId, pVersion)
		{
			Item = pItem;
		}
		public IItem Item { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Item)} '{Item}' Removed from Inventory | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ItemRemovedFromInventory);
		}

		public bool Equals(ItemRemovedFromInventory pOther)
		{
			return base.Equals(pOther)
				   && Equals(Item, pOther.Item);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (base.GetHashCode() * 397) ^ (Item != null ? Item.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}