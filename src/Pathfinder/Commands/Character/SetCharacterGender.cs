using System;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetCharacterGender : ICommand
	{
		public SetCharacterGender(Guid pId, int pOriginalVersion, Gender pGender)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Gender = pGender;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public Gender Gender { get; }
	}
}