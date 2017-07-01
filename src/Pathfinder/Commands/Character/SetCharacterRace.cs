using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetCharacterRace : ICommand
	{
		public SetCharacterRace(Guid pId, int pOriginalVersion, string pRaceName)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			RaceName = pRaceName;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }

		public string RaceName { get; }
	}
}
