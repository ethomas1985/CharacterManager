using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;

namespace Pathfinder.Model
{
	internal class Item : IItem
	{
		public Item(string pName,
					ItemType pItemType,
					string pCategory,
					IPurse pCost,
					string pWeight,
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
		public string Weight { get; }
		public string Description { get; }
		public ItemType ItemType { get; set; }
	}
}
