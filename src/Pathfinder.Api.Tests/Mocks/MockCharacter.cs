using System;
using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Api.Tests.Mocks
{
	internal class MockCharacter : ICharacter
	{
		public string Name { get; }
		public int Age { get; }
		public Alignment Alignment { get; }
		public IEnumerable<ICharacterClass> Classes { get; }
		public Deity Deity { get; }
		public string Eyes { get; }
		public Gender Gender { get; }
		public string Hair { get; }
		public string Height { get; }
		public string Homeland { get; }
		public IRace Race { get; }
		public Size BaseSize { get; }
		public Size Size { get; }
		public string Weight { get; }
		public IEnumerable<ILanguage> Languages { get; }
		public int MaxHealthPoints { get; }
		public int HealthPoints { get; }
		public int Damage { get; }
		public int BaseSpeed { get; }
		public int ArmoredSpeed { get; }
		public IAbilityScore Strength { get; }
		public IAbilityScore Dexterity { get; }
		public IAbilityScore Constitution { get; }
		public IAbilityScore Intelligence { get; }
		public IAbilityScore Wisdom { get; }
		public IAbilityScore Charisma { get; }
		public ISavingThrow Fortitude { get; }
		public ISavingThrow Reflex { get; }
		public ISavingThrow Will { get; }
		public IDefenseScore ArmorClass { get; }
		public IDefenseScore FlatFooted { get; }
		public IDefenseScore Touch { get; }
		public IDefenseScore CombatManeuverDefense { get; }
		public int BaseAttackBonus { get; }
		public int BaseFortitude { get; }
		public int BaseReflex { get; }
		public int BaseWill { get; }
		public IOffensiveScore Melee { get; }
		public IOffensiveScore Ranged { get; }
		public IOffensiveScore CombatManeuverBonus { get; }
		public int ExperiencePoints { get; }
		public IExperience Experience { get; }
		public IEnumerable<IDie> HitDice { get; }
		public IEnumerable<ITrait> Traits { get; }
		public IEnumerable<IFeat> Feats { get; }
		public IEnumerable<ISkillScore> SkillScores { get; }

		public ISkillScore this[ISkill pSkill]
		{
			get { throw new NotImplementedException(); }
		}

		public IEnumerable<IWeapon> Weapons { get; }
		public IPurse Purse { get; }
		public IInventory Inventory { get; }
		public IEnumerable<IArmor> EquipedArmor { get; }
		public IEnumerable<IEffect> Effects { get; }
		public ICharacter SetRace(IRace pRace)
		{
			return this;
		}

		public ICharacter SetName(string pName)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetAge(int pAge)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetAlignment(Alignment pAlignment)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetHomeland(string pHomeland)
		{
			throw new NotImplementedException();
		}

		public ICharacter AddClass(IClass pClass)
		{
			throw new NotImplementedException();
		}

		public ICharacter IncrementClass(IClass pClass)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetDeity(Deity pDeity)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetGender(Gender pGender)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetEyes(string pEyes)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetHair(string pHair)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetHeight(string pHeight)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetWeight(string pWeight)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetDamage(int pDamage)
		{
			throw new NotImplementedException();
		}

		public ICharacter AddDamage(int pDamage)
		{
			throw new NotImplementedException();
		}

		public ICharacter AddExperience(IEvent pEvent)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetSkill(ISkill pSkill)
		{
			throw new NotImplementedException();
		}

		public ICharacter AssignSkillPoint(ISkill pSkill, int pPoint)
		{
			throw new NotImplementedException();
		}

		public ICharacter AddFeat(IFeat pFeat)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetPurse(int pCopper, int pSilver = 0, int pGold = 0, int pPlatinum = 0)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetInventory(IItem pItem)
		{
			throw new NotImplementedException();
		}

		public ICharacter EquipArmor(IArmor pArmor)
		{
			throw new NotImplementedException();
		}

		public ICharacter ReplaceArmor(IArmor pArmorToReplace, IArmor pArmorToEquip)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetStrength(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			throw new NotImplementedException();
		}

		public ICharacter Copy()
		{
			throw new NotImplementedException();
		}
	}

}
