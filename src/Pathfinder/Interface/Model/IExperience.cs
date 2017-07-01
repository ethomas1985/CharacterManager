using System.Collections.Generic;

namespace Pathfinder.Interface.Model
{
	public interface IExperience : IEnumerable<IExperienceEvent>
	{
		int Total { get; }
		IExperience Append(IExperienceEvent pItem);
		IExperience Append(string pTitle, string pDescription, int pExperiencePoints);
		IExperience Append(IExperience pItem);
		IExperience Remove(IExperienceEvent pEvent);
	}
}
