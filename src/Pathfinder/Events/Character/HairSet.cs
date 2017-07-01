using System;

namespace Pathfinder.Events.Character
{
	internal class HairSet : AbstractEvent, IEquatable<HairSet>
	{
		public HairSet(Guid pId, int pVersion, string pHair)
			: base(pId, pVersion)
		{
			Hair = pHair;
		}

		public string Hair { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Hair)} set to {Hair} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as HairSet);
		}

		public bool Equals(HairSet pOther)
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
				   && string.Equals(Hair, pOther.Hair);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Hair != null ? Hair.GetHashCode() : 0);
			}
		}
	}
}