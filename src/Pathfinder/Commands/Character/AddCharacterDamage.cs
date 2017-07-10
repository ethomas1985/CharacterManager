using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class AddCharacterDamage : ICommand
	{
		public AddCharacterDamage(Guid pId, int pOriginalVersion, int pDamage)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Damage = pDamage;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public int Damage { get; }
	}
}