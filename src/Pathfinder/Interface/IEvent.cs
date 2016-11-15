namespace Pathfinder.Interface
{
	public interface IEvent
	{
		string Title { get; }
		string Description { get; }

		int ExperiencePoints { get; }
	}
}