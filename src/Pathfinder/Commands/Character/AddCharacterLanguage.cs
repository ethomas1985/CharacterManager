using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class AddCharacterLanguage : ICommand
	{
		public AddCharacterLanguage(Guid pId, int pOriginalVersion, ILanguage pLanguage)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Language = pLanguage;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public ILanguage Language { get; }
	}
}