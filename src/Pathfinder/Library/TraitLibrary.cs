using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class TraitLibrary : AbstractLibrary<ITrait>
	{
		public TraitLibrary(ISerializer<ITrait, string> pSerializer, string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
