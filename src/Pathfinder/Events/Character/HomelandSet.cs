using System;

namespace Pathfinder.Events.Character {
	internal class HomelandSet : AbstractEvent, IEquatable<HomelandSet>
	{
		public HomelandSet(Guid pId, int pVersion, string pHomeland)
			: base(pId, pVersion)
		{
			Homeland = pHomeland;
		}

		public string Homeland { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Homeland)} set to {Homeland} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as HomelandSet);
		}

		public bool Equals(HomelandSet pOther)
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
				&& string.Equals(Homeland, pOther.Homeland);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Homeland != null ? Homeland.GetHashCode() : 0);
			}
		}
	}
}