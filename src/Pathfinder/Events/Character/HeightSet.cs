using System;

namespace Pathfinder.Events.Character
{
	internal class HeightSet : AbstractEvent, IEquatable<HeightSet>
	{
		public HeightSet(Guid pId, int pVersion, string pHeight)
			: base(pId, pVersion)
		{
			Height = pHeight;
		}

		public string Height { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Height)} set to {Height} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as HeightSet);
		}

		public bool Equals(HeightSet pOther)
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
				   && string.Equals(Height, pOther.Height);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Height != null ? Height.GetHashCode() : 0);
			}
		}
	}
}