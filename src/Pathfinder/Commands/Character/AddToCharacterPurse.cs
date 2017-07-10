using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class AddToCharacterPurse : ICommand
	{
		public AddToCharacterPurse(
			Guid id, int originalVersion,
			int pCopper,
			int pSilver,
			int pGold,
			int pPlatinum)
		{
			Id = id;
			OriginalVersion = originalVersion;
			Copper = pCopper;
			Silver = pSilver;
			Gold = pGold;
			Platinum = pPlatinum;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public int Copper { get; }
		public int Silver { get; }
		public int Gold { get; }
		public int Platinum { get; }
	}
}