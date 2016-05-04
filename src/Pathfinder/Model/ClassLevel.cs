using System.Collections.Generic;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class ClassLevel : IClassLevel
	{
		public ClassLevel(
			int pLevel,
			IEnumerable<int> pBaseAttackBonus,
			int pFortitude,
			int pReflex,
			int pWill,
			IEnumerable<string> pSpecials,
			IDictionary<int, int> pSpellsPerDay = null,
			IDictionary<int, int> pSpellsKnown = null,
			IDictionary<int, IEnumerable<string>> pSpells = null)
		{
			Level = pLevel;
			BaseAttackBonus = pBaseAttackBonus;
			Fortitude = pFortitude;
			Reflex = pReflex;
			Will = pWill;
			Specials = pSpecials;
			SpellsPerDay = pSpellsPerDay;
			SpellsKnown = pSpellsKnown;
			Spells = pSpells;
		}

		public int Level { get; }
		public IEnumerable<int> BaseAttackBonus { get; }
		public int Fortitude { get; }
		public int Reflex { get; }
		public int Will { get; }
		public IEnumerable<string> Specials { get; }
		public IDictionary<int, int> SpellsPerDay { get; }
		public IDictionary<int, int> SpellsKnown { get; }
		public IDictionary<int, IEnumerable<string>> Spells { get; }
	}
}
