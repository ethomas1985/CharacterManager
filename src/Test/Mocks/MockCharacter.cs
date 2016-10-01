using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;
using System;
using System.Collections.Generic;

namespace Test.Mocks
{
	public class MockCharacter : ICharacter
	{
		public string Name { get; }
		public int Age { get; }
		public Alignment Alignment { get; }
		public IEnumerable<ICharacterClass> Classes { get; }
		public IDeity Deity { get; }
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
		public int Initiative { get; }
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
		public int MaxSkillRanks { get; }
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
			return this;
		}

		public ICharacter SetAge(int pAge)
		{
			return this;
		}

		public ICharacter SetAlignment(Alignment pAlignment)
		{
			return this;
		}

		public ICharacter SetHomeland(string pHomeland)
		{
			return this;
		}

		public ICharacter RemoveLanguage(ILanguage pLanguage)
		{
			throw new NotImplementedException();
		}

		public ICharacter AddClass(IClass pClass)
		{
			return this;
		}

		public ICharacter AddClass(IClass pClass, int pLevel, bool pIsFavoredClass, IEnumerable<int> pHitPoints)
		{
			throw new NotImplementedException();
		}

		public ICharacter IncrementClass(IClass pClass, int pHitPoints = 0)
		{
			return this;
		}

		public ICharacter SetDeity(IDeity pDeity)
		{
			return this;
		}

		public ICharacter SetGender(Gender pGender)
		{
			return this;
		}

		public ICharacter SetEyes(string pEyes)
		{
			return this;
		}

		public ICharacter SetHair(string pHair)
		{
			return this;
		}

		public ICharacter SetHeight(string pHeight)
		{
			return this;
		}

		public ICharacter SetWeight(string pWeight)
		{
			return this;
		}

		public ICharacter AddLanguage(ILanguage pLanguage)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetDamage(int pDamage)
		{
			return this;
		}

		public ICharacter AddDamage(int pDamage)
		{
			return this;
		}

		public ICharacter SetExperience(IExperience pExperience)
		{
			return this;
		}

		public ICharacter SetSkill(ISkill pSkill)
		{
			return this;
		}

		public ICharacter AssignSkillPoint(ISkill pSkill, int pPoint)
		{
			return this;
		}

		public ICharacter AddFeat(IFeat pFeat)
		{
			return this;
		}

		public ICharacter SetPurse(int pCopper, int pSilver = 0, int pGold = 0, int pPlatinum = 0)
		{
			return this;
		}

		public ICharacter SetInventory(IItem pItem)
		{
			return this;
		}

		public ICharacter EquipArmor(IArmor pArmor)
		{
			return this;
		}

		public ICharacter ReplaceArmor(IArmor pArmorToReplace, IArmor pArmorToEquip)
		{
			return this;
		}

		public ICharacter SetStrength(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			return this;
		}

		public ICharacter SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			return this;
		}

		public ICharacter SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			return this;
		}

		public ICharacter SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			return this;
		}

		public ICharacter SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			return this;
		}

		public ICharacter SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			return this;
		}

		public ICharacter Copy()
		{
			return this;
		}
	}

}
