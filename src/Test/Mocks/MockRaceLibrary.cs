using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using System.Collections;
using System.Collections.Generic;

namespace Test.Mocks
{
	public class MockRaceLibrary : ILibrary<IRace>
	{
		private const string TEST_RACE = "Test Race";

		private readonly Dictionary<string, IRace> _library =
			new Dictionary<string, IRace>
			{
				[TEST_RACE] =
					new Race(
						TEST_RACE,
						"Test-ish",
						"This is a Test Race Description.",
						Size.Medium,
						30,
						new Dictionary<AbilityType,int>(),
						new List<ITrait>(),
						new List<ILanguage>
						{
							new Language("Test Language")
						})
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
			_library[pValue.Name] = pValue;
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
