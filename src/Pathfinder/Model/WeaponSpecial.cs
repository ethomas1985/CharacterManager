using System;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class WeaponSpecial : IWeaponSpecial, IEquatable<IWeaponSpecial>
	{
		public WeaponSpecial(string pName, string pDescription)
		{
			Name = pName;
			Description = pDescription;
		}
		public string Name { get; }
		public string Description { get; }

		public override string ToString()
		{
			return $"{Name}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IWeaponSpecial);
		}

		public bool Equals(IWeaponSpecial pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}
			return ComparisonUtilities.Compare<string>(GetType().Name, Name, pOther.Name, nameof(Name))
				&& ComparisonUtilities.Compare<string>(GetType().Name, Description, pOther.Description, nameof(Description));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Description != null ? Description.GetHashCode() : 0);
			}
		}
	}
}