using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetCharacterHair : ICommand
	{
		public SetCharacterHair(Guid pId, int pOriginalVersion, string pHair)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Hair = pHair;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public string Hair { get; }
	}
}