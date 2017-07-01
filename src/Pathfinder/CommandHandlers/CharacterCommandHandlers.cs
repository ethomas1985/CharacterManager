using System;
using Pathfinder.Commands.Character;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.CommandHandlers
{
	public class CharacterCommandHandlers
	{
		private IRepository<ICharacter> Repository { get; }
		public IRepository<IRace> RaceRepository { get; }
		private IRepository<ISkill> SkillRepository { get; }

		public CharacterCommandHandlers(
			IRepository<ICharacter> pRepository,
			IRepository<ISkill> pSkillRepository,
			IRepository<IRace> pRaceRepository)
		{
			Repository = pRepository;
			SkillRepository = pSkillRepository;
			RaceRepository = pRaceRepository;
		}

		public void Handle(CreateCharacter pCommand)
		{
			if (!Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(new Character(SkillRepository), 0);
			}
			throw new Exception("Character Id already used.");
		}

		public void Handle(SetCharacterRace pCommand)
		{
			if (!Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				throw new Exception("Character Id not found.");
			}

			if (!RaceRepository.TryGetValue(pCommand.RaceName, out IRace race))
			{
				throw new Exception($"Race not found; {{{pCommand.RaceName}}}");
			}

			Repository.Save(character.SetRace(race), pCommand.OriginalVersion);
		}

		public void Handle(SetName pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetName(pCommand.Name), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(SetCharacterAge pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetAge(pCommand.Age), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}
	}
}
