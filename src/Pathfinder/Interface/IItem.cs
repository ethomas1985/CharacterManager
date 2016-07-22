namespace Pathfinder.Interface
{
	public interface IItem : INamed
	{
		string Category { get; }
		IMoney Cost { get; }
		string Weight { get; }
		string Description { get; }
	}
}