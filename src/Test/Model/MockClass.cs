using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Test.Model
{
	internal class MockClass : IClass
	{
		public string Name { get; set; }
		public ISet<Alignment> Alignments { get; set; } = new HashSet<Alignment>();
		public IDie HitDie { get; set; }
		public int SkillAddend { get; set; }
		public ISet<string> Skills { get; set; } = new HashSet<string>();
		public IEnumerable<IClassLevel> ClassLevels { get; set; } = new List<IClassLevel>();
		public IEnumerable<string> Features { get; set; } = new List<string>();

		public IClassLevel this[int pLevel]
		{
			get { throw new System.NotImplementedException(); }
		}

		public bool TryGetLevel(int pLevel, out IClassLevel pValue)
		{
			throw new System.NotImplementedException();
		}
	}
}