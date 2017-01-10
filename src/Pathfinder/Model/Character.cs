using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;
using Pathfinder.Model.Currency;
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

		public IEnumerable<ICharacterClass> Classes { get; private set; } = new List<ICharacterClass>().ToImmutableList();

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

		public IEnumerable<ILanguage> Languages { get; private set; } = new List<ILanguage>();

		public int MaxHealthPoints
		{
			get
			{
				return Classes?.SelectMany(x => x.HitPoints).Select(x => x + Constitution.Modifier).Sum() ?? 0;
			}
		}
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

		public int Initiative => (Dexterity?.Modifier ?? 0);

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
				(int)Size,
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
				(int)Size,
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
				(int)Size,
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
				(int)Size,
				DeflectBonus,
				DodgeBonus,
				TemporaryBonus,
				MiscellaneousCombatManeuverDefenseModifier);
		private int MiscellaneousCombatManeuverDefenseModifier { get; set; }

		private int ArmorBonus
		{
			get { return EquipedArmor?.Where(x => x.ShieldBonus != 0).Sum(x => x.ArmorBonus) ?? 0; }
		}
		private int ShieldBonus
		{
			get { return EquipedArmor?.Where(x => x.ShieldBonus != 0).Sum(x => x.ArmorBonus) ?? 0; }
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
				(int)Size,
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
				(int)Size,
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
				(int)Size,
				TemporaryMeleeModifier,
				MiscellaneousCombatManeuverBonusModifier);
		private int MiscellaneousCombatManeuverBonusModifier { get; set; }

		public IExperience Experience { get; private set; } = new Experience();
		public int ExperiencePoints
		{
			get { return Experience?.Sum(x => x.ExperiencePoints) ?? 0; }
		}

		public IEnumerable<IDie> HitDice { get { return Classes.Select(x => x.Class.HitDie); } }

		public IEnumerable<IFeat> Feats { get; private set; }

		public int MaxSkillRanks => Classes.Sum(x => x.Level * (Intelligence.Modifier + x.SkillAddend));
		private ILibrary<ISkill> SkillLibrary { get; }
		private IEnumerable<ISkill> Skills => SkillLibrary;

		private IDictionary<ISkill, int> SkillRanks { get; } = new Dictionary<ISkill, int>().ToImmutableDictionary();
		private IDictionary<ISkill, int> MiscellenaousSkillBonuses { get; } = new Dictionary<ISkill, int>().ToImmutableDictionary();
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

		public IInventory Inventory { get; private set; }
		public IEnumerable<IArmor> EquipedArmor { get; private set; }
		public IEnumerable<IEffect> Effects { get; private set; }

		public IPurse Purse { get; internal set; } = new Purse(0);

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
			Tracer.Message(pMessage: $"{nameof(pRace)}: {pRace}");

			Assert.ArgumentNotNull(pRace, nameof(pRace));

			var newCharacter = _copy();

			newCharacter.Race = pRace;
			newCharacter.Languages =
				newCharacter.Languages.Except(Race?.Languages ?? new List<ILanguage>())
					.Union(newCharacter.Race.Languages).ToList();

			return newCharacter;
		}
		public ICharacter SetName(string pName)
		{
			Tracer.Message(pMessage: $"{nameof(pName)}: {pName}");

			Assert.ArgumentNotNull(pName, nameof(pName));

			var newCharacter = _copy();
			newCharacter.Name = pName;

			return newCharacter;
		}
		public ICharacter SetAge(int pAge)
		{
			Tracer.Message(pMessage: $"{nameof(pAge)}: {pAge}");

			Assert.IsTrue(pAge > 0, $"{nameof(pAge)} cannot be less than 1.");

			var newCharacter = _copy();
			newCharacter.Age = pAge;
			return newCharacter;
		}
		public ICharacter SetAlignment(Alignment pAlignment)
		{
			Tracer.Message(pMessage: $"{nameof(pAlignment)}: {pAlignment}");

			var newCharacter = _copy();
			newCharacter.Alignment = pAlignment;
			return newCharacter;
		}
		public ICharacter SetHomeland(string pHomeland)
		{
			Tracer.Message(pMessage: $"{nameof(pHomeland)}: {pHomeland}");

			Assert.ArgumentNotNull(pHomeland, nameof(pHomeland));

			var newCharacter = _copy();
			newCharacter.Homeland = pHomeland;
			return newCharacter;
		}
		public ICharacter SetDeity(IDeity pDeity)
		{
			Tracer.Message(pMessage: $"{nameof(pDeity)}: {pDeity}");

			Assert.ArgumentNotNull(pDeity, nameof(pDeity));

			var newCharacter = _copy();
			newCharacter.Deity = pDeity;
			return newCharacter;
		}
		public ICharacter SetGender(Gender pGender)
		{
			Tracer.Message(pMessage: $"{nameof(pGender)}: {pGender}");

			var newCharacter = _copy();
			newCharacter.Gender = pGender;
			return newCharacter;
		}
		public ICharacter SetEyes(string pEyes)
		{
			Tracer.Message(pMessage: $"{nameof(pEyes)}: {pEyes}");

			Assert.ArgumentNotNull(pEyes, nameof(pEyes));

			var newCharacter = _copy();
			newCharacter.Eyes = pEyes;
			return newCharacter;
		}
		public ICharacter SetHair(string pHair)
		{
			Tracer.Message(pMessage: $"{nameof(pHair)}: {pHair}");

			Assert.ArgumentNotNull(pHair, nameof(pHair));

			var newCharacter = _copy();
			newCharacter.Hair = pHair;
			return newCharacter;
		}
		public ICharacter SetHeight(string pHeight)
		{
			Tracer.Message(pMessage: $"{nameof(pHeight)}: {pHeight}");

			Assert.ArgumentNotNull(pHeight, nameof(pHeight));

			var newCharacter = _copy();
			newCharacter.Height = pHeight;
			return newCharacter;
		}
		public ICharacter SetWeight(string pWeight)
		{
			Tracer.Message(pMessage: $"{nameof(pWeight)}: {pWeight}");

			Assert.ArgumentNotNull(pWeight, nameof(pWeight));

			var newCharacter = _copy();
			newCharacter.Weight = pWeight;
			return newCharacter;
		}

		public ICharacter AddLanguage(ILanguage pLanguage)
		{
			Tracer.Message(pMessage: $"{nameof(pLanguage)}: {pLanguage}");

			Assert.ArgumentNotNull(pLanguage, nameof(pLanguage));

			var newCharacter = _copy();

			newCharacter.Languages = newCharacter.Languages.Append(pLanguage);

			return newCharacter;
		}

		public ICharacter RemoveLanguage(ILanguage pLanguage)
		{
			Tracer.Message(pMessage: $"{nameof(pLanguage)}: {pLanguage}");

			Assert.ArgumentNotNull(pLanguage, nameof(pLanguage));

			var newCharacter = _copy();

			newCharacter.Languages = newCharacter.Languages.Where(x => !x.Equals(pLanguage));

			return newCharacter;
		}

		public ICharacter SetStrength(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Tracer.Message(pMessage: $"{nameof(pBase)}: {pBase}, " +
									 $"{nameof(pEnhanced)}: {pEnhanced}, " +
									 $"{nameof(pInherent)}: {pInherent}");

			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.BaseStrength = pBase;
			newCharacter.EnhancedStrength = pEnhanced;
			newCharacter.InherentStrength = pInherent;
			return newCharacter;
		}
		public ICharacter SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Tracer.Message(pMessage: $"{nameof(pBase)}: {pBase}, " +
									 $"{nameof(pEnhanced)}: {pEnhanced}, " +
									 $"{nameof(pInherent)}: {pInherent}");

			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.BaseDexterity = pBase;
			newCharacter.EnhancedDexterity = pEnhanced;
			newCharacter.InherentDexterity = pInherent;
			return newCharacter;
		}
		public ICharacter SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Tracer.Message(pMessage: $"{nameof(pBase)}: {pBase}, " +
									 $"{nameof(pEnhanced)}: {pEnhanced}, " +
									 $"{nameof(pInherent)}: {pInherent}");

			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.BaseConstitution = pBase;
			newCharacter.EnhancedConstitution = pEnhanced;
			newCharacter.InherentConstitution = pInherent;
			return newCharacter;
		}
		public ICharacter SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Tracer.Message(pMessage: $"{nameof(pBase)}: {pBase}, " +
									 $"{nameof(pEnhanced)}: {pEnhanced}, " +
									 $"{nameof(pInherent)}: {pInherent}");

			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.BaseIntelligence = pBase;
			newCharacter.EnhancedIntelligence = pEnhanced;
			newCharacter.InherentIntelligence = pInherent;
			return newCharacter;
		}
		public ICharacter SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Tracer.Message(pMessage: $"{nameof(pBase)}: {pBase}, " +
									 $"{nameof(pEnhanced)}: {pEnhanced}, " +
									 $"{nameof(pInherent)}: {pInherent}");

			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.BaseWisdom = pBase;
			newCharacter.EnhancedWisdom = pEnhanced;
			newCharacter.InherentWisdom = pInherent;
			return newCharacter;
		}
		public ICharacter SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Tracer.Message(pMessage: $"{nameof(pBase)}: {pBase}, " +
									 $"{nameof(pEnhanced)}: {pEnhanced}, " +
									 $"{nameof(pInherent)}: {pInherent}");

			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.BaseCharisma = pBase;
			newCharacter.EnhancedCharisma = pEnhanced;
			newCharacter.InherentCharisma = pInherent;
			return newCharacter;
		}

		public ICharacter AddClass(IClass pClass)
		{
			//Tracer.Message(pMessage: $"{pClass}");

			Assert.ArgumentNotNull(pClass, nameof(pClass));

			var hitPoints = pClass.HitDie.Faces;
			return AddClass(pClass, 1, true, new List<int> { hitPoints });
		}
		public ICharacter AddClass(IClass pClass, int pLevel, bool pIsFavoredClass, IEnumerable<int> pHitPoints)
		{
			Assert.ArgumentNotNull(pClass, nameof(pClass));
			Assert.IsTrue(pLevel > 0, nameof(pLevel));

			var hitPointsList = pHitPoints as int[] ?? (pHitPoints?.ToArray() ?? new int[0]);
			Assert.IsTrue(hitPointsList.Length > 0, nameof(pHitPoints));

			Tracer.Message(pMessage: $"{nameof(pClass)}: {pClass}, " +
									 $"{nameof(pLevel)}: {pLevel}, " +
									 $"{nameof(pIsFavoredClass)}: {pIsFavoredClass}, " +
									 $"{nameof(pHitPoints)}: [ {string.Join(", ", hitPointsList)} ]");


			var newCharacter = _copy();

			var characterClass = new CharacterClass(pClass, pLevel, pIsFavoredClass, hitPointsList);
			newCharacter.Classes = Classes.Append(characterClass);

			return newCharacter;
		}
		public ICharacter IncrementClass(IClass pClass, int pHitPoints = 0)
		{
			Tracer.Message(pMessage: $"{nameof(pClass)}: {pClass}, " +
									 $"{nameof(pHitPoints)}: {pHitPoints}");

			Assert.ArgumentNotNull(pClass, nameof(pClass));

			var characterClass = Classes.FirstOrDefault(x => x.Class.Equals(pClass));
			if (characterClass == null)
			{
				throw new ArgumentException($"Class not found. Class name was {pClass.Name}");
			}

			var hitPoints = pHitPoints > 0 ? pHitPoints : pClass.HitDie.Faces;
			var newCharacterClass = characterClass.IncrementLevel(hitPoints);

			var newCharacter = _copy();
			newCharacter.Classes = Classes.Where(x => !x.Equals(characterClass)).Append(newCharacterClass);

			return newCharacter;
		}

		public ICharacter SetDamage(int pDamage)
		{
			Tracer.Message(pMessage: $"{nameof(pDamage)}: {pDamage}");

			Assert.IsTrue(pDamage >= 0, $"{nameof(pDamage)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.Damage = pDamage;
			return newCharacter;
		}
		public ICharacter AddDamage(int pDamage)
		{
			Tracer.Message(pMessage: $"{nameof(pDamage)}: {pDamage}");

			var newCharacter = _copy();
			newCharacter.Damage += pDamage;
			return newCharacter;
		}

		public ICharacter AppendExperience(IEvent pEvent)
		{
			Tracer.Message(pMessage: $"{nameof(pEvent)}: {pEvent}");
			Assert.ArgumentNotNull(pEvent, nameof(pEvent));

			var newCharacter = _copy();
			newCharacter.Experience = Experience.Append(pEvent);

			return newCharacter;
		}

		public ICharacter AppendExperience(IExperience pExperience)
		{
			Tracer.Message(pMessage: $"{nameof(pExperience)}: {pExperience}");
			Assert.ArgumentNotNull(pExperience, nameof(pExperience));

			var newCharacter = _copy();
			newCharacter.Experience = Experience.Append(pExperience);

			return newCharacter;
		}

		public ICharacter AssignSkillPoint(ISkill pSkill, int pPoint)
		{
			Tracer.Message(pMessage: $"{nameof(pSkill)}: {pSkill}, " +
									 $"{nameof(pPoint)}: {pPoint}");

			throw new NotImplementedException();
		}

		public ICharacter AddFeat(IFeat pFeat)
		{
			Tracer.Message(pMessage: $"{nameof(pFeat)}: {pFeat}");

			throw new NotImplementedException();
		}

		//internal Character SetWeapons() { throw new NotImplementedException(); }

		public ICharacter SetPurse(int pCopper, int pSilver = 0, int pGold = 0, int pPlatinum = 0)
		{
			Tracer.Message(pMessage: $"{nameof(pCopper)}: {pCopper}, " +
									 $"{nameof(pSilver)}: {pSilver}, " +
									 $"{nameof(pGold)}: {pGold}, " +
									 $"{nameof(pPlatinum)}: {pPlatinum}");

			var newCharacter = _copy();
			newCharacter.Purse =
				Purse.Add(
					new Copper(pCopper),
					new Silver(pSilver),
					new Gold(pGold),
					new Platinum(pPlatinum));
			return newCharacter;
		}

		public ICharacter SetInventory(IItem pItem)
		{
			Tracer.Message(pMessage: $"{nameof(pItem)}: {pItem}");

			throw new NotImplementedException();
		}

		public ICharacter EquipArmor(IArmor pArmor)
		{
			Tracer.Message(pMessage: $"{nameof(pArmor)}: {pArmor}");

			throw new NotImplementedException();
		}

		public ICharacter ReplaceArmor(IArmor pArmorToReplace, IArmor pArmorToEquip)
		{
			Tracer.Message(pMessage: $"{nameof(pArmorToReplace)}: {pArmorToReplace}, " +
									 $"{nameof(pArmorToEquip)}: {pArmorToEquip}");

			throw new NotImplementedException();
		}

		public ICharacter Copy()
		{
			return _copy();
		}

		private Character _copy()
		{
			return (Character)MemberwiseClone();
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

			Tracer.Message(pMessage: $"Comparing Two Non-null {nameof(Character)}s");

			var result = ComparisonUtilities.Compare(GetType().Name, Age, pOther.Age, nameof(Age));
			result &= ComparisonUtilities.Compare(GetType().Name, Alignment, pOther.Alignment, nameof(Alignment));
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, Classes, pOther.Classes, nameof(Classes));
			result &= ComparisonUtilities.Compare(GetType().Name, Deity, pOther.Deity, nameof(Deity));
			result &= ComparisonUtilities.Compare(GetType().Name, Gender, pOther.Gender, nameof(Gender));

			result &= ComparisonUtilities.CompareString(GetType().Name, Eyes, pOther.Eyes, nameof(Eyes));
			result &= ComparisonUtilities.CompareString(GetType().Name, Hair, pOther.Hair, nameof(Hair));
			result &= ComparisonUtilities.CompareString(GetType().Name, Height, pOther.Height, nameof(Height));
			result &= ComparisonUtilities.CompareString(GetType().Name, Weight, pOther.Weight, nameof(Weight));
			result &= ComparisonUtilities.CompareString(GetType().Name, Homeland, pOther.Homeland, nameof(Homeland));
			result &= ComparisonUtilities.CompareString(GetType().Name, Name, pOther.Name, nameof(Name));

			result &= ComparisonUtilities.Compare(GetType().Name, Race, pOther.Race, nameof(Race));

			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, Languages, pOther.Languages, nameof(Languages));

			result &= ComparisonUtilities.Compare(GetType().Name, Damage, pOther.Damage, nameof(Damage));
			result &= ComparisonUtilities.Compare(GetType().Name, ArmoredSpeed, pOther.ArmoredSpeed, nameof(ArmoredSpeed));

			result &= ComparisonUtilities.Compare(GetType().Name, Strength, pOther.Strength, nameof(Strength));
			result &= ComparisonUtilities.Compare(GetType().Name, Dexterity, pOther.Dexterity, nameof(Dexterity));
			result &= ComparisonUtilities.Compare(GetType().Name, Constitution, pOther.Constitution, nameof(Constitution));
			result &= ComparisonUtilities.Compare(GetType().Name, Intelligence, pOther.Intelligence, nameof(Intelligence));
			result &= ComparisonUtilities.Compare(GetType().Name, Wisdom, pOther.Wisdom, nameof(Wisdom));
			result &= ComparisonUtilities.Compare(GetType().Name, Charisma, pOther.Charisma, nameof(Charisma));
			result &= ComparisonUtilities.Compare(GetType().Name, Fortitude, pOther.Fortitude, nameof(Fortitude));
			result &= ComparisonUtilities.Compare(GetType().Name, Reflex, pOther.Reflex, nameof(Reflex));
			result &= ComparisonUtilities.Compare(GetType().Name, Will, pOther.Will, nameof(Will));
			result &= ComparisonUtilities.Compare(GetType().Name, ArmorClass, pOther.ArmorClass, nameof(ArmorClass));
			result &= ComparisonUtilities.Compare(GetType().Name, FlatFooted, pOther.FlatFooted, nameof(FlatFooted));
			result &= ComparisonUtilities.Compare(GetType().Name, Touch, pOther.Touch, nameof(Touch));
			result &= ComparisonUtilities.Compare(GetType().Name, CombatManeuverDefense, pOther.CombatManeuverDefense, nameof(CombatManeuverDefense));
			result &= ComparisonUtilities.Compare(GetType().Name, Melee, pOther.Melee, nameof(Melee));
			result &= ComparisonUtilities.Compare(GetType().Name, Ranged, pOther.Ranged, nameof(Ranged));
			result &= ComparisonUtilities.Compare(GetType().Name, CombatManeuverBonus, pOther.CombatManeuverBonus, nameof(CombatManeuverBonus));
			result &= ComparisonUtilities.Compare(GetType().Name, Experience, pOther.Experience, nameof(Experience));

			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, SkillScores, pOther.SkillScores, nameof(SkillScores));

			result &= ComparisonUtilities.Compare(GetType().Name, Feats, pOther.Feats, nameof(Feats));

			//result &= Equals(Weapons, other.Weapons);
			result &= ComparisonUtilities.Compare(GetType().Name, Inventory, pOther.Inventory, nameof(Inventory));

			//result &= Equals(EquipedArmor, other.EquipedArmor);
			//result &= Equals(Effects, other.Effects);
			result &= ComparisonUtilities.Compare(GetType().Name, Purse, pOther.Purse, nameof(Purse));

			Tracer.Message(pMessage: $"\tComparison resulted with {result}");

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Age;
				hashCode = (hashCode * 397) ^ (int)Alignment;
				hashCode = (hashCode * 397) ^ (Classes?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ Deity.GetHashCode();
				hashCode = (hashCode * 397) ^ (int)Gender;
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