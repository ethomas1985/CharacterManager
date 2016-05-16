using System.Collections;
using System.Collections.Generic;
using Pathfinder.Interface;

namespace Pathfinder.Api.Tests.Mocks
{
	internal class MockRaceLibrary : ILibrary<IRace>
	{
		private const string TEST_RACE = "Test Race";

		private readonly Dictionary<string, IRace> _library =
			new Dictionary<string, IRace>
			{
				[TEST_RACE] = new MockRace { Name = TEST_RACE}
			};

		public IEnumerable<string> Keys => _library.Keys;
		public IEnumerable<IRace> Values => _library.Values;
		public IRace this[string pKey] => _library[pKey];

		public bool TryGetValue(string pKey, out IRace pValue)
		{
			return _library.TryGetValue(pKey, out pValue);
		}

		public void Store(IRace pValue)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerator<IRace> GetEnumerator()
		{
			return Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
