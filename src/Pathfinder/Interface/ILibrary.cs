using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface ILibrary<T> : IEnumerable<T>
	{
		IEnumerable<string> Keys { get; }
		IEnumerable<T> Values { get; }
		T this[string pKey] { get; }

		bool TryGetValue(string pKey, out T pValue);

		void Store(T pValue);
	}
}
