using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class CharacterLibrary : AbstractLibrary<ICharacter>
	{
		public CharacterLibrary(
			ISerializer<ICharacter, string> pSerializer,
			string pLibraryDirectory) : base(pSerializer, pLibraryDirectory)
		{
		}
	}
}
