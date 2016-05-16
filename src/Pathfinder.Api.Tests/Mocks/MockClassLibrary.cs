using System.Collections;
using System.Collections.Generic;
using Pathfinder.Interface;

namespace Pathfinder.Api.Tests.Mocks
{
	internal class MockClassLibrary : ILibrary<IClass>
	{
		private const string TEST_CLASS = "Test Class";

		private readonly Dictionary<string, IClass> _library =
			new Dictionary<string, IClass>
			{
				[TEST_CLASS] = new MockClass { Name = TEST_CLASS }
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