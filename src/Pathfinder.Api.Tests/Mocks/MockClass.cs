using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Api.Tests.Mocks
{
	public class MockClass : IClass
	{
		public string Name { get; set; }
		public ISet<Alignment> Alignments { get; }
		public IDie HitDie { get; }
		public int SkillAddend { get; }
		public ISet<string> Skills { get; }
		public IEnumerable<IClassLevel> ClassLevels { get; }
		public IEnumerable<string> Features { get; }

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
