using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Commands
{
	public static class AddExperienceCommand
	{
		public static ICharacter Execute(ICharacter pThis, IEvent pEvent)
		{
			return pThis.AppendExperience(pEvent);
		}

		public static ICharacter Execute(ICharacter pThis, string pEventName, string pEventDescription, int pExperiencePoints)
		{
			return pThis.AppendExperience(new Event(pEventName, pEventDescription, pExperiencePoints));
		}
	}
}
