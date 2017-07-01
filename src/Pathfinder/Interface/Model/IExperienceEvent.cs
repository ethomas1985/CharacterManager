namespace Pathfinder.Interface.Model
{
	public interface IExperienceEvent
	{
		string Title { get; }
		string Description { get; }

		int ExperiencePoints { get; }
	}
}