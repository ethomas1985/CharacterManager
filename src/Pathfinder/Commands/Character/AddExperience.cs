using System;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class AddExperience : ICommand, IExperienceEvent
	{
		public AddExperience(Guid pId, string pTitle, string pDescription, int pExperiencePoints, int pOriginalVersion)
		{
			Id = pId;
			Title = pTitle;
			Description = pDescription;
			ExperiencePoints = pExperiencePoints;
			OriginalVersion = pOriginalVersion;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }

		public string Title { get; }
		public string Description { get; }
		public int ExperiencePoints { get; }
	}
}
