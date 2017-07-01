using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Api.Models
{
	public class ExperienceEventImpl : IExperienceEvent
	{
		public ExperienceEventImpl(string pTitle, string pDescription, int pExperiencePoints)
		{
			Title = pTitle;
			Description = pDescription;
			ExperiencePoints = pExperiencePoints;
		}
		public string Title { get; }
		public string Description { get; }
		public int ExperiencePoints { get; }
	}
}