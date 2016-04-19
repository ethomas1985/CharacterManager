using System.Collections.Generic;
using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface IClass
	{
		string Name { get; }

		ISet<Alignment> Alignments { get; }
		IDie HitDie { get; }
		ISet<ISkill> Skills { get; }
		IEnumerable<IClassLevel> ClassLevels { get; }
		IEnumerable<IFeature> Features { get; }
	}
}
