using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class AddExperienceEvent : ICommand
	{
		public AddExperienceEvent(Guid pId, int pOriginalVersion, IExperienceEvent pEvent)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Event = pEvent;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public IExperienceEvent Event { get; }
	}
}