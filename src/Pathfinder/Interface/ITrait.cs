using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface ITrait
	{
		string Name { get; }
		bool Active{ get; set; }
		string Text { get; }

		IDictionary<string, int> PropertyModifiers { get; }
	}
}
