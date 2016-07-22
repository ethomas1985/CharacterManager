using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface IInventory : IEnumerable<IInventoryItem>
	{
		IInventory Add(IInventoryItem pItem, int pCount = 1);
		IInventory Remove(IInventoryItem pItem);
	}
}
