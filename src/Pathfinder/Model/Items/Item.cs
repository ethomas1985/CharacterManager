using Pathfinder.Enums;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;
using Pathfinder.Utilities;
using System;

namespace Pathfinder.Model.Items
{
	internal class Item : IItem, IEquatable<IItem>
	{
		public Item(string pName,
					ItemType pItemType,
					string pCategory,
					IPurse pCost,
					decimal pWeight,
					string pDescription)
		{
			Name = pName;
			ItemType = pItemType;
			Category = pCategory;
			Cost = pCost;
			Weight = pWeight;
			Description = pDescription;
		}

		public string Name { get; }
		public string Category { get; }
		public IPurse Cost { get; }
		public decimal Weight { get; }
		public string Description { get; }
		public ItemType ItemType { get; set; }

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

			return ComparisonUtilities.Compare(GetType().Name, Name, pOther.Name, nameof(Name))
				&& ComparisonUtilities.Compare(GetType().Name, Category, pOther.Category, nameof(Category))
				&& ComparisonUtilities.Compare(GetType().Name, Cost, pOther.Cost, nameof(Cost))
				&& ComparisonUtilities.Compare(GetType().Name, Weight, pOther.Weight, nameof(Weight))
				&& ComparisonUtilities.Compare(GetType().Name, Description, pOther.Description, nameof(Description))
				&& ComparisonUtilities.Compare(GetType().Name, ItemType, pOther.ItemType, nameof(ItemType));
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
				hashCode = (hashCode * 397) ^ (int) ItemType;
				return hashCode;
			}
		}
	}
}
