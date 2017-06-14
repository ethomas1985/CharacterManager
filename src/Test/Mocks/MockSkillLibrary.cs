using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinder.Test.Mocks
{
	public class MockSkillLibrary : ILibrary<ISkill>
	{
		public const string TEST_SKILL = "Test Skill";
		internal static readonly Skill TestSkill = 
			new Skill(
				TEST_SKILL,
				AbilityType.Strength,
				false,
				false,
				"Testing Description",
				"Testing Check",
				"Testing Action",
				"Testing Try Again",
				"Testing Special",
				"Testing Restriction",
				"Testing Untrained");

		private readonly Dictionary<string, ISkill> _library =
		new Dictionary<string, ISkill>
		{
			[TEST_SKILL] = TestSkill
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
			_library[pValue.Name] = pValue;
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