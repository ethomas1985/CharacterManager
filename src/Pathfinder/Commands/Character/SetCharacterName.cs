using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetCharacterName : ICommand
	{
		public SetCharacterName(Guid pId, int pOriginalVersion, string pName)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Name = pName;
		}
		public Guid Id { get; }
		public int OriginalVersion { get; }
		public string Name { get; }
	}
}
