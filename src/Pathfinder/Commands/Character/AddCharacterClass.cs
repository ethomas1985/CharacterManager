using System;
using System.Collections.Generic;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class AddCharacterClass : ICommand
	{
		public AddCharacterClass(
			Guid pId, int pOriginalVersion,
			IClass pClass, int pLevel, bool isFavoredClass, IEnumerable<int> hitPoints)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Class = pClass;
			Level = pLevel;
			IsFavoredClass = isFavoredClass;
			HitPoints = hitPoints;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public IClass Class { get; }
		public int Level { get; }
		public bool IsFavoredClass { get; }
		public IEnumerable<int> HitPoints { get; }
	}
}