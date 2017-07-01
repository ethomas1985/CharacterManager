using System;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Model
{
	internal class Language : ILanguage, IEquatable<ILanguage>
	{
		public Language(string pName)
		{
			Name = pName;
		}

		public string Name { get; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object pObject)
		{
			return Equals(pObject as ILanguage);
		}

		public bool Equals(ILanguage pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}
			return string.Equals(Name, pOther.Name);
		}

		public override int GetHashCode()
		{
			return Name?.GetHashCode() ?? 0;
		}
	}
}
