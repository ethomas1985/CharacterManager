using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetDeity : ICommand
	{
		public SetDeity(Guid pId, int pOriginalVersion, IDeity pDeity)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Deity = pDeity;
		}
		public Guid Id { get; }
		public int OriginalVersion { get; }
		public IDeity Deity { get; }
	}
}