using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface ICharacter
	{
		int Age { get; }
		Alignment Alignment { get; }

		IEnumerable<ICharacterClass> Classes { get; }

		Deity Deity { get; }

		string Eyes { get; }

		Gender Gender { get; }
		string Hair { get; }
		decimal Height { get; }

		string Homeland { get; }

		string Name { get; }

		IRace Race { get; }
		Size BaseSize { get; }
		Size Size { get; }

		decimal Weight { get; }

		IEnumerable<ILanguage> Languages { get; }

		int MaxHealthPoints { get; }
		int HealthPoints { get; }
		int Damage { get; }

		decimal BaseSpeed { get; }
		decimal ArmoredSpeed { get; }

		// Ability Scores
		IAbilityScore Strength { get; }
		IAbilityScore Dexterity { get; }
		IAbilityScore Constitution { get; }
		IAbilityScore Intelligence { get; }
		IAbilityScore Wisdom { get; }
		IAbilityScore Charisma { get; }

		// Saving Throws
		ISavingThrow Fortitude { get; }
		ISavingThrow Reflex { get; }
		ISavingThrow Will { get; }

		// Defensive Scores
		IDefenseScore ArmorClass { get; }
		IDefenseScore FlatFooted { get; }
		IDefenseScore Touch { get; }
		IDefenseScore CombatManeuverDefense { get; }

		/// <summary>
		/// Sum of the BaseAttackBonus of all classes.
		/// </summary>
		int BaseAttackBonus { get; }
		int BaseFortitude { get; }
		int BaseReflex { get; }
		int BaseWill { get; }

		// Offensive Scores
		IOffensiveScore Melee { get; }
		IOffensiveScore Ranged { get; }
		IOffensiveScore CombatManeuverBonus { get; }

		//IDictionary<BonusType, int> Bonus { get; }

		int ExperiencePoints { get; }
		IExperience Experience { get; }

		IEnumerable<IDie> HitDice { get; }

		IEnumerable<ITrait> Traits { get; }
		IEnumerable<IFeat> Feats { get; }

		IEnumerable<ISkillScore> SkillScores { get; }
		ISkillScore this[ISkill pSkill] { get; }

		IEnumerable<IWeapon> Weapons { get; }

		IPurse Purse { get; }

		IInventory Inventory { get; }

		IEnumerable<IArmor> EquipedArmor { get; }
		IEnumerable<IEffect> Effects { get; }

		ICharacter SetName(string pName);
		ICharacter SetAge(int pAge);
		ICharacter SetAlignment(Alignment pAlignment);
		ICharacter SetHomeland(string pHomeland);
		ICharacter AddClass(IClass pClass);
		ICharacter IncrementClass(IClass pClass);
		ICharacter SetDeity(Deity pDeity);
		ICharacter SetGender(Gender pGender);
		ICharacter SetEyes(string pEyes);
		ICharacter SetHair(string pHair);
		ICharacter SetHeight(decimal pHeight);
		ICharacter SetWeight(decimal pWeight);
		ICharacter SetDamage(int pDamage);
		ICharacter AddDamage(int pDamage);
		ICharacter AddExperience(IEvent pEvent);
		//ICharacter SetSkills(SkillsCollection pSkillsColection);
		ICharacter SetSkill(ISkill pSkill);
		ICharacter AssignSkillPoint(ISkill pSkill, int pPoint);
		ICharacter AddFeat(IFeat pFeat);
		ICharacter SetPurse(int pCopper, int pSilver = 0, int pGold = 0, int pPlatinum = 0);
		ICharacter SetInventory(IItem pItem);
		ICharacter EquipArmor(IArmor pArmor);
		ICharacter ReplaceArmor(IArmor pArmorToReplace, IArmor pArmorToEquip);
		ICharacter SetStrength(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0);

		ICharacter Copy();
	}
}