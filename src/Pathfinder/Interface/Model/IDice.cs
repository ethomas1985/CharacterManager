namespace Pathfinder.Interface.Model
{
	public interface IDice
	{
		int DieCount { get; }
		IDie Die { get; }
	}
}