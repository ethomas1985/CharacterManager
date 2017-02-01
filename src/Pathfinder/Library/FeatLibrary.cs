using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class FeatLibrary : AbstractLibrary<IFeat>
	{
		public FeatLibrary(ISerializer<IFeat, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
