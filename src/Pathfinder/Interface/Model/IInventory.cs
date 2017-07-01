using System.Collections.Generic;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Interface.Model
{
	public interface IInventory : IEnumerable<KeyValuePair<IItem, int>>
	{
		int this[IItem pKey] { get; }

		IInventory Add(IItem pItem, int pCount);
		IInventory Remove(IItem pItem, int pQuantity);

		bool TryGetValue(IItem pKey, out int pValue);
		bool Contains(IItem pKey);

		decimal Load { get; }
	}
}
