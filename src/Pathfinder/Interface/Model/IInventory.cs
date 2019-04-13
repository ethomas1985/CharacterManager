using System.Collections.Generic;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Interface.Model
{
    public interface IInventoryItem
    {
        IItem Item { get; }
        int Quantity { get; }
    }

    public interface IInventory : IEnumerable<IInventoryItem>
    {
        int this[IItem pKey] { get; }

        IInventory Add(IItem pItem, int pCount);
        IInventory Remove(IItem pItem, int pQuantity);

        bool Contains(IItem pKey);

        decimal Load { get; }
    }
}
