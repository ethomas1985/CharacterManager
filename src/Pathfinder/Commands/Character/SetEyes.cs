using System;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character {
	public class SetEyes : ICommand
	{
		public SetEyes(Guid pId, int pOriginalVersion, string pEyes)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Eyes = pEyes;
		}
		public Guid Id { get; }
		public int OriginalVersion { get; }
		public string Eyes { get; }
	}
}