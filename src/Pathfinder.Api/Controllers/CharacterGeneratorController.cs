using Pathfinder.Api.Models;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;
using System.Collections.Generic;
using System.Web.Http;

namespace Pathfinder.Api.Controllers
{

	public class CharacterGeneratorController : ApiController
	{

		public CharacterGeneratorController()
		{
			CharacterLibrary = new LibraryFactory().GetCharacterLibrary();
			SkillLibrary = new LibraryFactory().GetSkillLibrary();
			RaceLibrary = new LibraryFactory().GetRaceLibrary();
			ClassLibrary = new LibraryFactory().GetClassLibrary();
			//FeatLibrary = new LibraryFactory().GetFeatLibrary();
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
		internal CharacterGeneratorController(
			ILibrary<ICharacter> pCharacterLibrary,
			ILibrary<IRace> pLibrary,
			ILibrary<ISkill> pSkillLibrary,
			ILibrary<IClass> pClassLibrary)
		{
			CharacterLibrary = pCharacterLibrary;
			RaceLibrary = pLibrary;
			SkillLibrary = pSkillLibrary;
			ClassLibrary = pClassLibrary;
		}

		internal ILibrary<ICharacter> CharacterLibrary { get; }
		internal ILibrary<IRace> RaceLibrary { get; }
		internal ILibrary<ISkill> SkillLibrary { get; }
		internal ILibrary<IClass> ClassLibrary { get; }
		internal ILibrary<IFeat> FeatLibrary { get; set; }

		public ICharacter Create()
		{
			return
				new CharacterFactory(CharacterLibrary, SkillLibrary)
					.Create();
		}

		public ICharacter CreatePreBuilt()
		{
			return
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

					//.AssignSkillPoint(SkillLibrary["Bluff"], 1)
					//.AssignSkillPoint(SkillLibrary["Escape Artist"], 1)
					//.AssignSkillPoint(SkillLibrary["Perception"], 2)
					//.AssignSkillPoint(SkillLibrary["Spellcraft"], 2)
					//.AssignSkillPoint(SkillLibrary["Stealth"], 2)
					//.AssignSkillPoint(SkillLibrary["Use Magic Device"], 1)

					.SetDamage(1)
					.AddDamage(1)
					.AddDamage(3)
					.AddDamage(6)
					.AddDamage(-7)
					.AddDamage(8)
					.AddDamage(9)
					.AddDamage(8)
					.AddDamage(7)
					.AddDamage(30) // = 1 + 1 + 3 + 6 - 7 + 8 + 9 + 8 + 7 + 30
					;
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
