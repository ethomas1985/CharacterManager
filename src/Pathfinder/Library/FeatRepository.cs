using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
	internal class FeatRepository : AbstractRepository<IFeat>
	{
		public FeatRepository(ISerializer<IFeat, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
