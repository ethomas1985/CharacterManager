using System;
using Pathfinder.Enums;

namespace Pathfinder.Events.Character
{
	internal class GenderSet : AbstractEvent, IEquatable<GenderSet>
	{
		public GenderSet(Guid pId, int pVersion, Gender pGender)
			: base(pId, pVersion)
		{
			Gender = pGender;
		}

		public Gender Gender { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Gender set to {Gender} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as GenderSet);
		}

		public bool Equals(GenderSet pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			if (pOther.GetType() != GetType())
			{
				return false;
			}

			return base.Equals(pOther)
				&& Gender == pOther.Gender;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (int)Gender;
			}
		}
	}
}
