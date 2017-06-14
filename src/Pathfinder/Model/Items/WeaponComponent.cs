using System;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using System.Collections.Generic;
using Pathfinder.Utilities;

namespace Pathfinder.Model.Items
{
	internal class WeaponComponent : IWeaponComponent, IEquatable<IWeaponComponent>
	{
		public WeaponComponent(
			Proficiency pProficiency,
			WeaponType pWeaponType,
			Encumbrance pEncumbrance,
			WeaponSize pSize,
			DamageType pDamageType,
			IEnumerable<IDice> pBaseWeaponDamage,
			int pCriticalThreat,
			int pCriticalMultiplier,
			int pRange,
			IEnumerable<IWeaponSpecial> pSpecials) {
			Proficiency = pProficiency;
			WeaponType = pWeaponType;
			Encumbrance = pEncumbrance;
			Size = pSize;
			DamageType = pDamageType;
			BaseWeaponDamage = pBaseWeaponDamage;
			CriticalThreat = pCriticalThreat;
			CriticalMultiplier = pCriticalMultiplier;
			Range = pRange;
			Specials = pSpecials;
		}

		public Proficiency Proficiency { get; }
		public WeaponType WeaponType { get; }
		public Encumbrance Encumbrance { get; }
		public WeaponSize Size { get; }
		public DamageType DamageType { get; }
		public IEnumerable<IDice> BaseWeaponDamage { get; }
		public int CriticalThreat { get; }
		public int CriticalMultiplier { get; }
		public int Range { get; }
		public IEnumerable<IWeaponSpecial> Specials { get; }

		public override string ToString()
		{
			return $"{Size} {WeaponType}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IWeaponComponent);
		}

		public bool Equals(IWeaponComponent pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var result = ComparisonUtilities.Compare(GetType().Name, Proficiency,  pOther.Proficiency, nameof(Proficiency));
			result &= ComparisonUtilities.Compare(GetType().Name, WeaponType, pOther.WeaponType, nameof(WeaponType));
			result &= ComparisonUtilities.Compare(GetType().Name, Encumbrance, pOther.Encumbrance, nameof(Encumbrance));
			result &= ComparisonUtilities.Compare(GetType().Name, Size, pOther.Size, nameof(Size));
			result &= ComparisonUtilities.Compare(GetType().Name, DamageType, pOther.DamageType, nameof(DamageType));
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, BaseWeaponDamage, pOther.BaseWeaponDamage, nameof(BaseWeaponDamage));
			result &= ComparisonUtilities.Compare(GetType().Name, CriticalThreat, pOther.CriticalThreat, nameof(CriticalThreat));
			result &= ComparisonUtilities.Compare(GetType().Name, CriticalMultiplier, pOther.CriticalMultiplier, nameof(CriticalMultiplier));
			result &= ComparisonUtilities.Compare(GetType().Name, Range, pOther.Range, nameof(Range));
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, Specials, pOther.Specials, nameof(Specials));

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (int) Proficiency;
				hashCode = (hashCode * 397) ^ (int) WeaponType;
				hashCode = (hashCode * 397) ^ (int) Encumbrance;
				hashCode = (hashCode * 397) ^ (int) Size;
				hashCode = (hashCode * 397) ^ (int) DamageType;
				hashCode = (hashCode * 397) ^ (BaseWeaponDamage != null ? BaseWeaponDamage.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ CriticalThreat;
				hashCode = (hashCode * 397) ^ CriticalMultiplier;
				hashCode = (hashCode * 397) ^ Range;
				hashCode = (hashCode * 397) ^ (Specials != null ? Specials.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
