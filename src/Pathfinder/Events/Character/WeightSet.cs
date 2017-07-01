using System;

namespace Pathfinder.Events.Character
{
	internal class WeightSet : AbstractEvent, IEquatable<WeightSet>
	{
		public WeightSet(Guid pId, int pVersion, string pWeight)
			: base(pId, pVersion)
		{
			Weight = pWeight;
		}

		public string Weight { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Weight)} set to {Weight} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as WeightSet);
		}

		public bool Equals(WeightSet pOther)
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
				   && string.Equals(Weight, pOther.Weight);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Weight != null ? Weight.GetHashCode() : 0);
			}
		}
	}
}