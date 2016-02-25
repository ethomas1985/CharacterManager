using Pathfinder.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinder.Model
{
	internal class SkillsCollection : Dictionary<string, Skill>, IDictionary<string, ISkill>
	{
		ISkill IDictionary<string, ISkill>.this[string key]
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		int ICollection<KeyValuePair<string, ISkill>>.Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ICollection<KeyValuePair<string, ISkill>>.IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		ICollection<string> IDictionary<string, ISkill>.Keys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		ICollection<ISkill> IDictionary<string, ISkill>.Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		void ICollection<KeyValuePair<string, ISkill>>.Add(KeyValuePair<string, ISkill> item)
		{
			throw new NotImplementedException();
		}

		void IDictionary<string, ISkill>.Add(string key, ISkill value)
		{
			throw new NotImplementedException();
		}

		void ICollection<KeyValuePair<string, ISkill>>.Clear()
		{
			throw new NotImplementedException();
		}

		bool ICollection<KeyValuePair<string, ISkill>>.Contains(KeyValuePair<string, ISkill> item)
		{
			throw new NotImplementedException();
		}

		bool IDictionary<string, ISkill>.ContainsKey(string key)
		{
			throw new NotImplementedException();
		}

		void ICollection<KeyValuePair<string, ISkill>>.CopyTo(KeyValuePair<string, ISkill>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		IEnumerator<KeyValuePair<string, ISkill>> IEnumerable<KeyValuePair<string, ISkill>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		bool ICollection<KeyValuePair<string, ISkill>>.Remove(KeyValuePair<string, ISkill> item)
		{
			throw new NotImplementedException();
		}

		bool IDictionary<string, ISkill>.Remove(string key)
		{
			throw new NotImplementedException();
		}

		bool IDictionary<string, ISkill>.TryGetValue(string key, out ISkill value)
		{
			throw new NotImplementedException();
		}
	}
}