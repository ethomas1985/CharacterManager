using System;
using System.IO;
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

		public override void Store(ICharacter pCharacter)
		{
			var serialized = Serializer.Serialize(pCharacter);

			var fileName = Path.ChangeExtension(pCharacter.Name, "xml");
			var filePath = Path.Combine(LibraryDirectory, fileName);
			if (File.Exists(filePath))
			{
				var newFileName = Path.ChangeExtension(filePath, $"{DateTime.Now}.xml");
				File.Copy(filePath, newFileName);
			}

			File.WriteAllText(filePath, serialized);
		}
	}
}
