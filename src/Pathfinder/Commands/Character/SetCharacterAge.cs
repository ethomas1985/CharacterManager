using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetCharacterAge : ICommand
	{
		public SetCharacterAge(Guid pId, int pOriginalVersion, int pAge)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Age = pAge;
		}
		public Guid Id { get; }
		public int OriginalVersion { get; }
		public int Age { get; }
	}
}
