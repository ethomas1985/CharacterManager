using Pathfinder.Interface;
using Pathfinder.Interface.Item;

namespace Pathfinder.Library
{
	internal class ItemLibrary : AbstractLibrary<IItem>
	{
		internal ItemLibrary(
			ISerializer<IItem, string> pSerializer,
			string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
