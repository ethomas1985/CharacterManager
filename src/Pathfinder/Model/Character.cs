using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Library;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Character : ICharacter
	{
		public Character(IRace pRace, ILibrary<ISkill> pSkillLibrary)
		{
			Assert.ArgumentNotNull(pRace, nameof(pRace));

			Race = pRace;
			SkillLibrary = pSkillLibrary;
			Languages = new List<ILanguage>(Race.Languages);
		}

		public int Age { get; private set; }
		public Alignment Alignment { get; private set; }

		// ReSharper disable once InconsistentNaming 
		internal ICollection<ICharacterClass> classes { get; set; } = new List<ICharacterClass>();
		public IEnumerable<ICharacterClass> Classes
		{
			get { return classes.ToImmutableList(); }
			internal set { classes = value.ToList(); }
		}

		public Deity Deity { get; private set; }

		public Gender Gender { get; private set; }

		public string Eyes { get; private set; }
		public string Hair { get; private set; }
		public decimal Height { get; private set; }
		public decimal Weight { get; private set; }

		public string Homeland { get; private set; }

		public string Name { get; private set; }

		public IRace Race { get; }
		public Size BaseSize => Race.Size;
		public virtual Size Size => BaseSize;// + AnySizeModifier;

		public IEnumerable<ILanguage> Languages { get; }

		public int MaxHealthPoints { get { throw new NotImplementedException(); } }
		public int Damage { get; private set; }
		public int HealthPoints => MaxHealthPoints - Damage;

		public decimal BaseSpeed => Race.BaseSpeed;
		public decimal ArmoredSpeed { get; }

		public IAbilityScore Strength =>
			new AbilityScore(
				AbilityType.Strength,
				BaseStrength,
				EnhancedStrength,
				InherentStrength,
				TemporaryStrengthModifier);
		private int BaseStrength { get; set; }
		private int EnhancedStrength { get; set; }
		private int InherentStrength { get; set; }
		private int TemporaryStrengthModifier
		{
			get
			{
				return
					Race.AbilityScores[AbilityType.Strength] +
					Effects
						.Where(x => x.Active && x.StrengthModifier != 0)
						.Sum(x => x.StrengthModifier);
			}
		}

		public IAbilityScore Dexterity =>
			new AbilityScore(
				AbilityType.Dexterity,
				BaseDexterity,
				EnhancedDexterity,
				InherentDexterity,
				TemporaryDexterityModifier);
		private int BaseDexterity { get; set; }
		private int EnhancedDexterity { get; set; }
		private int InherentDexterity { get; set; }
		private int TemporaryDexterityModifier
		{
			get
			{
				return
					Race.AbilityScores[AbilityType.Dexterity] +
					Effects
						.Where(x => x.Active && x.DexterityModifier != 0)
						.Sum(x => x.DexterityModifier);
			}
		}

		public IAbilityScore Constitution =>
			new AbilityScore(
				AbilityType.Constitution,
				BaseConstitution,
				EnhancedConstitution,
				InherentConstitution,
				TemporaryConstitutionModifier);
		private int BaseConstitution { get; set; }
		private int EnhancedConstitution { get; set; }
		private int InherentConstitution { get; set; }
		private int TemporaryConstitutionModifier
		{
			get
			{
				return
					Race.AbilityScores[AbilityType.Constitution] +
					Effects
						.Where(x => x.Active && x.ConstitutionModifier != 0)
						.Sum(x => x.ConstitutionModifier);
			}
		}

		public IAbilityScore Intelligence =>
			new AbilityScore(
				AbilityType.Intelligence,
				BaseIntelligence,
				EnhancedIntelligence,
				InherentIntelligence,
				TemporaryIntelligenceModifier);
		private int BaseIntelligence { get; set; }
		private int EnhancedIntelligence { get; set; }
		private int InherentIntelligence { get; set; }
		private int TemporaryIntelligenceModifier
		{
			get
			{
				return
					Race.AbilityScores[AbilityType.Intelligence] +
					Effects
						.Where(x => x.Active && x.IntelligenceModifier != 0)
						.Sum(x => x.IntelligenceModifier);
			}
		}

		public IAbilityScore Wisdom =>
			new AbilityScore(
				AbilityType.Wisdom,
				BaseWisdom,
				EnhancedWisdom,
				InherentWisdom,
				TemporaryWisdomModifier);
		private int BaseWisdom { get; set; }
		private int EnhancedWisdom { get; set; }
		private int InherentWisdom { get; set; }
		private int TemporaryWisdomModifier
		{
			get
			{
				return
					Race.AbilityScores[AbilityType.Wisdom] +
					Effects
						.Where(x => x.Active && x.WisdomModifier != 0)
						.Sum(x => x.WisdomModifier);
			}
		}

		public IAbilityScore Charisma =>
			new AbilityScore(
				AbilityType.Charisma,
				BaseCharisma,
				EnhancedCharisma,
				InherentCharisma,
				TemporaryCharismaModifier);
		private int BaseCharisma { get; set; }
		private int EnhancedCharisma { get; set; }
		private int InherentCharisma { get; set; }
		private int TemporaryCharismaModifier
		{
			get
			{
				return
					Race.AbilityScores[AbilityType.Charisma] +
					Effects
						.Where(x => x.Active && x.CharismaModifier != 0)
						.Sum(x => x.CharismaModifier);
			}
		}

		private IAbilityScore this[AbilityType pAbilityType]
		{
			get
			{
				switch (pAbilityType)
				{
					case AbilityType.Strength:
						return Strength;
					case AbilityType.Dexterity:
						return Dexterity;
					case AbilityType.Constitution:
						return Constitution;
					case AbilityType.Intelligence:
						return Intelligence;
					case AbilityType.Wisdom:
						return Wisdom;
					case AbilityType.Charisma:
						return Charisma;
					default:
						throw new ArgumentException($"Unexpected Value for Ability Type: {pAbilityType}");
				}
			}
		}

		public IDefenseScore ArmorClass =>
			new DefenseScore(
				DefensiveType.ArmorClass,
				ArmorBonus,
				ShieldBonus,
				Dexterity,
				(int) Size,
				NaturalBonus,
				DeflectBonus,
				DodgeBonus,
				TemporaryBonus,
				MiscellaneousArmorClassModifier);
		private int MiscellaneousArmorClassModifier { get; set; }

		public IDefenseScore FlatFooted =>
			new DefenseScore(
				DefensiveType.FlatFooted,
				ArmorBonus,
				ShieldBonus,
				null,
				(int) Size,
				NaturalBonus,
				DeflectBonus,
				0,
				TemporaryBonus,
				MiscellaneousFlatFootedModifier);
		private int MiscellaneousFlatFootedModifier { get; set; }

		public IDefenseScore Touch =>
			new DefenseScore(
				DefensiveType.Touch,
				0,
				0,
				Dexterity,
				(int) Size,
				0,
				DeflectBonus,
				DodgeBonus,
				TemporaryBonus,
				MiscellaneousTouchModifier);
		private int MiscellaneousTouchModifier { get; set; }

		public IDefenseScore CombatManeuverDefense =>
			new DefenseScore(
				BaseAttackBonus,
				Strength,
				Dexterity,
				(int) Size,
				DeflectBonus,
				DodgeBonus,
				TemporaryBonus,
				MiscellaneousCombatManeuverDefenseModifier);
		private int MiscellaneousCombatManeuverDefenseModifier { get; set; }

		private int ArmorBonus
		{
			get { return EquipedArmor.Where(x => !x.IsShield).Sum(x => x.Bonus); }
		}
		private int ShieldBonus
		{
			get { return EquipedArmor.Where(x => x.IsShield).Sum(x => x.Bonus); }
		}
		private int NaturalBonus
		{
			get
			{
				return
					Effects
						.Where(x => x.Active && x.ArmorClassNaturalModifier != 0)
						.Sum(x => x.ArmorClassNaturalModifier);
			}
		}
		private int DeflectBonus
		{
			get
			{
				return
					Effects
						.Where(x => x.Active && x.Type == EffectType.Deflection)
						.Sum(x => x.ArmorClassOtherModifier);
			}
		}
		private int DodgeBonus
		{
			get
			{
				return
					Effects
						.Where(x => x.Active && x.Type == EffectType.Dodge)
						.Sum(x => x.ArmorClassOtherModifier);
			}
		}
		private int TemporaryBonus
		{
			get
			{
				return
					Effects
						.Where(x => x.Active
									&& x.Type != EffectType.Deflection
									&& x.Type != EffectType.Dodge)
						.Sum(x => x.ArmorClassNaturalModifier);
			}
		}
		public int BaseAttackBonus { get { return Classes.Sum(x => x.BaseAttackBonus); } }

		public ISavingThrow Fortitude =>
				new SavingThrow(
					SavingThrowType.Fortitude,
					Constitution,
					BaseFortitude,
					FortitudeResistance,
					TemporaryFortitude,
					MiscellaneousFortitudeModifier);
		public int BaseFortitude { get { return Classes.Sum(x => x.Fortitude); } }
		private int FortitudeResistance
		{
			get
			{
				return Effects
					.Where(x => x.Active && x.Type == EffectType.Resistance)
					.Sum(x => x.FortitudeModifier);
			}
		}
		private int TemporaryFortitude
		{
			get
			{
				return Effects
					.Where(x => x.Active && x.Type != EffectType.Resistance)
					.Sum(x => x.FortitudeModifier);
			}
		}
		private int MiscellaneousFortitudeModifier { get; set; }

		public ISavingThrow Reflex =>
				new SavingThrow(
					SavingThrowType.Reflex,
					Dexterity,
					BaseReflex,
					ReflexResistance,
					TemporaryReflex,
					MiscellaneousReflexModifier);
		public int BaseReflex { get { return Classes.Sum(x => x.Reflex); } }
		private int ReflexResistance
		{
			get
			{
				return Effects
					.Where(x => x.Active && x.Type == EffectType.Resistance)
					.Sum(x => x.ReflexModifier);
			}
		}
		private int TemporaryReflex
		{
			get
			{
				return Effects
					.Where(x => x.Active && x.Type != EffectType.Resistance)
					.Sum(x => x.ReflexModifier);
			}
		}
		private int MiscellaneousReflexModifier { get; set; }

		public ISavingThrow Will =>
				new SavingThrow(
					SavingThrowType.Will,
					Wisdom,
					BaseWill,
					WillResistance,
					TemporaryWill,
					MiscellaneousWillModifier);
		public int BaseWill { get { return Classes.Sum(x => x.Will); } }
		private int WillResistance
		{
			get
			{
				return Effects
					.Where(x => x.Active && x.Type == EffectType.Resistance)
					.Sum(x => x.WillModifier);
			}
		}
		private int TemporaryWill
		{
			get
			{
				return Effects
					.Where(x => x.Active && x.Type != EffectType.Resistance)
					.Sum(x => x.WillModifier);
			}
		}
		private int MiscellaneousWillModifier { get; set; }

		public IOffensiveScore Melee =>
			new OffensiveScore(
				OffensiveType.Melee,
				Strength,
				BaseAttackBonus,
				(int) Size,
				TemporaryMeleeModifier,
				MiscellaneousMeleeModifier);
		private int TemporaryMeleeModifier
		{
			get
			{
				return Effects
					.Where(x => x.Active)
					.Sum(x => x.MeleeAttackModifier);
			}
		}
		private int MiscellaneousMeleeModifier { get; set; }

		public IOffensiveScore Ranged =>
			new OffensiveScore(
				OffensiveType.Ranged,
				Dexterity,
				BaseAttackBonus,
				(int) Size,
				TemporaryRangedModifier,
				MiscellaneousRangedModifier);
		private int TemporaryRangedModifier
		{
			get
			{
				return Effects
					.Where(x => x.Active)
					.Sum(x => x.RangedAttackModifier);
			}
		}
		private int MiscellaneousRangedModifier { get; set; }

		public IOffensiveScore CombatManeuverBonus =>
			new OffensiveScore(
				OffensiveType.CombatManeuverBonus,
				Strength,
				BaseAttackBonus,
				(int) Size,
				TemporaryMeleeModifier,
				MiscellaneousCombatManeuverBonusModifier);
		private int MiscellaneousCombatManeuverBonusModifier { get; set; }

		public IExperience Experience { get; }
		public int ExperiencePoints
		{
			get { return Experience.Sum(x => x.ExperiencePoints); }
		}

		public IEnumerable<IDie> HitDice { get { return Classes.Select(x => x.Class.HitDie); } }

		public IEnumerable<IFeat> Feats { get; }

		private ILibrary<ISkill> SkillLibrary { get; }
		private IEnumerable<ISkill> Skills => SkillLibrary;

		private IDictionary<ISkill, int> SkillRanks { get; set; } = new Dictionary<ISkill, int>().ToImmutableDictionary();
		private IDictionary<ISkill, int> MiscellenaousSkillBonuses { get; set; } = new Dictionary<ISkill, int>().ToImmutableDictionary();
		public IEnumerable<ISkillScore> SkillScores
		{
			get {
				return Skills.Select(skill => this[skill]);
			}
		}
		public ISkillScore this[ISkill pSkill]
		{
			get
			{
				var ranks = GetSkillRanks(pSkill);
				var classModifier = GetClassModifier(pSkill);
				var misc = GetMiscellaneousSkillModifier(pSkill);
				var temp = GetTemporarySkillModifier(pSkill);
				var armorClassPenalty = EquipedArmor.Sum(x => x.ArmorCheckPenalty);

				return
					new SkillScore(
						pSkill,
						this[pSkill.AbilityType],
						ranks,
						classModifier,
						misc,
						temp,
						armorClassPenalty);
			}
		}

		public IEnumerable<IWeapon> Weapons { get; }

		public IInventory Inventory { get; }
		public IEnumerable<IArmor> EquipedArmor { get; }
		public IEnumerable<IEffect> Effects { get; }

		public IPurse Purse { get; internal set; }

		public IEnumerable<ITrait> Traits => Race.Traits;

		/*
		 * METHODS
		 */

		private int GetSkillRanks(ISkill pSkill)
		{
			int ranks;
			if (!SkillRanks.TryGetValue(pSkill, out ranks))
			{
				ranks = 0;
			}
			return ranks;
		}
		private int GetClassModifier(ISkill pSkill)
		{
			return Classes.Any(x => x.Class.Skills.Contains(pSkill)) ? 3 : 0;
		}
		private int GetMiscellaneousSkillModifier(ISkill pSkill)
		{
			int misc;
			if (!MiscellenaousSkillBonuses.TryGetValue(pSkill, out misc))
			{
				misc = 0;
			}
			return misc;
		}
		private int GetTemporarySkillModifier(ISkill pSkill)
		{
			var temp =
				Effects
					.Where(x => x.Active)
					.Sum(x => x[pSkill]);
			return temp;
		}

		public ICharacter SetName(string pName)
		{
			var newCharacter = copy();
			newCharacter.Name = pName;

			return newCharacter;
		}

		public ICharacter SetAge(int pAge)
		{
			var newCharacter = copy();
			newCharacter.Age = pAge;
			return newCharacter;
		}

		public ICharacter SetAlignment(Alignment pAlignment)
		{
			var newCharacter = copy();
			newCharacter.Alignment = pAlignment;
			return newCharacter;
		}

		public ICharacter SetHomeland(string pHomeland)
		{
			var newCharacter = copy();
			newCharacter.Homeland = pHomeland;
			return newCharacter;
		}

		//internal Character Set() { throw new NotImplementedException(); }

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
			var newCharacter = copy();
			newCharacter.Deity = pDeity;
			return newCharacter;
		}

		public ICharacter SetGender(Gender pGender)
		{
			var newCharacter = copy();
			newCharacter.Gender = pGender;
			return newCharacter;
		}

		public ICharacter SetEyes(string pEyes)
		{
			var newCharacter = copy();
			newCharacter.Eyes = pEyes;
			return newCharacter;
		}

		public ICharacter SetHair(string pHair)
		{
			var newCharacter = copy();
			newCharacter.Hair = pHair;
			return newCharacter;
		}

		public ICharacter SetHeight(decimal pHeight)
		{
			var newCharacter = copy();
			newCharacter.Height = pHeight;
			return newCharacter;
		}

		public ICharacter SetWeight(decimal pWeight)
		{
			var newCharacter = copy();
			newCharacter.Weight = pWeight;
			return newCharacter;
		}

		public ICharacter SetDamage(int pDamage)
		{
			var newCharacter = copy();
			newCharacter.Damage = pDamage;
			return newCharacter;
		}

		public ICharacter AddDamage(int pDamage)
		{
			var newCharacter = copy();
			newCharacter.Damage += pDamage;
			return newCharacter;
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

		//internal Character SetWeapons() { throw new NotImplementedException(); }

		public ICharacter SetPurse(int pCopper, int pSilver = 0, int pGold = 0, int pPlatinum = 0)
		{
			var newCharacter = copy();
			newCharacter.Purse = Purse.Add(pCopper, pSilver, pGold, pPlatinum);
			return newCharacter;
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
			var newCharacter = copy();
			newCharacter.BaseStrength = pBase;
			newCharacter.EnhancedStrength = pEnhanced;
			newCharacter.InherentStrength = pInherent;
			return newCharacter;
		}

		public ICharacter SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = copy();
			newCharacter.BaseDexterity = pBase;
			newCharacter.EnhancedDexterity = pEnhanced;
			newCharacter.InherentDexterity = pInherent;
			return newCharacter;
		}

		public ICharacter SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = copy();
			newCharacter.BaseConstitution = pBase;
			newCharacter.EnhancedConstitution = pEnhanced;
			newCharacter.InherentConstitution = pInherent;
			return newCharacter;
		}

		public ICharacter SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = copy();
			newCharacter.BaseIntelligence = pBase;
			newCharacter.EnhancedIntelligence = pEnhanced;
			newCharacter.InherentIntelligence = pInherent;
			return newCharacter;
		}

		public ICharacter SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = copy();
			newCharacter.BaseWisdom = pBase;
			newCharacter.EnhancedWisdom = pEnhanced;
			newCharacter.InherentWisdom = pInherent;
			return newCharacter;
		}

		public ICharacter SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = copy();
			newCharacter.BaseCharisma = pBase;
			newCharacter.EnhancedCharisma = pEnhanced;
			newCharacter.InherentCharisma = pInherent;
			return newCharacter;
		}

		public ICharacter Copy()
		{
			return copy();
		}

		private Character copy()
		{
			throw new NotImplementedException();
		}
	}
}