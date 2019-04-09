using System;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Utilities;

namespace Pathfinder.Model.Items
{
	internal class ArmorComponent : IArmorComponent, IEquatable<IArmorComponent>
	{
		public ArmorComponent(
			int pArmorBonus,
			int pShieldBonus,
			int pMaximumDexterityBonus,
			int pArmorCheckPenalty,
			decimal pArcaneSpellFailureChance,
			int pSpeed
		) {
			ArmorBonus = pArmorBonus;
			ShieldBonus = pShieldBonus;
			MaximumDexterityBonus = pMaximumDexterityBonus;
			ArmorCheckPenalty = pArmorCheckPenalty;
			ArcaneSpellFailureChance = pArcaneSpellFailureChance;
			SpeedModifier = pSpeed;
		}

		public int ArmorBonus { get; }
		public int ShieldBonus { get; }
		public int MaximumDexterityBonus { get; }
		public int ArmorCheckPenalty { get; }
		public decimal ArcaneSpellFailureChance { get; }
		public int SpeedModifier { get; }

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IArmorComponent);
		}

		public bool Equals(IArmorComponent pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var result = ComparisonUtilities.Compare(GetType().Name, ArmorBonus, pOther.ArmorBonus, nameof(ArmorBonus));
			result &= ComparisonUtilities.Compare(GetType().Name, ShieldBonus, pOther.ShieldBonus, nameof(ShieldBonus));
			result &= ComparisonUtilities.Compare(GetType().Name, MaximumDexterityBonus, pOther.MaximumDexterityBonus, nameof(MaximumDexterityBonus));
			result &= ComparisonUtilities.Compare(GetType().Name, ArmorCheckPenalty, pOther.ArmorCheckPenalty, nameof(ArmorCheckPenalty));
			result &= ComparisonUtilities.Compare(GetType().Name, ArcaneSpellFailureChance, pOther.ArcaneSpellFailureChance, nameof(ArcaneSpellFailureChance));
			result &= ComparisonUtilities.Compare(GetType().Name, SpeedModifier, pOther.SpeedModifier, nameof(SpeedModifier));

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = ArmorBonus;
				hashCode = (hashCode * 397) ^ ShieldBonus;
				hashCode = (hashCode * 397) ^ MaximumDexterityBonus;
				hashCode = (hashCode * 397) ^ ArmorCheckPenalty;
				hashCode = (hashCode * 397) ^ ArcaneSpellFailureChance.GetHashCode();
				hashCode = (hashCode * 397) ^ SpeedModifier;
				return hashCode;
			}
		}

        public static ArmorComponent Copy(IArmorComponent pOther)
        {
            return new ArmorComponent(pOther.ArmorBonus,
                                      pOther.ShieldBonus,
                                      pOther.MaximumDexterityBonus,
                                      pOther.ArmorCheckPenalty,
                                      pOther.ArcaneSpellFailureChance,
                                      pOther.SpeedModifier);
        }
	}
}
