using System;
using Pathfinder.Enums;
using Pathfinder.Utilities;

namespace Pathfinder.Events.Character
{
	internal abstract class AbstractAbilityScoreSet : AbstractEvent, IEquatable<AbstractAbilityScoreSet>
	{
		protected AbstractAbilityScoreSet(Guid pId, int pVersion, int pBase, int pEnhanced, int pInherent, AbilityType pAbilityType)
			: base(pId, pVersion)
		{
			AbilityType = pAbilityType;
			Base = pBase;
			Enhanced = pEnhanced;
			Inherent = pInherent;
		}

		public AbilityType AbilityType { get; }
		public int Base { get; }
		public int Enhanced { get; }
		public int Inherent { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {AbilityType} set to [{nameof(Base)}: {Base}, {nameof(Enhanced)}: {Enhanced}, {nameof(Inherent)}: {Inherent}] | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as AbstractAbilityScoreSet);
		}

		public bool Equals(AbstractAbilityScoreSet pOther)
		{
			return base.Equals(pOther)
				&& ComparisonUtilities.Compare(GetType().Name, AbilityType, pOther.AbilityType, nameof(AbilityType))
				&& ComparisonUtilities.Compare(GetType().Name, Base, pOther.Base, nameof(Base))
				&& ComparisonUtilities.Compare(GetType().Name, Enhanced, pOther.Enhanced, nameof(Enhanced))
				&& ComparisonUtilities.Compare(GetType().Name, Inherent, pOther.Inherent, nameof(Inherent));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397)
					   ^ AbilityType.GetHashCode()
					   ^ Base.GetHashCode()
					   ^ Enhanced.GetHashCode()
					   ^ Inherent.GetHashCode();
			}
		}
	}
}