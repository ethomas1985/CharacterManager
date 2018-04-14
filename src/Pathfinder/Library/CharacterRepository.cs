using System;
using System.IO;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
	internal class CharacterRepository : AbstractFilesystemRepository<ICharacter>
	{
		public CharacterRepository(
			ISerializer<ICharacter, string> pSerializer,
			string pLibraryDirectory) : base(pSerializer, pLibraryDirectory)
		{
		}

		public override void Save(ICharacter pCharacter, int pVersion)
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
