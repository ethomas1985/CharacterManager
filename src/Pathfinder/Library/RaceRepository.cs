using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
	internal class RaceRepository : AbstractRepository<IRace>
	{
		internal RaceRepository(ISerializer<IRace, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{}
	}
}
