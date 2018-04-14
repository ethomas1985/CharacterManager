using System;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
	internal class RaceRepository : AbstractFilesystemRepository<IRace>
	{
		internal RaceRepository(ISerializer<IRace, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{}
	}
}
