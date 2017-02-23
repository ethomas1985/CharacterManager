using Pathfinder.Interface.Item;

namespace Pathfinder.Interface
{
	public interface IInventoryItem
	{
		IItem Item { get; }
		int Quantity { get; }
	}
}