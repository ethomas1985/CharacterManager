using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface ILibrary<T> : IEnumerable<T>
	{
		IEnumerable<string> Keys { get; }
		IEnumerable<T> Values { get; }
		T this[string pKey] { get; }

		void Store(T pValue);
	}
}
