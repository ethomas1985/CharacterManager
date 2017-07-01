using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
	internal class ClassRepository : AbstractRepository<IClass>
	{
		internal ClassRepository(
			ISerializer<IClass, string> pSerializer,
			string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
