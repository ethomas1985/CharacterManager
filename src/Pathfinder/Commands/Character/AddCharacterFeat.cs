using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class AddCharacterFeat : ICommand
	{
		public AddCharacterFeat(Guid pId, int pOriginalVersion, IFeat pFeat)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Feat = pFeat;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public IFeat Feat { get; }
	}
}