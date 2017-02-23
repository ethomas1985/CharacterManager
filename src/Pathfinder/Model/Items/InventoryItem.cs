using Pathfinder.Interface;
using Pathfinder.Interface.Item;

namespace Pathfinder.Model.Items
{
	internal class InventoryItem : IInventoryItem
	{
		public InventoryItem(IItem pItem, int pQuantity)
		{
			Item = pItem;
			Quantity = pQuantity;
		}

		public IItem Item { get; }
		public int Quantity { get; }
	}
}