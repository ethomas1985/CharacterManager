using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface IClassLevel
	{
		int Level { get; }

		IEnumerable<int> BaseAttackBonus { get; }
		int Fortitude { get; }
		int Reflex { get; }
		int Will { get; }

		IEnumerable<string> Specials { get; }

		IDictionary<int, int> SpellsPerDay { get; }
		IDictionary<int, int> SpellsKnown { get; }

		IDictionary<int, IEnumerable<string>> Spells { get; }
	}
}