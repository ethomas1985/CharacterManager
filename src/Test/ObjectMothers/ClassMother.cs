using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class ClassMother
	{

		public static IClass Create()
		{
			return new Class(
				pName: "Test Class",
				pAlignments: new HashSet<Alignment>
				{
					Alignment.Neutral
				},
				pHitDie: new Die(6),
				pSkillAddend: 4,
				pSkills: new HashSet<string>(),
				pClassLevels: new List<IClassLevel>
				{
					new ClassLevel(
						pLevel: 1,
						pBaseAttackBonus: new List<int>
						{
							1
						},
						pFortitude: 1,
						pReflex: 1,
						pWill: 1,
						pSpecials: new List<string> { },
						pSpellsPerDay: new Dictionary<int, int>(),
						pSpellsKnown: new Dictionary<int, int>(),
						pSpells: new Dictionary<int, IEnumerable<string>>())
				},
				pFeatures: new List<string>());
		}

		public static IClass Create(string pName, ISet<Alignment> pAlignments, IDie pHitDie, int pSkillAddend)
		{
			return new Class(
				pName: pName,
				pAlignments: pAlignments,
				pHitDie: pHitDie,
				pSkillAddend: pSkillAddend,
				pSkills: new HashSet<string>(),
				pClassLevels: new IClassLevel[0],
				pFeatures: new string[0]);
		}
	}
}
