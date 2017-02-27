﻿using Pathfinder.Commands;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Test.Mocks;
using System.Collections.Generic;
using System.Linq;
using CharacterImpl = Pathfinder.Model.Character;

// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.Serializers.Json.Character
{
	internal static class CharacterJsonSerializerUtils
	{
		public static ICharacter GetTestCharacter()
		{
			const int copperValue = 1;
			const int silverValue = 2;
			const int goldValue = 3;
			const int platinumValue = 4;

			const string name = "Unit McTesterFace";

			var deity = new Deity("Deity");

			var race = new MockRaceLibrary().Values.First();
			var testCharacter =
				CreateNewCharacter()
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
					.AddLanguage(new Language("Mock Language"))
					.AddClass(CreateTestingClass())
					.SetDamage(2)
					.SetStrength(12)
					.SetDexterity(12)
					.SetConstitution(12)
					.SetIntelligence(12)
					.SetWisdom(12)
					.SetCharisma(12)
					.SetPurse(copperValue, silverValue, goldValue, platinumValue)
					.AddFeat(CreateTestingFeat1(), "user-choice")
					.AddFeat(CreateTestingFeat2())
					.AddToInventory(CreateTestingItem());

			testCharacter = AddExperienceCommand.Execute(testCharacter, "Event 1", "Freebie", 2000);


			return testCharacter;
		}

		public static IItem CreateTestingItem()
		{
			return new Item("Testing Item", ItemType.None, "Unit Testing", new Purse(1,1,1,1), 12, "For Unit Testing");
		}

		public static ICharacter CreateNewCharacter()
		{
			return new CharacterImpl(new MockSkillLibrary());
		}

		public static IClass CreateTestingClass()
		{
			const string className = "Mock Class";
			var classLevel = new ClassLevel(1, new List<int> { 1 }, 1, 1, 1, null);

			return new Class(
				className,
				new HashSet<Alignment> { Alignment.Neutral },
				new Die(6),
				0,
				new HashSet<string>(),
				new IClassLevel[] { classLevel },
				new List<string>());
		}

		public static IFeat CreateTestingFeat1()
		{
			return new Feat(
				"Feat 1",
				FeatType.General,
				new List<string>(),
				"Testing Description",
				"Testing Benefit",
				"Testing Special",
				true);
		}

		public static IFeat CreateTestingFeat2(bool pIsSpecialized = false, string pSpecialization = null)
		{
			return
				new Feat(
					"Feat 2",
					FeatType.General,
					new List<string> { "Feat 1" },
					"Testing Description",
					"Testing Benefit",
					"Testing Special",
					pIsSpecialized,
					pSpecialization);
		}
	}
}
