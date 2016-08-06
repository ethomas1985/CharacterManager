using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;

namespace Pathfinder.Model.Items
{
	internal class Weapon : Item, IWeapon
	{
		public Weapon(
			string pName,
			string pCategory,
			IPurse pCost,
			string pWeight,
			string pDescription,
			Proficiency pProficiency,
			WeaponType pWeaponType,
			Encumbrance pEncumbrance,
			WeaponSize pSize,
			DamageType pDamageType,
			IEnumerable<IWeaponDamage> pBaseWeaponDamage,
			int pCriticalThreat,
			int pCriticalMultiplier,
			int pRange,
			IEnumerable<IWeaponSpecial> pSpecials)
			: base(pName, ItemType.Weapon, pCategory, pCost, pWeight, pDescription)
		{
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
		public IEnumerable<IWeaponDamage> BaseWeaponDamage { get; }
		public int CriticalThreat { get; }
		public int CriticalMultiplier { get; }
		public int Range { get; }
		public IEnumerable<IWeaponSpecial> Specials { get; }
	}
}
