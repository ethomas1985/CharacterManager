using System;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character {
	public class SetHair : ICommand
	{
		public SetHair(Guid pId, int pOriginalVersion, string pHair)
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