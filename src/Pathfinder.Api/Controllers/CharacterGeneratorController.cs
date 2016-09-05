using System.Web.Http;
using Pathfinder.Api.Models;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;

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

		public ICharacter Create()
		{
			return
				new CharacterFactory(CharacterLibrary, SkillLibrary)
					.Create();
		}

		public ICharacter CreateTyrida()
		{
			return
				new CharacterFactory(CharacterLibrary, SkillLibrary)
					.Create()
					.SetName("Tyrida")
					.SetAlignment(Alignment.NeutralGood)
					.SetGender(Gender.Male)
					.SetAge(30)
					.SetHeight("6' 8\"")
					.SetWeight("190")
					.SetStrength(13)
					.SetDexterity(10)
					.SetConstitution(14)
					.SetIntelligence(15)
					.SetWisdom(16)
					.SetCharisma(19)
					.SetRace(RaceLibrary["Half-Orc"])
					.AddClass(ClassLibrary["Sorcerer"])
					.IncrementClass(ClassLibrary["Sorcerer"])
					//.IncrementClass(ClassLibrary["Sorcerer"])
					//.AssignSkillPoint(SkillLibrary["Intimidate"], 1)
					//.AssignSkillPoint(SkillLibrary["Knowledge (Arcana)"], 1)
					//.AssignSkillPoint(SkillLibrary["Knowledge (Dungeoneering)"], 1)
					//.AssignSkillPoint(SkillLibrary["Perception"], 1)
					//.AssignSkillPoint(SkillLibrary["Sense Motive"], 1)
					//.AssignSkillPoint(SkillLibrary["Spellcraft"], 2)
					//.AssignSkillPoint(SkillLibrary["Use Magic Device"], 1)
					.SetDamage(5)
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
