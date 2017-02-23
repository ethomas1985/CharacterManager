namespace Pathfinder.Interface
{
	public interface IDice
	{
		int DieCount { get; }
		IDie Die { get; }
	}
}