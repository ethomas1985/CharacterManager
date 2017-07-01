using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Events.Character
{
	internal class DeitySet : AbstractEvent, IEquatable<DeitySet>
	{
		public DeitySet(Guid pId, int pVersion, IDeity pDeity)
			: base(pId, pVersion)
		{
			Deity = pDeity;
		}

		public IDeity Deity { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Deity set to {Deity} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as DeitySet);
		}

		public bool Equals(DeitySet pOther)
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

			return base.Equals(pOther) && Equals(Deity, pOther.Deity);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Deity != null ? Deity.GetHashCode() : 0);
			}
		}
	}
}
