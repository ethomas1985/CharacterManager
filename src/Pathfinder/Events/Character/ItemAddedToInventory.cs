using System;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Events.Character
{
	internal class ItemAddedToInventory : AbstractEvent, IEquatable<ItemAddedToInventory>
	{
		public ItemAddedToInventory(Guid pId, int pVersion, IItem pItem)
			: base(pId, pVersion)
		{
			Item = pItem;
		}
		public IItem Item { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Item)} '{Item}' Added to Inventory | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ItemAddedToInventory);
		}

		public bool Equals(ItemAddedToInventory pOther)
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