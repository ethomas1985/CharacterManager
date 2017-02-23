using Pathfinder.Enums;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;

namespace Pathfinder.Model.Items
{
	internal class Item : IItem
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
	}
}
