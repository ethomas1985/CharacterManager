using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
	internal class TraitRepository : AbstractRepository<ITrait>
	{
		public TraitRepository(ISerializer<ITrait, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
