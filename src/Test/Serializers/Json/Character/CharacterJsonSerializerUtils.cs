using System.Linq;
using Pathfinder.Commands;
using Pathfinder.Interface;
using Pathfinder.Test.Mocks;
using CharacterImpl = Pathfinder.Model.Character;

// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.Serializers.Json.Character
{
	public static class CharacterJsonSerializerUtils
	{
		public static ICharacter getTestCharacter()
		{
			var character = CreateNewCharacter();
			var withStrength = character.SetStrength(12);
			var withDexterity = withStrength.SetDexterity(12);
			var withConstitution = withDexterity.SetConstitution(12);
			var withIntelligence = withConstitution.SetIntelligence(12);
			var withWisdom = withIntelligence.SetWisdom(12);
			var withCharisma = withWisdom.SetCharisma(12);
			var withAge = withCharisma.SetAge(10);
			var withRace = withAge.SetRace(new MockRaceLibrary().Values.First());
			var withClass = withRace.AddClass(new MockClassLibrary().Values.First());

			var withExperience = AddExperienceCommand.Execute(withClass, "Event 1", "Freebie", 2000);

			return withExperience;
		}

		public static ICharacter CreateNewCharacter()
		{
			return new CharacterImpl(new MockSkillLibrary());
		}
	}
}
