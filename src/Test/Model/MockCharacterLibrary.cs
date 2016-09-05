using System.Collections;
using System.Collections.Generic;
using Pathfinder.Interface;

namespace Test.Model
{
	public class MockCharacterLibrary : ILibrary<ICharacter>
	{
		private List<ICharacter> _list = new List<ICharacter>();

		public IEnumerator<ICharacter> GetEnumerator()
		{
			return _list.GetEnumerator();
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