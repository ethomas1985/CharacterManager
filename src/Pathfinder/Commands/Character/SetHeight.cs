using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class SetHeight : ICommand
	{
		public SetHeight(Guid pId, int pOriginalVersion, string pHeight)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Height = pHeight;
		}
		public Guid Id { get; }
		public int OriginalVersion { get; }
		public string Height { get; }
	}
}