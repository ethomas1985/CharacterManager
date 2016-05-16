using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface ICharacterClass
	{
		IClass Class { get; }
		int Level { get; }
		bool IsFavored { get; }

		IEnumerable<int> HitPoints { get; }

		int SkillAddend { get; }

		int BaseAttackBonus { get; }
		int Fortitude { get; }
		int Reflex { get; }
		int Will { get; }

		ICharacterClass IncrementLevel(int pHitPoints);
	}
}