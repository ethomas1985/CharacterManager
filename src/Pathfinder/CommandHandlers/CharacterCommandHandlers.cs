using System;
using Pathfinder.Commands.Character;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.CommandHandlers
{
	public class CharacterCommandHandlers
	{
		private ILegacyRepository<ICharacter> Repository { get; }
		public ILegacyRepository<IRace> RaceRepository { get; }
		private ILegacyRepository<ISkill> SkillRepository { get; }

		public CharacterCommandHandlers(
			ILegacyRepository<ICharacter> pRepository,
			ILegacyRepository<ISkill> pSkillRepository,
			ILegacyRepository<IRace> pRaceRepository)
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

		public void Handle(SetCharacterName pCommand)
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

		public void Handle(SetCharacterAlignment pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetAlignment(pCommand.Alignment), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(SetCharacterHomeland pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetHomeland(pCommand.Homeland), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(SetCharacterDeity pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetDeity(pCommand.Deity), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(SetCharacterGender pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetGender(pCommand.Gender), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(SetCharacterEyes pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetEyes(pCommand.Eyes), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(SetCharacterHair pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetHair(pCommand.Hair), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(SetCharacterHeight pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetHeight(pCommand.Height), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(SetCharacterWeight pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetWeight(pCommand.Weight), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(AddCharacterLanguage pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.AddLanguage(pCommand.Language), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(RemoveCharacterLanguage pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.RemoveLanguage(pCommand.Language), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(AddCharacterClass pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(
					character.AddClass(pCommand.Class, pCommand.Level, pCommand.IsFavoredClass, pCommand.HitPoints),
					pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(IncrementCharacterClass pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.IncrementClass(pCommand.Class, pCommand.HitPoints), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(AddCharacterDamage pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.AddDamage(pCommand.Damage), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(AddExperienceEvent pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.AppendExperience(pCommand.Event), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(AssignCharacterSkillPoint pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.AssignSkillPoint(pCommand.Skill, pCommand.Rank), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(AddCharacterFeat pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.AddFeat(pCommand.Feat), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(AddToCharacterPurse pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.SetPurse(pCommand.Copper, pCommand.Silver, pCommand.Gold, pCommand.Platinum), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(AddItemToInventory pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.AddToInventory(pCommand.Item), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}

		public void Handle(RemoveItemFromInventory pCommand)
		{
			if (Repository.TryGetValue(pCommand.Id.ToString(), out ICharacter character))
			{
				Repository.Save(character.RemoveFromInventory(pCommand.Item), pCommand.OriginalVersion);
			}
			throw new Exception("Character Id not found.");
		}
	}
}
