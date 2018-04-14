using System;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
	internal class FeatRepository : AbstractFilesystemRepository<IFeat>
	{
		public FeatRepository(ISerializer<IFeat, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
