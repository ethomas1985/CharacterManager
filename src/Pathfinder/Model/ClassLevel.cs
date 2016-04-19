using System.Collections.Generic;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class ClassLevel : IClassLevel
	{
		public ClassLevel(
			int level,
			IEnumerable<int> baseAttackBonus,
			int fortitude,
			int reflex,
			int will,
			IEnumerable<IFeature> specials,
			IDictionary<int, int> spellsPerDay = null,
			IDictionary<int, int> spellsKnown = null,
			IDictionary<int, IEnumerable<ISpell>> spells = null)
		{
			Level = level;
			BaseAttackBonus = baseAttackBonus;
			Fortitude = fortitude;
			Reflex = reflex;
			Will = will;
			Specials = specials;
			SpellsPerDay = spellsPerDay;
			SpellsKnown = spellsKnown;
			Spells = spells;
		}

		public int Level { get; }
		public IEnumerable<int> BaseAttackBonus { get; }
		public int Fortitude { get; }
		public int Reflex { get; }
		public int Will { get; }
		public IEnumerable<IFeature> Specials { get; }
		public IDictionary<int, int> SpellsPerDay { get; }
		public IDictionary<int, int> SpellsKnown { get; }
		public IDictionary<int, IEnumerable<ISpell>> Spells { get; }
	}
}
