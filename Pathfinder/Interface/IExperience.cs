using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface IExperience : ICollection<IEvent>
	{
		int Total { get; }
	}
}
