using System;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
	internal class ClassRepository : AbstractFilesystemRepository<IClass>
	{
		internal ClassRepository(
			ISerializer<IClass, string> pSerializer,
			string pLibraryDirectory)
		: base(pSerializer, pLibraryDirectory)
		{ }
	}
}
