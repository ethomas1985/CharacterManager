using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;

namespace Pathfinder.Test.ObjectMothers
{
	internal static class ItemMother
	{
		public static IItem Create()
		{
			return new Item(
				"Testing Item",
				ItemType.None,
				"Unit Testing",
				new Purse(1, 1, 1, 1),
				pWeight: 12,
				pDescription: new [] {"For Unit Testing" },
				pWeaponComponent: new WeaponComponent(
					Proficiency.None,
					WeaponType.Unarmed,
					Encumbrance.None,
					WeaponSize.Medium,
					DamageType.Bludgeoning,
					new[]
					{
						new Dice(1, new Die(6))
					},
					pCriticalMultiplier: 20,
					pCriticalThreat: 2,
					pRange: 0,
					pSpecials: new List<IWeaponSpecial>()),
				pArmorComponent: new ArmorComponent(
					pArmorBonus: 1,
					pShieldBonus: 1,
					pMaximumDexterityBonus: 1,
					pArmorCheckPenalty: 1,
					pArcaneSpellFailureChance: 0.20m,
					pSpeed: 25));
		}

		public static IItem Arms()
		{
			return new Item(
				"Testing Item",
				ItemType.Arms,
				"Category",
				new Purse(100),
				10,
				new [] { "Description" },
				pArmorComponent: new ArmorComponent(
					pArmorBonus: 1,
					pShieldBonus: 1,
					pMaximumDexterityBonus: 1,
					pArmorCheckPenalty: 1,
					pArcaneSpellFailureChance: 0.20m,
					pSpeed: 25));
		}

		public static IItem Armor()
		{
			return new Item(
				"Testing Item",
				ItemType.Armor,
				"Category",
				new Purse(100),
				10,
				new [] { "Description" },
				pArmorComponent: new ArmorComponent(
					pArmorBonus: 1,
					pShieldBonus: 1,
					pMaximumDexterityBonus: 1,
					pArmorCheckPenalty: 1,
					pArcaneSpellFailureChance: 0.20m,
					pSpeed: 25));
		}

		public static IItem Armor(string pName)
		{
			return new Item(
				pName,
				ItemType.Armor,
				"Category",
				new Purse(100),
				10,
				new [] { "Description" },
				pArmorComponent: new ArmorComponent(
					pArmorBonus: 1,
					pShieldBonus: 1,
					pMaximumDexterityBonus: 1,
					pArmorCheckPenalty: 1,
					pArcaneSpellFailureChance: 0.20m,
					pSpeed: 25));
		}
	}
}
