using Pathfinder.Api.Models;
using Pathfinder.Enums;
using System.Collections.Generic;
using System.Web.Http;
using Pathfinder.Factories;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Api.Controllers
{

	public class CharacterGeneratorController : ApiController
	{

		public CharacterGeneratorController()
		{
			var libraryFactory = new RepositoryFactory();

			CharacterRepository = libraryFactory.GetCharacterRespository();
			SkillRepository = libraryFactory.GetSkillRespository();
			RaceRepository = libraryFactory.GetRaceRespository();
			ClassRepository = libraryFactory.GetClassRespository();
			FeatRepository = libraryFactory.GetFeatRespository();
			ItemRepository = libraryFactory.GetItemRespository();
		}

		/// <summary>
		/// For testing.
		/// 
		/// TODO - Should probably figure out how to get WebApi to inject the library.
		/// </summary>
		/// <param name="pCharacterRepository"></param>
		/// <param name="pRepository"></param>
		/// <param name="pSkillRepository"></param>
		/// <param name="pClassRepository"></param>
		/// <param name="pFeatRepository"></param>
		/// <param name="pItemRepository"></param>
		internal CharacterGeneratorController(
			IRepository<ICharacter> pCharacterRepository,
			IRepository<IRace> pRepository,
			IRepository<ISkill> pSkillRepository,
			IRepository<IClass> pClassRepository,
			IRepository<IFeat> pFeatRepository,
			IRepository<IItem> pItemRepository)
		{
			CharacterRepository = pCharacterRepository;
			RaceRepository = pRepository;
			SkillRepository = pSkillRepository;
			ClassRepository = pClassRepository;
			FeatRepository = pFeatRepository;
			ItemRepository = pItemRepository;
		}

		internal IRepository<ICharacter> CharacterRepository { get; }
		internal IRepository<IRace> RaceRepository { get; }
		internal IRepository<ISkill> SkillRepository { get; }
		internal IRepository<IClass> ClassRepository { get; }
		internal IRepository<IFeat> FeatRepository { get; }
		internal IRepository<IItem> ItemRepository { get; }
		

		public ICharacter CreatePreBuilt()
		{
			var fullPlate = ItemRepository["Full Plate"];

			var preBuilt = 
				CharacterFactory
					.Create(SkillRepository)
					.SetRace(RaceRepository["Gnome"])
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

					.AddClass(ClassRepository["Sorcerer"], 1, true, new List<int> { 6 }) // Max + Toughness(Feat)
					//.AddFeat(FeatLibrary["Toughness"])
					.IncrementClass(ClassRepository["Sorcerer"], 6) // Lvl 2 -> 6 = 4+2
					.IncrementClass(ClassRepository["Sorcerer"], 8) // Lvl 3 -> 8 = 6+2
					//.AddFeat(FeatLibrary["Spell Focus (Evocation)"])
					.IncrementClass(ClassRepository["Sorcerer"], 6) // Lvl 4 -> 6 = 4+2

					.AssignSkillPoint(SkillRepository["Bluff"], 1)
					.AssignSkillPoint(SkillRepository["Escape Artist"], 1)
					.AssignSkillPoint(SkillRepository["Perception"], 2)
					.AssignSkillPoint(SkillRepository["Spellcraft"], 2)
					.AssignSkillPoint(SkillRepository["Stealth"], 2)
					.AssignSkillPoint(SkillRepository["Use Magic Device"], 1)

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
					
					.AddFeat(FeatRepository["Dodge"])
					.AddFeat(FeatRepository["Eschew Materials"])
					.AddFeat(FeatRepository["Toughness"])
					.AddFeat(FeatRepository["Spell Focus"], "Evocation")

					.AddToInventory(fullPlate)
					.EquipArmor(fullPlate)
					.AddToInventory(ItemRepository["Quarterstaff"])

					.AddToInventory(ItemRepository["Crossbow, Light"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])
					.AddToInventory(ItemRepository["Bolts"])

					.AddToInventory(ItemRepository["Dagger"])

					.AppendExperience(new ExperienceEventImpl("Event 1", "Freebie", 2000))
					.AppendExperience(new ExperienceEventImpl("Event 2", "Defeated Bugbear, killed some spiders", 1200))
					.AppendExperience(new ExperienceEventImpl("Event 3", "Killed 1 Spider and 2 Gnomes", 290))
					.AppendExperience(new ExperienceEventImpl("Event 4", "Killed 4 bandits", 667))
					.AppendExperience(new ExperienceEventImpl("Event 5", "Killed 1 mouthy ozee thing, and 6 humans", 660))
					.AppendExperience(new ExperienceEventImpl("Event 6", "Killed 2 Humans, and 1 Wight", 350))
					.AppendExperience(new ExperienceEventImpl("Event 7", "Killed Jaris Phenogian and his Iron Cobra", 400))
					.AppendExperience(new ExperienceEventImpl("Event 8", "Killed an Automaton, Tarrin and 2 bandits", 440));

			return preBuilt;
		}
	}
}
