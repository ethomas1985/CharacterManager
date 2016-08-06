using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;

namespace Pathfinder.Model.Items
{
	internal class Armor : Item, IArmor
	{
		public Armor(
			string pName,
			ItemType pItemType,
			string pCategory,
			IPurse pCost,
			string pWeight,
			string pDescription,
			int pArmorBonus,
			int pShieldBonus,
			int pMaximumDexterityBonus,
			int pArmorCheckPenalty,
			decimal pArcaneSpellFailureChance,
			int pSpeed
		)
			: base(pName, pItemType, pCategory, pCost, pWeight, pDescription)
		{
			ArmorBonus = pArmorBonus;
			ShieldBonus = pShieldBonus;
			MaximumDexterityBonus = pMaximumDexterityBonus;
			ArmorCheckPenalty = pArmorCheckPenalty;
			ArcaneSpellFailureChance = pArcaneSpellFailureChance;
			Speed = pSpeed;
		}

		public int ArmorBonus { get; }
		public int ShieldBonus { get; }
		public int MaximumDexterityBonus { get; }
		public int ArmorCheckPenalty { get; }
		public decimal ArcaneSpellFailureChance { get; }
		public int Speed { get; }
	}
}
