using Pathfinder.Interface;
using System.Collections.Generic;

namespace Pathfinder.Model
{
	internal class Inventory : List<IItem>, IInventory
	{
	}
}