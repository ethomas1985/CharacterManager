using Pathfinder.Enums;
using Pathfinder.Utilities;
using System;
using Pathfinder.Interface.Model.Currency;
using Pathfinder.Interface.Model.Item;
using WeaponComponentImpl = Pathfinder.Model.Items.WeaponComponent;
using ArmorComponentImpl = Pathfinder.Model.Items.ArmorComponent;

namespace Pathfinder.Model.Items
{
	internal class Item : IItem, IEquatable<IItem>
	{
		public Item(
			string pName,
			ItemType pItemType,
			string pCategory,
			IPurse pCost,
			decimal pWeight,
			string pDescription,
			IWeaponComponent pWeaponComponent = null,
			IArmorComponent pArmorComponent = null)
		{
			Name = pName;
			ItemType = pItemType;
			Category = pCategory;
			Cost = pCost;
			Weight = pWeight;
			Description = pDescription;

			WeaponComponent = pWeaponComponent;
			ArmorComponent = pArmorComponent;
		}

		public string Name { get; }
		public string Category { get; }
		public IPurse Cost { get; }
		public decimal Weight { get; }
		public string Description { get; }
		public ItemType ItemType { get; }
		public IWeaponComponent WeaponComponent { get; }
		public IArmorComponent ArmorComponent { get; }

		public override string ToString()
		{
			return ItemType != ItemType.None ? $"{ItemType}: {Name}" : $"{Name}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IItem);
		}

		public bool Equals(IItem pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var result = ComparisonUtilities.Compare(GetType().Name, Name, pOther.Name, nameof(Name));
			result &= ComparisonUtilities.Compare(GetType().Name, Category, pOther.Category, nameof(Category));
			result &= ComparisonUtilities.Compare(GetType().Name, Cost, pOther.Cost, nameof(Cost));
			result &= ComparisonUtilities.Compare(GetType().Name, Weight, pOther.Weight, nameof(Weight));
			result &= ComparisonUtilities.Compare(GetType().Name, Description, pOther.Description, nameof(Description));
			result &= ComparisonUtilities.Compare(GetType().Name, ItemType, pOther.ItemType, nameof(ItemType));
			result &= ComparisonUtilities.Compare(GetType().Name, WeaponComponent, pOther.WeaponComponent, nameof(WeaponComponent));
			result &= ComparisonUtilities.Compare(GetType().Name, ArmorComponent, pOther.ArmorComponent, nameof(ArmorComponent));

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Name?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (Category?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Cost?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ Weight.GetHashCode();
				hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (int)ItemType;
				return hashCode;
			}
		}

        public static Item Copy(IItem pOther)
        {
            return new Item(pOther.Name,
                            pOther.ItemType,
                            pOther.Category,
                            pOther.Cost,
                            pOther.Weight,
                            pOther.Description,
                            WeaponComponentImpl.Copy(pOther.WeaponComponent),
                            ArmorComponentImpl.Copy(pOther.ArmorComponent));
        }
	}
}
