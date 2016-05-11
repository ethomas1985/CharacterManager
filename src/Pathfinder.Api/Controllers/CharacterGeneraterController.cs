using System.Web.Http;
using Pathfinder.Api.Models;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Api.Controllers
{
	public class CharacterGeneraterController : ApiController
	{

		public CharacterGeneraterController()
		{
			CharacterLibrary = new LibraryFactory().GetCharacterLibrary();
			SkillLibrary = new LibraryFactory().GetSkillLibrary();
			RaceLibrary = new LibraryFactory().GetRaceLibrary();
		}

		/// <summary>
		/// For testing.
		/// 
		/// TODO - Should probably figure out how to get WebApi to inject the library.
		/// </summary>
		/// <param name="pCharacterLibrary"></param>
		/// <param name="pLibrary"></param>
		/// <param name="pSkillLibrary"></param>
		internal CharacterGeneraterController(ILibrary<ICharacter> pCharacterLibrary, ILibrary<IRace> pLibrary, ILibrary<ISkill> pSkillLibrary)
		{
			CharacterLibrary = pCharacterLibrary;
			RaceLibrary = pLibrary;
			SkillLibrary = pSkillLibrary;
		}

		internal ILibrary<ICharacter> CharacterLibrary { get; }
		internal ILibrary<IRace> RaceLibrary { get; }
		internal ILibrary<ISkill> SkillLibrary { get; }

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
	}
}
