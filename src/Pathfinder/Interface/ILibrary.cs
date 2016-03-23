using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface ILibrary<out T> : IEnumerable<T>
	{
		T this[string pKey] { get; }
	}
}
