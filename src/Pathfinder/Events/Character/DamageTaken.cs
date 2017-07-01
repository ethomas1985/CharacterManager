using System;

namespace Pathfinder.Events.Character
{
	internal class DamageTaken : AbstractEvent, IEquatable<DamageTaken>
	{
		public DamageTaken(Guid pId, int pVersion, int pDamage)
			: base(pId, pVersion)
		{
			Damage = pDamage;
		}

		public int Damage { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Took {Damage} Damage | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as DamageTaken);
		}

		public bool Equals(DamageTaken pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return Id.Equals(pOther.Id) && Version == pOther.Version && Equals(Damage, pOther.Damage);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Id.GetHashCode();
				hashCode = (hashCode * 397) ^ Version;
				hashCode = (hashCode * 397) ^ Damage.GetHashCode();
				return hashCode;
			}
		}
	}
}