using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class IncrementCharacterClass : ICommand
	{
		public IncrementCharacterClass(Guid pId, int pOriginalVersion, IClass pClass, int pHitPoints)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Class = pClass;
			HitPoints = pHitPoints;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public IClass Class { get; }
		public int HitPoints { get; }
	}
}