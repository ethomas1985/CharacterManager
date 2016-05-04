using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class RaceLibrary : AbstractLibrary<IRace>
	{
		internal RaceLibrary(ISerializer<IRace, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{}
	}
}
