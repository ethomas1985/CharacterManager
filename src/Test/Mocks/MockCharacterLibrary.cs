using System.Collections;
using System.Collections.Generic;
using Pathfinder.Interface;

namespace Pathfinder.Test.Mocks
{
	internal class MockCharacterLibrary : ILibrary<ICharacter>
	{
		public IEnumerator<ICharacter> GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<string> Keys { get; }
		public IEnumerable<ICharacter> Values { get; }

		public ICharacter this[string pKey]
		{
			get { throw new System.NotImplementedException(); }
		}

		public bool TryGetValue(string pKey, out ICharacter pValue)
		{
			throw new System.NotImplementedException();
		}

		public void Store(ICharacter pValue)
		{
			throw new System.NotImplementedException();
		}
	}
}