using Pathfinder.Interface.Item;
using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface IInventory : IEnumerable<IInventoryItem>
	{
		IInventory Add(IItem pItem, int pCount = 1);
		IInventory Remove(IItem pItem, int pQuantity = 1);

		decimal Load { get; }
	}
}
