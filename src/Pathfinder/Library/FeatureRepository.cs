using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
	internal class FeatureRepository : AbstractRepository<IFeature>
	{
		public FeatureRepository(ISerializer<IFeature, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
