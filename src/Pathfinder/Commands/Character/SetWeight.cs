using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetWeight : ICommand
	{
		public SetWeight(Guid pId, int pOriginalVersion, string pWeight)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Weight = pWeight;
		}
		public Guid Id { get; }
		public int OriginalVersion { get; }
		public string Weight { get; }
	}
}