using System;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character {
	public class SetHomeland : ICommand
	{
		public SetHomeland(Guid pId, int pOriginalVersion, string pHomeland)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Homeland = pHomeland;
		}
		public Guid Id { get; }
		public int OriginalVersion { get; }
		public string Homeland { get; }
	}
}