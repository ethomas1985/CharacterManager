using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Pathfinder.Interface;
using Pathfinder.Library;
using Pathfinder.Model;
using Pathfinder.Properties;
using Pathfinder.Serializers;

namespace Pathfinder
{
	public class CharacterFactory
	{
		private readonly SkillLibrary _skillLibrary 
			= new SkillLibrary(new SkillXmlSerializer(), Settings.Default.SkillLibrary);

		public CharacterFactory(string pCharacterLibraryDirectory)
		{
			CharacterLibraryDirectory = pCharacterLibraryDirectory;
			CharacterLibrary = new CharacterLibrary(XmlSerializer, CharacterLibraryDirectory);
		}

		private CharacterXmlSerializer XmlSerializer { get; } = new CharacterXmlSerializer();
		private string CharacterLibraryDirectory { get; }
		private CharacterLibrary CharacterLibrary { get; }

		public ICharacter Create(IRace pRace)
		{
			return new Character(pRace, _skillLibrary);
		}

		public IEnumerable<ICharacter> Get()
		{
			return CharacterLibrary.Library.Values.ToImmutableList();
		}

		public ICharacter Get(string pName)
		{
			ICharacter value;
			if (CharacterLibrary.Library.TryGetValue(pName, out value))
			{
				return value;
			}
			return null;
		}

		public void Save(ICharacter pCharacter)
		{
			if (pCharacter == null)
			{
				return;
			}

			var serialized = XmlSerializer.Serialize(pCharacter);

			var fileName = Path.ChangeExtension(pCharacter.Name, "xml");
			var filePath = Path.Combine(CharacterLibraryDirectory, fileName);
			if (File.Exists(filePath))
			{
				var newFileName = Path.ChangeExtension(filePath, $"{DateTime.Now}.xml");
				File.Copy(filePath, newFileName);
			}

			File.WriteAllText(filePath, serialized);
		}

		public void Delete(ICharacter pCharacter)
		{
			if (pCharacter == null)
			{
				return;
			}

			var fileName = Path.ChangeExtension(pCharacter.Name, "xml");
			var filePath = Path.Combine(CharacterLibraryDirectory, fileName);
			if (!File.Exists(filePath))
			{
				return;
			}

			var newFileName = Path.ChangeExtension(filePath, $"{DateTime.Now}.xml");
			File.Move(filePath, newFileName);
		}
	}
}
