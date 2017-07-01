using System;

namespace Pathfinder.Events.Character
{
	internal class DamageHealed : AbstractEvent, IEquatable<DamageHealed>
	{
		public DamageHealed(Guid pId, int pVersion, int pHealed)
			: base(pId, pVersion)
		{
			Healed = pHealed;
		}

		public int Healed { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Healed)} {Healed} Damage | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as DamageHealed);
		}

		public bool Equals(DamageHealed pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return Id.Equals(pOther.Id) && Version == pOther.Version && Equals(Healed, pOther.Healed);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Id.GetHashCode();
				hashCode = (hashCode * 397) ^ Version;
				hashCode = (hashCode * 397) ^ Healed.GetHashCode();
				return hashCode;
			}
		}
	}
}