using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Class : IClass
	{
		public Class(
			string pName,
			ISet<Alignment> pAlignments,
			IDie pHitDie,
			ISet<string> pSkills,
			IEnumerable<IClassLevel> pClassLevels,
			IEnumerable<string> pFeatures)
		{
			Alignments = pAlignments;
			HitDie = pHitDie;
			Skills = pSkills;
			ClassLevels = pClassLevels;
			Features = pFeatures;
			Name = pName;
		}

		public string Name { get; }
		public ISet<Alignment> Alignments { get; }
		public IDie HitDie { get; }
		public ISet<string> Skills { get; }
		public IEnumerable<IClassLevel> ClassLevels { get; }
		public IEnumerable<string> Features { get; }
	}
}
