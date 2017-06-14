using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface ITrait : INamed
	{
		bool Active{ get; }
		string Text { get; }

		IDictionary<string, int> PropertyModifiers { get; }
	}
}
