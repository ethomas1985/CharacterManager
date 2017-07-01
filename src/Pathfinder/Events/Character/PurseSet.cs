using System;
using Pathfinder.Model.Currency;

namespace Pathfinder.Events.Character
{
	internal class PurseSet : AbstractEvent, IEquatable<PurseSet>
	{
		public PurseSet(Guid pId, int pVersion, int pCopper, int pSilver, int pGold, int pPlatinum)
			: base(pId, pVersion)
		{
			Copper = new Copper(pCopper);
			Silver = new Silver(pSilver);
			Gold = new Gold(pGold);
			Platinum = new Platinum(pPlatinum);
		}

		public Copper Copper { get; }
		public Silver Silver { get; }
		public Gold Gold { get; }
		public Platinum Platinum { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Purse value changed [{Copper}, {Silver}, {Gold}, {Platinum}] | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as PurseSet);
		}

		public bool Equals(PurseSet pOther)
		{
			return base.Equals(pOther)
				   && Copper.Equals(pOther.Copper)
				   && Silver.Equals(pOther.Silver)
				   && Gold.Equals(pOther.Gold)
				   && Platinum.Equals(pOther.Platinum);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Id.GetHashCode();
				hashCode = (hashCode * 397) ^ Version;
				hashCode = (hashCode * 397) ^ Copper.GetHashCode();
				hashCode = (hashCode * 397) ^ Silver.GetHashCode();
				hashCode = (hashCode * 397) ^ Gold.GetHashCode();
				hashCode = (hashCode * 397) ^ Platinum.GetHashCode();
				return hashCode;
			}
		}
	}
}