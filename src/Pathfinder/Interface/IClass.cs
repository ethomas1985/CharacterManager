using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface IClass : INamed
	{
		ISet<Alignment> Alignments { get; }
		IDie HitDie { get; }
		int SkillAddend { get; }
		ISet<string> Skills { get; }
		IEnumerable<IClassLevel> ClassLevels { get; }
		IEnumerable<string> Features { get; }

		IClassLevel this[int pLevel] { get; }

		bool TryGetLevel(int pLevel, out IClassLevel pValue);
	}
}
