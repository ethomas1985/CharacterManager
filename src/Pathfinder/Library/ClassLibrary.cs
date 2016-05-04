using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class ClassLibrary : AbstractLibrary<IClass>
	{
		internal ClassLibrary(
			ISerializer<IClass, string> pSerializer,
			string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
