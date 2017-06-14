using Pathfinder.Interface.Item;
using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface IInventory : IDictionary<IItem, int>
	{
		new IInventory Add(IItem pItem, int pCount);
		IInventory Remove(IItem pItem, int pQuantity);

		decimal Load { get; }
	}
}
