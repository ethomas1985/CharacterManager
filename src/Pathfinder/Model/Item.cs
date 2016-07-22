using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Item : IItem
	{
		public Item(string pName, string pCategory, IMoney pCost, string pWeight, string pDescription)
		{
			Name = pName;
			Category = pCategory;
			Cost = pCost;
			Weight = pWeight;
			Description = pDescription;
		}

		public string Name { get; }
		public string Category { get; }
		public IMoney Cost { get; }
		public string Weight { get; }
		public string Description { get; }
	}
}
