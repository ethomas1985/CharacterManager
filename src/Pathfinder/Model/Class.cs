using System.Collections.Generic;
using Pathfinder.Enum;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Class : IClass
	{
		public Class(
			string name,
			ISet<Alignment> alignments,
			IDie hitDie,
			ISet<ISkill> skills,
			IEnumerable<IClassLevel> classLevels,
			IEnumerable<IFeature> features)
		{
			Alignments = alignments;
			HitDie = hitDie;
			Skills = skills;
			ClassLevels = classLevels;
			Features = features;
			Name = name;
		}

		public string Name { get; }
		public ISet<Alignment> Alignments { get; }
		public IDie HitDie { get; }
		public ISet<ISkill> Skills { get; }
		public IEnumerable<IClassLevel> ClassLevels { get; }
		public IEnumerable<IFeature> Features { get; }
	}
}
