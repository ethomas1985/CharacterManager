using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using System.Collections;
using System.Collections.Generic;

namespace Test.Mocks
{
	public class MockClassLibrary : ILibrary<IClass>
	{
		private const string TEST_CLASS = "Test Class";

		private readonly Dictionary<string, IClass> _library =
			new Dictionary<string, IClass>
			{
				[TEST_CLASS] =
					new Class(
						pName:TEST_CLASS,
						pAlignments:new HashSet<Alignment>
						{
							Alignment.Neutral
						},
						pHitDie:new Die(6),
						pSkillAddend:4,
						pSkills:new HashSet<string>(),
						pClassLevels:new List<IClassLevel>
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
								pSpecials: new List<string> {},
								pSpellsPerDay: new Dictionary<int, int>(),
								pSpellsKnown: new Dictionary<int, int>(),
								pSpells: new Dictionary<int, IEnumerable<string>>())
						},
						pFeatures:new List<string>())
			};

		public IEnumerable<string> Keys => _library.Keys;
		public IEnumerable<IClass> Values => _library.Values;
		public IClass this[string pKey] => _library[pKey];

		public bool TryGetValue(string pKey, out IClass pValue)
		{
			return _library.TryGetValue(pKey, out pValue);
		}

		public void Store(IClass pValue)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerator<IClass> GetEnumerator()
		{
			return Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}