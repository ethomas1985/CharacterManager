namespace Pathfinder.Interface
{
	public interface ILibrary<out T>
	{
		T this[string pKey] { get; }
	}
}
