using System.Collections;
using System.Collections.Generic;
using Pathfinder.Interface;

namespace Pathfinder.Api.Tests.Mocks
{
	internal class MockSkillLibrary : ILibrary<ISkill>
	{
		private readonly Dictionary<string, ISkill> _library =
		new Dictionary<string, ISkill>
		{
			["Test Race"] = new MockSkill { Name = "Test Race" }
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