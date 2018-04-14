using System;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
	internal class ItemRepository : AbstractFilesystemRepository<IItem>
	{
		internal ItemRepository(
			ISerializer<IItem, string> pSerializer,
			string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
