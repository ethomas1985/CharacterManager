using System.Collections.Generic;
using System.Collections.Immutable;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder
{
	public class CharacterFactory
	{

		public CharacterFactory(
			ILibrary<ICharacter> pCharacterLibrary,
			ILibrary<ISkill> pSkillLibrary)
		{
			Library = pCharacterLibrary;
			SkillLibrary = pSkillLibrary;
		}

		private ILibrary<ICharacter> Library { get; }
		private ILibrary<ISkill> SkillLibrary { get; }

		public ICharacter Create()
		{
			return new Character(SkillLibrary);
		}

		public IEnumerable<ICharacter> Get()
		{
			return Library.Values.ToImmutableList();
		}

		public ICharacter Get(string pName)
		{
			ICharacter value;
			if (pName != null && Library.TryGetValue(pName, out value))
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

			Library.Store(pCharacter);
		}

		//public void Delete(ICharacter pCharacter)
		//{
		//	if (pCharacter == null)
		//	{
		//		return;
		//	}

		//	var fileName = Path.ChangeExtension(pCharacter.Name, "xml");
		//	var filePath = Path.Combine(CharacterLibraryDirectory, fileName);
		//	if (!File.Exists(filePath))
		//	{
		//		return;
		//	}

		//	var newFileName = Path.ChangeExtension(filePath, $"{DateTime.Now}.xml");
		//	File.Move(filePath, newFileName);
		//}
	}
}
