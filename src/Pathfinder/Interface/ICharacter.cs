using Pathfinder.Enums;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;
using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface ICharacter : INamed
	{
		int Age { get; }
		Alignment Alignment { get; }

		IEnumerable<ICharacterClass> Classes { get; }

		IDeity Deity { get; }

		string Eyes { get; }

		Gender Gender { get; }
		string Hair { get; }
		string Height { get; }

		string Homeland { get; }

		IRace Race { get; }
		Size BaseSize { get; }
		Size Size { get; }

		string Weight { get; }

		IEnumerable<ILanguage> Languages { get; }

		int MaxHealthPoints { get; }
		int HealthPoints { get; }
		int Damage { get; }

		int BaseSpeed { get; }
		int Speed { get; }

		// Ability Scores
		IAbilityScore Strength { get; }
		IAbilityScore Dexterity { get; }
		IAbilityScore Constitution { get; }
		IAbilityScore Intelligence { get; }
		IAbilityScore Wisdom { get; }
		IAbilityScore Charisma { get; }

		int Initiative { get; }

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

		int MaxSkillRanks { get; }
		IEnumerable<ISkillScore> SkillScores { get; }
		ISkillScore this[ISkill pSkill] { get; }
		ISkillScore this[string pSkillName] { get; }

		IPurse Purse { get; }

		IInventory Inventory { get; }
		IEnumerable<IItem> Weapons { get; }
		IDictionary<ItemType, IItem> EquipedArmor { get; }
		int ArmorCheckPenalty { get; }

		IEnumerable<IEffect> Effects { get; }

		ICharacter SetRace(IRace pRace);

		ICharacter SetName(string pName);
		ICharacter SetAge(int pAge);
		ICharacter SetAlignment(Alignment pAlignment);
		ICharacter SetHomeland(string pHomeland);
		ICharacter SetDeity(IDeity pDeity);
		ICharacter SetGender(Gender pGender);
		ICharacter SetEyes(string pEyes);
		ICharacter SetHair(string pHair);
		ICharacter SetHeight(string pHeight);
		ICharacter SetWeight(string pWeight);

		ICharacter AddLanguage(ILanguage pLanguage);
		ICharacter RemoveLanguage(ILanguage pLanguage);

		ICharacter AddClass(IClass pClass);
		ICharacter AddClass(IClass pClass, int pLevel, bool pIsFavoredClass, IEnumerable<int> pHitPoints);
		ICharacter IncrementClass(IClass pClass, int pHitPoints = 0);

		ICharacter SetDamage(int pDamage);
		ICharacter AddDamage(int pDamage);

		ICharacter AppendExperience(IEvent pEvent);
		ICharacter AppendExperience(IExperience pExperience);

		ICharacter AssignSkillPoint(ISkill pSkill, int pPoint);

		ICharacter AddFeat(IFeat pFeat, string pSpecialization = null);

		ICharacter SetPurse(int pCopper, int pSilver = 0, int pGold = 0, int pPlatinum = 0);

		ICharacter AddToInventory(IItem pItem);
		ICharacter RemoveFromInventory(IItem pItem);
		ICharacter UpdateInventory(IItem pItem);

		ICharacter EquipArmor(IItem pArmorComponent);

		ICharacter ReplaceArmor(IItem pArmorToReplace, IItem pArmorToEquip);

		ICharacter SetStrength(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0);
		ICharacter SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0);

		ICharacter Copy();
	}
}