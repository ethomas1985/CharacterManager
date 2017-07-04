using System;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Test.Serializers.Json;
using CharacterImpl = Pathfinder.Model.Character;

// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.ObjectMothers
{
	internal static class CharacterMother
	{
		public static ICharacter UnitMcTesterFace()
		{
			const int copperValue = 1;
			const int silverValue = 2;
			const int goldValue = 3;
			const int platinumValue = 4;

			var skillRepository = SetupTestFixtureForJsonSerializers.SkillRepository;

			const string name = "Unit McTesterFace";

			var deity = DeityMother.Skepticus();
			var race = RaceMother.Create();
			var skill = skillRepository.First();

			var testingItem = ItemMother.Create();
			var testCharacter =
				new CharacterImpl(skillRepository, new Guid("DEADBEEF-0000-0001-0010-000000000011"))
					.SetName(name)
					.SetAge(10)
					.SetHomeland("Homeland")
					.SetDeity(deity)
					.SetGender(Gender.Male)
					.SetEyes("Blue")
					.SetHair("Blue")
					.SetHeight("9' 6\"")
					.SetWeight("180 lbs.")
					.SetAlignment(Alignment.LawfulGood)
					.SetRace(race)
					.AddLanguage(LanguageMother.MockLanguage())
					.AddClass(ClassMother.Level1Neutral())
					.SetDamage(2)
					.SetStrength(12)
					.SetDexterity(12)
					.SetConstitution(12)
					.SetIntelligence(12)
					.SetWisdom(12)
					.SetCharisma(12)
					.SetPurse(copperValue, silverValue, goldValue, platinumValue)
					.AddFeat(FeatMother.CreateTestingFeat1(), "user-choice")
					.AddFeat(FeatMother.CreateTestingFeat2())
					.AssignSkillPoint(skill, 1)
					.AddToInventory(testingItem)
					.EquipArmor(testingItem)
					.AppendExperience(
						new ExperienceEvent("Event 1", "Freebie", 2000));

			return testCharacter;
		}
	}
}
