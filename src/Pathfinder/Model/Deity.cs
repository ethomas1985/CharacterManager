using Pathfinder.Interface;
using Pathfinder.Utilities;
using System;

namespace Pathfinder.Model
{
	internal class Deity : IDeity, IEquatable<IDeity>
	{
		public Deity(string pName)
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
			return Equals(pObject as IDeity);
		}

		public bool Equals(IDeity pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return ComparisonUtilities.Compare(GetType().Name, Name, pOther.Name, nameof(Name));
		}

		public override int GetHashCode()
		{
			return Name?.GetHashCode() ?? 0;
		}
	}
}
