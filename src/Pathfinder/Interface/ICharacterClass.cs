using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface ICharacterClass
	{
		IClass Class { get; }

		int Level { get; }
		IEnumerable<int> HitPoints { get; }

		int SkillRanks { get; }

		int BaseAttackBonus { get; }
		int Fortitude { get; }
		int Reflex { get; }
		int Will { get; }
	}
}