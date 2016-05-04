using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class FeatureLibrary : AbstractLibrary<IFeature>
	{
		public FeatureLibrary(ISerializer<IFeature, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
