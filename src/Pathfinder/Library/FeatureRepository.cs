using System;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
	internal class FeatureRepository : AbstractFilesystemRepository<IFeature>
	{
		public FeatureRepository(ISerializer<IFeature, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
