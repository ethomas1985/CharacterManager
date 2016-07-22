namespace Pathfinder.Interface
{
	public interface IInventoryItem
	{
		IItem Item { get; }
		int Count { get; }
	}
}