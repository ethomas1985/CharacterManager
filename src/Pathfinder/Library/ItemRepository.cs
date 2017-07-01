using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Library
{
	internal class ItemRepository : AbstractRepository<IItem>
	{
		internal ItemRepository(
			ISerializer<IItem, string> pSerializer,
			string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
