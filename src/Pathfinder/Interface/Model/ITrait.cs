using System.Collections.Generic;

namespace Pathfinder.Interface.Model
{
	public interface ITrait : INamed
	{
		bool Active{ get; }
		string Text { get; }

		IDictionary<string, int> PropertyModifiers { get; }
	}
}
