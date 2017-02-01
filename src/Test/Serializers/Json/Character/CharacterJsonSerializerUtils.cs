using System.Collections.Generic;
using System.Linq;
using Pathfinder.Commands;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
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

			var withFeat = withExperience.AddFeat(CreateTestingFeat());

			return withFeat;
		}

		public static ICharacter CreateNewCharacter()
		{
			return new CharacterImpl(new MockSkillLibrary());
		}

		public static IFeat CreateTestingFeat()
		{
			return new Feat(
				"Feat 2",
				FeatType.General,
				new List<string> { "Feat 1" },
				"Testing Description",
				"Testing Benefit",
				"Testing Special");
		}
	}
}
