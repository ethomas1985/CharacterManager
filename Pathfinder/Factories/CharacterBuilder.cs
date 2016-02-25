using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Library;
using Pathfinder.Model.Character;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Pathfinder.Model.Factories
{
	public class CharacterBuilder : IDataErrorInfo, IValidatableObject
	{
		private static readonly Dictionary<Race, Func<AbstractCharacter>> _factories =
			new Dictionary<Race, Func<AbstractCharacter>>
			{
				[Race.Dwarf] = () => new Dwarf(),
				//[Race.Halfling] = () => new Halfling(),
				//[Race.Elf] = () => new Elf(),
				//[Race.Human] = () => new Human(),
				//[Race.Gnome] = () => new Gnome(),
				//[Race.HalfOrc] = () => new HalfOrc(),
				//[Race.HalfElf] = () => new HalfElf()
			};

		private static readonly Dictionary<string, Func<ICharacter, ValidationResult>> _validators =
			new Dictionary<string, Func<ICharacter, ValidationResult>>
			{
				[nameof(ICharacter.Classes)] = ValidateClasses,
				[nameof(ICharacter.Strength)] = ValidateStrength,
				[nameof(ICharacter.Dexterity)] = ValidateDexterity,
				[nameof(ICharacter.Constitution)] = ValidateConstitution,
				[nameof(ICharacter.Intelligence)] = ValidateIntelligence,
				[nameof(ICharacter.Wisdom)] = ValidateWisdom,
				[nameof(ICharacter.Charisma)] = ValidateCharisma,
			};

		public CharacterBuilder(Race pRace)
		{
			Func<AbstractCharacter> factory;
			if (!_factories.TryGetValue(pRace, out factory) && factory == null)
			{
				throw new ArgumentException($"Unsupported Race: {pRace}");
			}
			Character = factory();
		}

		internal CharacterBuilder(CharacterBuilder pBuilder)
		{
			Character = pBuilder.Character.InternalCopy();
		}

		internal AbstractCharacter Character { get; private set; }

		public bool Built { get; private set; }

		public string Error
		{
			get
			{
				var errors = Validate(null).Where(x => !x.Equals(ValidationResult.Success));
				return
					errors.Any()
						? string.Join(Environment.NewLine, errors.Select(x => x.ErrorMessage))
						: string.Empty;
			}
		}

		public string this[string columnName]
		{
			get
			{
				Func<ICharacter, ValidationResult> validator;
				if (_validators.TryGetValue(columnName, out validator))
				{
					return validator(Character).ErrorMessage;
				}
				return null;
			}
		}

		public ICharacter Build()
		{
			AssertNotBuilt();

			if (!string.IsNullOrEmpty(Error))
			{
				throw new Exception(Error);
			}

			Character.Initialize();

			Built = true;
			return Character;
		}

		public bool IsCharacterReady()
		{
			return Validate(null).All(x => x.Equals(ValidationResult.Success));
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext pValidationContext)
		{
			return _validators.Values.ToList().Select(x => x(Character));
		}

		private static ValidationResult ValidateStrength(ICharacter pCharacter)
		{
			if (pCharacter.Strength != null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Strength is a required Ability Score.");
		}

		private static ValidationResult ValidateDexterity(ICharacter pCharacter)
		{
			if (pCharacter.Dexterity != null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Dexterity is a required Ability Score.");
		}

		private static ValidationResult ValidateConstitution(ICharacter pCharacter)
		{
			if (pCharacter.Constitution != null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Constitution is a required Ability Score.");
		}

		private static ValidationResult ValidateIntelligence(ICharacter pCharacter)
		{
			if (pCharacter.Intelligence != null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Intelligence is a required Ability Score.");
		}

		private static ValidationResult ValidateWisdom(ICharacter pCharacter)
		{
			if (pCharacter.Wisdom != null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Wisdom is a required Ability Score.");
		}

		private static ValidationResult ValidateCharisma(ICharacter pCharacter)
		{
			if (pCharacter.Charisma != null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Charisma is a required Ability Score.");
		}

		private static ValidationResult ValidateClasses(ICharacter pCharacter)
		{
			return new ValidationResult("Validator is not Implemented");
		}

		private void AssertNotBuilt()
		{
			Assert(!Built, "Character has already been built.");
		}
		private static void Assert(bool pCondition, string pMessage, params object[] pParams)
		{
			if (!pCondition)
			{
				throw new Exception(string.Format(pMessage, pParams));
			}
		}

		public CharacterBuilder SetBaseStrength(int pStrength)
		{
			AssertNotBuilt();

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.SetStrength(pStrength);
			return newBuilder;
		}

		public CharacterBuilder SetBaseDexterity(int pDexterity)
		{
			AssertNotBuilt();

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.SetDexterity(pDexterity);
			return newBuilder;
		}

		public CharacterBuilder SetBaseConstitution(int pConstitution)
		{
			AssertNotBuilt();

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.SetConstitution(pConstitution);
			return newBuilder;
		}

		public CharacterBuilder SetBaseIntelligence(int pIntelligence)
		{
			AssertNotBuilt();

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.SetIntelligence(pIntelligence);
			return newBuilder;
		}

		public CharacterBuilder SetBaseWisdom(int pWisdom)
		{
			AssertNotBuilt();

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.SetWisdom(pWisdom);
			return newBuilder;
		}

		public CharacterBuilder SetBaseCharisma(int pCharisma)
		{
			AssertNotBuilt();

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.SetCharisma(pCharisma);
			return newBuilder;
		}

		public CharacterBuilder SetClass(IClass pClass)
		{
			AssertNotBuilt();
			Assert(pClass != null, $"{nameof(pClass)} cannot be null.");

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.classes.Add(pClass);
			return newBuilder;
		}

		public CharacterBuilder AddSkillRank(string pSkill, int pRank)
		{
			AssertNotBuilt();
			Assert(!string.IsNullOrEmpty(pSkill), $"{nameof(pSkill)} cannot be null.");

			var newBuilder = new CharacterBuilder(this);

			Skill outSkill;
			if (!newBuilder.Character.skills.TryGetValue(pSkill, out outSkill))
			{
				outSkill = SkillLibrary.CreateSkill(pSkill);
			}

			outSkill.Ranks += pRank;
			newBuilder.Character.skills[outSkill.Name] = outSkill;

			return newBuilder;
		}

		public CharacterBuilder AddSkill(ISkill pSkill)
		{
			AssertNotBuilt();
			Assert(pSkill != null, $"{nameof(pSkill)} cannot be null.");

			var newBuilder = new CharacterBuilder(this);
			var skill = SkillLibrary.CreateSkill(pSkill);

			newBuilder.Character.skills[pSkill.Name] = skill;
			return newBuilder;
		}

		public CharacterBuilder AddFeat(IFeat pFeat)
		{
			AssertNotBuilt();
			Assert(pFeat != null, $"{nameof(pFeat)} cannot be null.");

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.feats.Add(pFeat);
			return newBuilder;
		}

		public CharacterBuilder SetStartingGold(
			int pPlatinum = 0,
			int pGold = 0,
			int pSilver = 0,
			int pCopper = 0)
		{
			AssertNotBuilt();

			var newBuilder = new CharacterBuilder(this);
			newBuilder.Character.Purse.Platinum = pPlatinum;
			newBuilder.Character.Purse.Gold = pGold;
			newBuilder.Character.Purse.Silver = pGold;
			newBuilder.Character.Purse.Copper = pGold;
			return newBuilder;
		}
	}
}
