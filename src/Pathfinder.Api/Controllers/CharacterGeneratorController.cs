﻿using Pathfinder.Api.Models;
using Pathfinder.Commands;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Utilities;
using System.Collections.Generic;
using System.Web.Http;

namespace Pathfinder.Api.Controllers
{

	public class CharacterGeneratorController : ApiController
	{

		public CharacterGeneratorController()
		{
			var libraryFactory = new LibraryFactory();

			CharacterLibrary = libraryFactory.GetCharacterLibrary();
			SkillLibrary = libraryFactory.GetSkillLibrary();
			RaceLibrary = libraryFactory.GetRaceLibrary();
			ClassLibrary = libraryFactory.GetClassLibrary();
			FeatLibrary = libraryFactory.GetFeatLibrary();
			ItemLibrary = libraryFactory.GetItemLibrary();
		}

		/// <summary>
		/// For testing.
		/// 
		/// TODO - Should probably figure out how to get WebApi to inject the library.
		/// </summary>
		/// <param name="pCharacterLibrary"></param>
		/// <param name="pLibrary"></param>
		/// <param name="pSkillLibrary"></param>
		/// <param name="pClassLibrary"></param>
		/// <param name="pFeatLibrary"></param>
		/// <param name="pItemLibrary"></param>
		internal CharacterGeneratorController(
			ILibrary<ICharacter> pCharacterLibrary,
			ILibrary<IRace> pLibrary,
			ILibrary<ISkill> pSkillLibrary,
			ILibrary<IClass> pClassLibrary,
			ILibrary<IFeat> pFeatLibrary,
			ILibrary<IItem> pItemLibrary)
		{
			CharacterLibrary = pCharacterLibrary;
			RaceLibrary = pLibrary;
			SkillLibrary = pSkillLibrary;
			ClassLibrary = pClassLibrary;
			FeatLibrary = pFeatLibrary;
			ItemLibrary = pItemLibrary;
		}

		internal ILibrary<ICharacter> CharacterLibrary { get; }
		internal ILibrary<IRace> RaceLibrary { get; }
		internal ILibrary<ISkill> SkillLibrary { get; }
		internal ILibrary<IClass> ClassLibrary { get; }
		internal ILibrary<IFeat> FeatLibrary { get; }
		internal ILibrary<IItem> ItemLibrary { get; }

		public ICharacter Create()
		{
			return
				new CharacterFactory(CharacterLibrary, SkillLibrary)
					.Create();
		}

		public ICharacter CreatePreBuilt()
		{
			var full_plate = ItemLibrary["Full Plate"];

			var preBuilt = 
				new CharacterFactory(CharacterLibrary, SkillLibrary)
					.Create()
					.SetRace(RaceLibrary["Gnome"])
					.SetName("Jorkur")
					.SetAlignment(Alignment.ChaoticGood)
					.SetGender(Gender.Male)
					.SetAge(28)

					.SetHeight("3' 6\"")
					.SetWeight("42 lbs.")

					.SetStrength(9)
					.SetDexterity(9)
					.SetConstitution(12)
					.SetIntelligence(11)
					.SetWisdom(11)
					.SetCharisma(16)

					.AddClass(ClassLibrary["Sorcerer"], 1, true, new List<int> { 6 }) // Max + Toughness(Feat)
					//.AddFeat(FeatLibrary["Toughness"])
					.IncrementClass(ClassLibrary["Sorcerer"], 6) // Lvl 2 -> 6 = 4+2
					.IncrementClass(ClassLibrary["Sorcerer"], 8) // Lvl 3 -> 8 = 6+2
					//.AddFeat(FeatLibrary["Spell Focus (Evocation)"])
					.IncrementClass(ClassLibrary["Sorcerer"], 6) // Lvl 4 -> 6 = 4+2

					.AssignSkillPoint(SkillLibrary["Bluff"], 1)
					.AssignSkillPoint(SkillLibrary["Escape Artist"], 1)
					.AssignSkillPoint(SkillLibrary["Perception"], 2)
					.AssignSkillPoint(SkillLibrary["Spellcraft"], 2)
					.AssignSkillPoint(SkillLibrary["Stealth"], 2)
					.AssignSkillPoint(SkillLibrary["Use Magic Device"], 1)

					.SetDamage(1)
					.AddDamage(1)
					.AddDamage(3)
					.AddDamage(6)
					.AddDamage(-7)
					.AddDamage(8)
					//.AddDamage(9)
					//.AddDamage(8)
					//.AddDamage(7)
					//.AddDamage(30) // = 1 + 1 + 3 + 6 - 7 + 8 + 9 + 8 + 7 + 30
					
					.AddFeat(FeatLibrary["Dodge"])
					.AddFeat(FeatLibrary["Eschew Materials"])
					.AddFeat(FeatLibrary["Toughness"])
					.AddFeat(FeatLibrary["Spell Focus"], "Evocation")

					.AddToInventory(full_plate)
					.EquipArmor(full_plate)
					.AddToInventory(ItemLibrary["Quarterstaff"])

					.AddToInventory(ItemLibrary["Crossbow, Light"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])
					.AddToInventory(ItemLibrary["Bolts"])

					.AddToInventory(ItemLibrary["Dagger"])

					.AppendExperience(new EventImpl("Event 1", "Freebie", 2000))
					.AppendExperience(new EventImpl("Event 2", "Defeated Bugbear, killed some spiders", 1200))
					.AppendExperience(new EventImpl("Event 3", "Killed 1 Spider and 2 Gnomes", 290))
					.AppendExperience(new EventImpl("Event 4", "Killed 4 bandits", 667))
					.AppendExperience(new EventImpl("Event 5", "Killed 1 mouthy ozee thing, and 6 humans", 660))
					.AppendExperience(new EventImpl("Event 6", "Killed 2 Humans, and 1 Wight", 350))
					.AppendExperience(new EventImpl("Event 7", "Killed Jaris Phenogian and his Iron Cobra", 400))
					.AppendExperience(new EventImpl("Event 8", "Killed an Automaton, Tarrin and 2 bandits", 440));

			return preBuilt;
		}


		public ICharacter SetAbilityScores([FromBody] AbilityScoreSet pAbilityScores)
		{
			Assert.ArgumentNotNull(pAbilityScores, nameof(pAbilityScores));

			return
				new CharacterFactory(CharacterLibrary, SkillLibrary)
					.Create()
					.SetStrength(pAbilityScores.Strength)
					.SetDexterity(pAbilityScores.Dexterity)
					.SetConstitution(pAbilityScores.Constitution)
					.SetIntelligence(pAbilityScores.Intelligence)
					.SetWisdom(pAbilityScores.Wisdom)
					.SetCharisma(pAbilityScores.Charisma);
		}

		public ICharacter SetRace([FromUri] string pRaceName, [FromBody] ICharacter pCharacter)
		{
			Assert.ArgumentNotNull(pRaceName, nameof(pRaceName));
			Assert.ArgumentNotNull(pCharacter, nameof(pCharacter));
			return pCharacter.SetRace(RaceLibrary[pRaceName]);
		}

		public ICharacter SetClass([FromUri] string pClassName, [FromBody] ICharacter pCharacter)
		{
			Assert.ArgumentNotNull(pClassName, nameof(pClassName));
			Assert.ArgumentNotNull(pCharacter, nameof(pCharacter));
			return pCharacter.AddClass(ClassLibrary[pClassName]);
		}
	}
}
