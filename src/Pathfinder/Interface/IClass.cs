using System.Collections.Generic;
using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface IClass
	{
		int BaseAttackBonus { get; }
		int Fortitude { get; }
		int Reflex { get; }
		int Will { get; }
		int SkillRanks { get; }
		int FCSSkills { get; }
		int FCHp { get; }

		int Level { get; }

		IHitDice HitDice { get; }

		ISet<Alignment> Alignments { get; }
			 
		ISet<ISkill> Skills { get; }

		IEnumerable<IFeature> Features { get; }
	}
}