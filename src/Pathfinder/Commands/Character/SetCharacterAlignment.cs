using System;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetCharacterAlignment : ICommand
	{
		public SetCharacterAlignment(Guid pId, int pOriginalVersion, Alignment pAlignment)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Alignment = pAlignment;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public Alignment Alignment { get; }
	}
}