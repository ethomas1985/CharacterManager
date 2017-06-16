using Pathfinder.Interface;

namespace Pathfinder.Api.Models
{
	public class EventImpl : IEvent
	{
		public EventImpl(string pTitle, string pDescription, int pExperiencePoints)
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