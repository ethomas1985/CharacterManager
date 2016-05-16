using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Character : ICharacter, IEquatable<ICharacter>
	{
		public Character(ILibrary<ISkill> pSkillLibrary)
		{
			SkillLibrary = pSkillLibrary;
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

		public IDeity Deity { get; private set; }

		public Gender Gender { get; private set; }

		public string Eyes { get; private set; }
		public string Hair { get; private set; }
		public string Height { get; private set; }
		public string Weight { get; private set; }

		public string Homeland { get; private set; }

		public string Name { get; private set; }

		public IRace Race { get; private set; }
		public Size BaseSize => Race?.Size ?? default(Size);
		public virtual Size Size => BaseSize;// + AnySizeModifier;

		public IEnumerable<ILanguage> Languages { get; private set; }

		public int MaxHealthPoints { get { return Classes?.SelectMany(x => x.HitPoints).Sum() ?? 0; } }
		public int Damage { get; private set; }
		public int HealthPoints => MaxHealthPoints - Damage;

		public int BaseSpeed => Race?.BaseSpeed ?? 0;
		public int ArmoredSpeed { get; }

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
				int strength;
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Strength, out strength))
				{
					return 0;
				}
				return strength
					+ Effects
						?.Where(x => x.Active && x.StrengthModifier != 0)
						.Sum(x => x.StrengthModifier) ?? 0;
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
				int dexterity;
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Dexterity, out dexterity))
				{
					return 0;
				}
				return dexterity
					+ Effects
						?.Where(x => x.Active && x.DexterityModifier != 0)
						.Sum(x => x.DexterityModifier) ?? 0;
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
				int constitution;
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Constitution, out constitution))
				{
					return 0;
				}
				return constitution
					+ Effects
						?.Where(x => x.Active && x.ConstitutionModifier != 0)
						.Sum(x => x.ConstitutionModifier) ?? 0;
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
				int intelligence;
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Intelligence, out intelligence))
				{
					return 0;
				}
				return intelligence
					+ Effects
						?.Where(x => x.Active && x.IntelligenceModifier != 0)
						.Sum(x => x.IntelligenceModifier) ?? 0;
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
				int wisdom;
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Wisdom, out wisdom))
				{
					return 0;
				}
				return wisdom
					+ Effects
						?.Where(x => x.Active && x.WisdomModifier != 0)
						.Sum(x => x.WisdomModifier) ?? 0;
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
				int charisma;
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Charisma, out charisma))
				{
					return 0;
				}
				return charisma
					+ Effects
						?.Where(x => x.Active && x.CharismaModifier != 0)
						.Sum(x => x.CharismaModifier) ?? 0;
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
				Dexterity ?? new AbilityScore(AbilityType.Dexterity, 0),
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
				Dexterity ?? new AbilityScore(AbilityType.Dexterity, 0),
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
				Strength ?? new AbilityScore(AbilityType.Strength, 0),
				Dexterity ?? new AbilityScore(AbilityType.Dexterity, 0),
				(int) Size,
				DeflectBonus,
				DodgeBonus,
				TemporaryBonus,
				MiscellaneousCombatManeuverDefenseModifier);
		private int MiscellaneousCombatManeuverDefenseModifier { get; set; }

		private int ArmorBonus
		{
			get { return EquipedArmor?.Where(x => !x.IsShield).Sum(x => x.Bonus) ?? 0; }
		}
		private int ShieldBonus
		{
			get { return EquipedArmor?.Where(x => x.IsShield).Sum(x => x.Bonus) ?? 0; }
		}
		private int NaturalBonus
		{
			get
			{
				return
					Effects
						?.Where(x => x.Active && x.ArmorClassNaturalModifier != 0)
						.Sum(x => x.ArmorClassNaturalModifier) ?? 0;
			}
		}
		private int DeflectBonus
		{
			get
			{
				return
					Effects
						?.Where(x => x.Active && x.Type == EffectType.Deflection)
						.Sum(x => x.ArmorClassOtherModifier) ?? 0;
			}
		}
		private int DodgeBonus
		{
			get
			{
				return
					Effects
						?.Where(x => x.Active && x.Type == EffectType.Dodge)
						.Sum(x => x.ArmorClassOtherModifier) ?? 0;
			}
		}
		private int TemporaryBonus
		{
			get
			{
				return
					Effects
						?.Where(x => x.Active
									&& x.Type != EffectType.Deflection
									&& x.Type != EffectType.Dodge)
						.Sum(x => x.ArmorClassNaturalModifier) ?? 0;
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
					?.Where(x => x.Active && x.Type == EffectType.Resistance)
					.Sum(x => x.FortitudeModifier) ?? 0;
			}
		}
		private int TemporaryFortitude
		{
			get
			{
				return Effects
					?.Where(x => x.Active && x.Type != EffectType.Resistance)
					.Sum(x => x.FortitudeModifier) ?? 0;
			}
		}
		private int MiscellaneousFortitudeModifier { get; set; }

		public ISavingThrow Reflex =>
				new SavingThrow(
					SavingThrowType.Reflex,
					Dexterity ?? new AbilityScore(AbilityType.Dexterity, 0),
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
					?.Where(x => x.Active && x.Type == EffectType.Resistance)
					.Sum(x => x.ReflexModifier) ?? 0;
			}
		}
		private int TemporaryReflex
		{
			get
			{
				return Effects
					?.Where(x => x.Active && x.Type != EffectType.Resistance)
					.Sum(x => x.ReflexModifier) ?? 0;
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
					?.Where(x => x.Active && x.Type == EffectType.Resistance)
					.Sum(x => x.WillModifier) ?? 0;
			}
		}
		private int TemporaryWill
		{
			get
			{
				return Effects
					?.Where(x => x.Active && x.Type != EffectType.Resistance)
					.Sum(x => x.WillModifier) ?? 0;
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
					?.Where(x => x.Active)
					.Sum(x => x.MeleeAttackModifier) ?? 0;
			}
		}
		private int MiscellaneousMeleeModifier { get; set; }

		public IOffensiveScore Ranged =>
			new OffensiveScore(
				OffensiveType.Ranged,
				Dexterity ?? new AbilityScore(AbilityType.Dexterity, 0),
				BaseAttackBonus,
				(int) Size,
				TemporaryRangedModifier,
				MiscellaneousRangedModifier);
		private int TemporaryRangedModifier
		{
			get
			{
				return Effects
					?.Where(x => x.Active)
					.Sum(x => x.RangedAttackModifier) ?? 0;
			}
		}
		private int MiscellaneousRangedModifier { get; set; }

		public IOffensiveScore CombatManeuverBonus =>
			new OffensiveScore(
				OffensiveType.CombatManeuverBonus,
				Strength ?? new AbilityScore(AbilityType.Strength, 0),
				BaseAttackBonus,
				(int) Size,
				TemporaryMeleeModifier,
				MiscellaneousCombatManeuverBonusModifier);
		private int MiscellaneousCombatManeuverBonusModifier { get; set; }

		public IExperience Experience { get; }
		public int ExperiencePoints
		{
			get { return Experience?.Sum(x => x.ExperiencePoints) ?? 0; }
		}

		public IEnumerable<IDie> HitDice { get { return Classes.Select(x => x.Class.HitDie); } }

		public IEnumerable<IFeat> Feats { get; }

		private ILibrary<ISkill> SkillLibrary { get; }
		private IEnumerable<ISkill> Skills => SkillLibrary;

		private IDictionary<ISkill, int> SkillRanks { get; set; } = new Dictionary<ISkill, int>().ToImmutableDictionary();
		private IDictionary<ISkill, int> MiscellenaousSkillBonuses { get; set; } = new Dictionary<ISkill, int>().ToImmutableDictionary();
		public IEnumerable<ISkillScore> SkillScores
		{
			get
			{
				var skillScores = Skills?.Select(skill => this[skill]).ToImmutableList();
				return skillScores
					?? new List<ISkillScore>().ToImmutableList();
			}
		}
		public ISkillScore this[ISkill pSkill]
		{
			get
			{
				var ranks = _GetSkillRanks(pSkill);
				var classModifier = _GetClassModifier(pSkill);
				var misc = _GetMiscellaneousSkillModifier(pSkill);
				var temp = _GetTemporarySkillModifier(pSkill);
				var armorClassPenalty = EquipedArmor?.Sum(x => x.ArmorCheckPenalty) ?? 0;

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

		public IEnumerable<ITrait> Traits => Race?.Traits;

		/*
		 * METHODS
		 */

		private int _GetSkillRanks(ISkill pSkill)
		{
			int ranks;
			if (!SkillRanks.TryGetValue(pSkill, out ranks))
			{
				ranks = 0;
			}
			return ranks;
		}
		private int _GetClassModifier(ISkill pSkill)
		{
			return
				(Classes != null && Classes.Any(x => x.Class.Skills.Contains(pSkill.Name)))
					? 3
					: 0;
		}
		private int _GetMiscellaneousSkillModifier(ISkill pSkill)
		{
			int misc;
			if (!MiscellenaousSkillBonuses.TryGetValue(pSkill, out misc))
			{
				misc = 0;
			}
			return misc;
		}
		private int _GetTemporarySkillModifier(ISkill pSkill)
		{
			var temp =
				Effects
					?.Where(x => x.Active)
					.Sum(x => x[pSkill]) ?? 0;
			return temp;
		}

		public ICharacter SetRace(IRace pRace)
		{
			Assert.ArgumentNotNull(pRace, nameof(pRace));

			var newCharacter = _copy();

			newCharacter.Race = pRace;
			newCharacter.Languages = new List<ILanguage>(newCharacter.Race.Languages);

			return newCharacter;
		}

		public ICharacter SetName(string pName)
		{
			var newCharacter = _copy();
			newCharacter.Name = pName;

			return newCharacter;
		}

		public ICharacter SetAge(int pAge)
		{
			var newCharacter = _copy();
			newCharacter.Age = pAge;
			return newCharacter;
		}

		public ICharacter SetAlignment(Alignment pAlignment)
		{
			var newCharacter = _copy();
			newCharacter.Alignment = pAlignment;
			return newCharacter;
		}

		public ICharacter SetHomeland(string pHomeland)
		{
			var newCharacter = _copy();
			newCharacter.Homeland = pHomeland;
			return newCharacter;
		}

		public ICharacter AddClass(IClass pClass)
		{
			Assert.ArgumentNotNull(pClass, nameof(pClass));

			var newCharacter = _copy();

			var hitPoints = pClass.HitDie.Faces + Constitution.Modifier;
			var characterClass = new CharacterClass(pClass, true, hitPoints);

			newCharacter.classes.Add(characterClass);

			return newCharacter;
		}
		public ICharacter AddClass(IClass pClass, int pLevel, bool pIsFavoredClass, IEnumerable<int> pHitPoints)
		{
			Assert.ArgumentNotNull(pClass, nameof(pClass));

			var newCharacter = _copy();

			var hitPoints = pClass.HitDie.Faces + Constitution.Modifier;
			var characterClass = new CharacterClass(pClass, pLevel, pIsFavoredClass, pHitPoints);

			newCharacter.classes.Add(characterClass);

			return newCharacter;
		}

		public ICharacter IncrementClass(IClass pClass)
		{
			throw new NotImplementedException();
		}

		public ICharacter SetDeity(IDeity pDeity)
		{
			var newCharacter = _copy();
			newCharacter.Deity = pDeity;
			return newCharacter;
		}

		public ICharacter SetGender(Gender pGender)
		{
			var newCharacter = _copy();
			newCharacter.Gender = pGender;
			return newCharacter;
		}

		public ICharacter SetEyes(string pEyes)
		{
			var newCharacter = _copy();
			newCharacter.Eyes = pEyes;
			return newCharacter;
		}

		public ICharacter SetHair(string pHair)
		{
			var newCharacter = _copy();
			newCharacter.Hair = pHair;
			return newCharacter;
		}

		public ICharacter SetHeight(string pHeight)
		{
			var newCharacter = _copy();
			newCharacter.Height = pHeight;
			return newCharacter;
		}

		public ICharacter SetWeight(string pWeight)
		{
			var newCharacter = _copy();
			newCharacter.Weight = pWeight;
			return newCharacter;
		}

		public ICharacter SetDamage(int pDamage)
		{
			var newCharacter = _copy();
			newCharacter.Damage = pDamage;
			return newCharacter;
		}

		public ICharacter AddDamage(int pDamage)
		{
			var newCharacter = _copy();
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
			var newCharacter = _copy();
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
			var newCharacter = _copy();
			newCharacter.BaseStrength = pBase;
			newCharacter.EnhancedStrength = pEnhanced;
			newCharacter.InherentStrength = pInherent;
			return newCharacter;
		}

		public ICharacter SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = _copy();
			newCharacter.BaseDexterity = pBase;
			newCharacter.EnhancedDexterity = pEnhanced;
			newCharacter.InherentDexterity = pInherent;
			return newCharacter;
		}

		public ICharacter SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = _copy();
			newCharacter.BaseConstitution = pBase;
			newCharacter.EnhancedConstitution = pEnhanced;
			newCharacter.InherentConstitution = pInherent;
			return newCharacter;
		}

		public ICharacter SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = _copy();
			newCharacter.BaseIntelligence = pBase;
			newCharacter.EnhancedIntelligence = pEnhanced;
			newCharacter.InherentIntelligence = pInherent;
			return newCharacter;
		}

		public ICharacter SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = _copy();
			newCharacter.BaseWisdom = pBase;
			newCharacter.EnhancedWisdom = pEnhanced;
			newCharacter.InherentWisdom = pInherent;
			return newCharacter;
		}

		public ICharacter SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			var newCharacter = _copy();
			newCharacter.BaseCharisma = pBase;
			newCharacter.EnhancedCharisma = pEnhanced;
			newCharacter.InherentCharisma = pInherent;
			return newCharacter;
		}

		public ICharacter Copy()
		{
			return _copy();
		}

		private Character _copy()
		{
			return (Character) MemberwiseClone();
		}

		public override bool Equals(object pObj)
		{
			return Equals(pObj as ICharacter);
		}

		public bool Equals(ICharacter pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var result = Age == pOther.Age;

			result &= Alignment == pOther.Alignment;
			result &= classes.SequenceEqual(pOther.Classes);
			result &= Equals(Deity, pOther.Deity);
			result &= Gender == pOther.Gender;
			result &= string.Equals(Eyes, pOther.Eyes, StringComparison.InvariantCultureIgnoreCase);
			result &= string.Equals(Hair, pOther.Hair, StringComparison.InvariantCultureIgnoreCase);
			result &= string.Equals(Height, pOther.Height, StringComparison.InvariantCultureIgnoreCase);
			result &= string.Equals(Weight, pOther.Weight, StringComparison.InvariantCultureIgnoreCase);
			result &= string.Equals(Homeland, pOther.Homeland, StringComparison.InvariantCultureIgnoreCase);
			result &= string.Equals(Name, pOther.Name, StringComparison.InvariantCultureIgnoreCase);
			result &= Equals(Race, pOther.Race);
			result &= (Languages != null && pOther.Languages != null) && Languages.SequenceEqual(pOther.Languages);
			result &= Damage == pOther.Damage;
			result &= ArmoredSpeed == pOther.ArmoredSpeed;
			result &= Equals(Strength, pOther.Strength);
			result &= Equals(Dexterity, pOther.Dexterity);
			result &= Equals(Constitution, pOther.Constitution);
			result &= Equals(Intelligence, pOther.Intelligence);
			result &= Equals(Wisdom, pOther.Wisdom);
			result &= Equals(Charisma, pOther.Charisma);
			result &= Equals(Fortitude, pOther.Fortitude);
			result &= Equals(Reflex, pOther.Reflex);
			result &= Equals(Will, pOther.Will);
			result &= Equals(ArmorClass, pOther.ArmorClass);
			result &= Equals(FlatFooted, pOther.FlatFooted);
			result &= Equals(Touch, pOther.Touch);
			result &= Equals(CombatManeuverDefense, pOther.CombatManeuverDefense);
			result &= Equals(Melee, pOther.Melee);
			result &= Equals(Ranged, pOther.Ranged);
			result &= Equals(CombatManeuverBonus, pOther.CombatManeuverBonus);
			result &= Equals(Experience, pOther.Experience);

			var skillScoresEqual = SkillScores.SequenceEqual(pOther.SkillScores);
			result &= skillScoresEqual;
			if (!skillScoresEqual)
			{
				Tracer.Message(pMessage: $"{nameof(SkillScores)} :: {string.Join(", ", SkillScores.Select(x => x.ToString()))}");
				Tracer.Message(pMessage: $"{nameof(pOther)}.{nameof(SkillScores)} :: {string.Join(", ", pOther.SkillScores.Select(x => x.ToString()))}");
			}

			result &= Equals(Feats, pOther.Feats);
			//result &= Equals(Weapons, other.Weapons);
			result &= Equals(Inventory, pOther.Inventory);
			//result &= Equals(EquipedArmor, other.EquipedArmor);
			//result &= Equals(Effects, other.Effects);
			 result &= Equals(Purse, pOther.Purse);

			return result;
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Age;
				hashCode = (hashCode * 397) ^ (int) Alignment;
				hashCode = (hashCode * 397) ^ (classes?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ Deity.GetHashCode();
				hashCode = (hashCode * 397) ^ (int) Gender;
				hashCode = (hashCode * 397) ^ (Eyes != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Eyes) : 0);
				hashCode = (hashCode * 397) ^ (Hair != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Hair) : 0);
				hashCode = (hashCode * 397) ^ (Height != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Height) : 0);
				hashCode = (hashCode * 397) ^ (Weight != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Weight) : 0);
				hashCode = (hashCode * 397) ^ (Homeland != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Homeland) : 0);
				hashCode = (hashCode * 397) ^ (Name != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Name) : 0);
				hashCode = (hashCode * 397) ^ (Race?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Languages?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ Damage;
				hashCode = (hashCode * 397) ^ ArmoredSpeed;
				hashCode = (hashCode * 397) ^ BaseStrength;
				hashCode = (hashCode * 397) ^ BaseDexterity;
				hashCode = (hashCode * 397) ^ BaseConstitution;
				hashCode = (hashCode * 397) ^ BaseIntelligence;
				hashCode = (hashCode * 397) ^ BaseWisdom;
				hashCode = (hashCode * 397) ^ BaseCharisma;
				hashCode = (hashCode * 397) ^ ArmorClass.GetHashCode();
				hashCode = (hashCode * 397) ^ FlatFooted.GetHashCode();
				hashCode = (hashCode * 397) ^ Touch.GetHashCode();
				hashCode = (hashCode * 397) ^ CombatManeuverDefense.GetHashCode();
				hashCode = (hashCode * 397) ^ Fortitude.GetHashCode();
				hashCode = (hashCode * 397) ^ Reflex.GetHashCode();
				hashCode = (hashCode * 397) ^ Will.GetHashCode();
				hashCode = (hashCode * 397) ^ Melee.GetHashCode();
				hashCode = (hashCode * 397) ^ Ranged.GetHashCode();
				hashCode = (hashCode * 397) ^ CombatManeuverBonus.GetHashCode();
				hashCode = (hashCode * 397) ^ (Experience?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Feats?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (SkillLibrary?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (SkillRanks?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (MiscellenaousSkillBonuses?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Weapons?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Inventory?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (EquipedArmor?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Effects?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Purse?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}