using System.Collections;
using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Mocks
{
	public class MockSkillLibrary : ILibrary<ISkill>
	{
		public const string TEST_SKILL = "Test Skill";

		private readonly Dictionary<string, ISkill> _library =
		new Dictionary<string, ISkill>
		{
			[TEST_SKILL] = new Skill(
				TEST_SKILL,
				AbilityType.Strength,
				false,
				false,
				"This is a testing Skill",
				null,
				null,
				null,
				null,
				null,
				null)
		};

		public IEnumerable<string> Keys => _library.Keys;
		public IEnumerable<ISkill> Values => _library.Values;
		public ISkill this[string pKey] => _library[pKey];

		public bool TryGetValue(string pKey, out ISkill pValue)
		{
			return _library.TryGetValue(pKey, out pValue);
		}

		public void Store(ISkill pValue)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerator<ISkill> GetEnumerator()
		{
			return Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}