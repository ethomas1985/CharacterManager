using System.Collections.Generic;

namespace Pathfinder.Interface.Model
{
	public interface ITrait : INamed
	{
		string Text { get; }
		bool Conditional { get; }

		IDictionary<string, int> PropertyModifiers { get; }
	}
}
