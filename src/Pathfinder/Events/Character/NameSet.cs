using System;

namespace Pathfinder.Events.Character {
	internal class NameSet : AbstractEvent, IEquatable<NameSet>
	{
		public NameSet(Guid pId, int pVersion, string pName)
			: base(pId, pVersion)
		{
			Name = pName;
		}

		public string Name { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Name set to {Name} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as NameSet);
		}

		public bool Equals(NameSet pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return base.Equals(pOther)
				&& string.Equals(Name, pOther.Name);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			}
		}
	}
}