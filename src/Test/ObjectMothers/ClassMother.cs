using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class ClassMother
	{

		public static IClass Level1Neutral()
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

		public static IClass Chaotic()
		{
			return new Class(
				pName: "Mock Class",
				pAlignments: new HashSet<Alignment>
					{
						Alignment.ChaoticGood,
						Alignment.ChaoticNeutral,
						Alignment.ChaoticEvil
					},
				pHitDie: new Die(6),
				pSkillAddend: 5,
				pSkills: new HashSet<string>(),
				pClassLevels: new IClassLevel[0],
				pFeatures: new string[0]);
		}
	}
}
