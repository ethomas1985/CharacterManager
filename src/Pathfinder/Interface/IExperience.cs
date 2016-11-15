using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface IExperience : IEnumerable<IEvent>
	{
		int Total { get; }
		IExperience Append(IEvent pItem);
		IExperience Append(string pTitle, string pDescription, int pExperiencePoints);
		IExperience Append(IExperience pItem);
		IExperience Remove(IEvent pEvent);
	}
}
