using System;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class CreateCharacter : ICommand
	{
		public CreateCharacter(Guid pId)
		{
			Id = pId;
			OriginalVersion = 0;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
	}
}
