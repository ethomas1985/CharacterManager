using System;
using Pathfinder.Interface;

namespace Pathfinder.Serializers.Xml
{
	internal class CharacterXmlSerializer : ISerializer<ICharacter, string>
	{
		public ICharacter Deserialize(string pCharacterString)
		{
			throw new NotImplementedException();
			/*
			var newCharacter = CreateCharacter(pCharacter.RaceEnum);

			newCharacter = 
				newCharacter
					.SetName(pCharacter.Name)
					.SetClasses(pCharacter.Classes)
					.SetAlignment(pCharacter.Alignment)
					.SetGender(newCharacter.Gender)
					.SetAge(newCharacter.Age)
					.SetHeight(pCharacter.Height)
					.SetWeight(pCharacter.Weight);
					.SetStrength(
						pCharacter.Strength?.Base ?? 0, 
						pCharacter.Strength?.Enhanced ?? 0, 
						pCharacter.Strength?.Inherent ?? 0);
					.SetDexterity(
						pCharacter.Dexterity?.Base ?? 0, 
						pCharacter.Dexterity?.Enhanced ?? 0, 
						pCharacter.Dexterity?.Inherent ?? 0);
					.SetConstitution(
						pCharacter.Constitution?.Base ?? 0, 
						pCharacter.Constitution?.Enhanced ?? 0, 
						pCharacter.Constitution?.Inherent ?? 0);
					.SetIntelligence(
						pCharacter.Intelligence?.Base ?? 0,
						pCharacter.Intelligence?.Enhanced ?? 0,
						pCharacter.Intelligence?.Inherent ?? 0);
					.SetWisdom(
						pCharacter.Wisdom?.Base ?? 0,
						pCharacter.Wisdom?.Enhanced ?? 0,
						pCharacter.Wisdom?.Inherent ?? 0);
					.SetCharisma(
						pCharacter.Charisma?.Base ?? 0,
						pCharacter.Charisma?.Enhanced ?? 0,
						pCharacter.Charisma?.Inherent ?? 0);

			return newCharacter;
			*/
		}

		public string Serialize(ICharacter pThis)
		{
			throw new NotImplementedException();
		}
	}
}
