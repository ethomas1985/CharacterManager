using Pathfinder.Enums;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Currency;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Model
{
	internal class Character : AbstractAggregate, ICharacter, IEquatable<ICharacter>
	{
		private ImmutableDictionary<ItemType, IItem> _equipedArmor = ImmutableDictionary<ItemType, IItem>.Empty;

		public Character(IRepository<ISkill> pSkillRepository)
		{
			SkillRepository = pSkillRepository;
			ApplyChange(new CharacterCreated(Guid.NewGuid()), true);
		}

		public Character(IRepository<ISkill> pSkillRepository, Guid pId)
		{
			SkillRepository = pSkillRepository;
			ApplyChange(new CharacterCreated(pId), true);
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

		public int MaxHealthPoints =>
			Classes?
				.SelectMany(x => x.HitPoints)
				.Select(x => x + Constitution.Modifier)
				.Sum() ?? 0;
		public int Damage { get; private set; }
		public int HealthPoints => MaxHealthPoints - Damage;

		public int BaseSpeed => Race?.BaseSpeed ?? 0;
		public int Speed
		{
			get
			{
				if (EquipedArmor.All(x => x.Value?.ArmorComponent == null))
				{
					return BaseSpeed;
				}
				return Math.Min(BaseSpeed, EquipedArmor.Values.Select(x => x.ArmorComponent).Where(x => x != null).Min(x => x.SpeedModifier));
			}
		}

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
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Strength, out int strength))
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
				TemporaryDexterityModifier,
				pMaximumBound: MaximumDexterityBonus);
		private int BaseDexterity { get; set; }
		private int EnhancedDexterity { get; set; }
		private int InherentDexterity { get; set; }
		private int TemporaryDexterityModifier
		{
			get
			{
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Dexterity, out int dexterity))
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
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Constitution, out int constitution))
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
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Intelligence, out int intelligence))
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
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Wisdom, out int wisdom))
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
				if (Race == null || !Race.TryGetAbilityScore(AbilityType.Charisma, out int charisma))
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

		private int NaturalBonus =>
			Effects
				?.Where(x => x.Active && x.ArmorClassNaturalModifier != 0)
				.Sum(x => x.ArmorClassNaturalModifier) ?? 0;

		private int DeflectBonus =>
			Effects
				?.Where(x => x.Active && x.Type == EffectType.Deflection)
				.Sum(x => x.ArmorClassOtherModifier) ?? 0;

		private int DodgeBonus =>
			Effects
				?.Where(x => x.Active && x.Type == EffectType.Dodge)
				.Sum(x => x.ArmorClassOtherModifier) ?? 0;

		private int TemporaryBonus =>
			Effects
				?.Where(x => x.Active
							&& x.Type != EffectType.Deflection
							&& x.Type != EffectType.Dodge)
				.Sum(x => x.ArmorClassNaturalModifier) ?? 0;

		public int BaseAttackBonus { get { return Classes.Sum(x => x.BaseAttackBonus); } }

		public ISavingThrow Fortitude =>
				new SavingThrow(
					SavingThrowType.Fortitude,
					Constitution,
					BaseFortitude,
					FortitudeResistance,
					TemporaryFortitude,
					MiscellaneousFortitudeModifier);
		public int BaseFortitude => Classes.Sum(x => x.Fortitude);
		private int FortitudeResistance =>
			Effects
				?.Where(x => x.Active && x.Type == EffectType.Resistance)
				.Sum(x => x.FortitudeModifier) ?? 0;
		private int TemporaryFortitude =>
			Effects
				?.Where(x => x.Active && x.Type != EffectType.Resistance)
				.Sum(x => x.FortitudeModifier) ?? 0;

		private int MiscellaneousFortitudeModifier { get; set; }

		public ISavingThrow Reflex =>
				new SavingThrow(
					SavingThrowType.Reflex,
					Dexterity ?? new AbilityScore(AbilityType.Dexterity, 0),
					BaseReflex,
					ReflexResistance,
					TemporaryReflex,
					MiscellaneousReflexModifier);
		public int BaseReflex => Classes.Sum(x => x.Reflex);
		private int ReflexResistance =>
			Effects
				?.Where(x => x.Active && x.Type == EffectType.Resistance)
				.Sum(x => x.ReflexModifier) ?? 0;
		private int TemporaryReflex =>
			Effects
				?.Where(x => x.Active && x.Type != EffectType.Resistance)
				.Sum(x => x.ReflexModifier) ?? 0;

		private int MiscellaneousReflexModifier { get; set; }

		public ISavingThrow Will =>
				new SavingThrow(
					SavingThrowType.Will,
					Wisdom,
					BaseWill,
					WillResistance,
					TemporaryWill,
					MiscellaneousWillModifier);
		public int BaseWill => Classes.Sum(x => x.Will);
		private int WillResistance =>
			Effects
				?.Where(x => x.Active && x.Type == EffectType.Resistance)
				.Sum(x => x.WillModifier) ?? 0;
		private int TemporaryWill =>
			Effects
				?.Where(x => x.Active && x.Type != EffectType.Resistance)
				.Sum(x => x.WillModifier) ?? 0;

		private int MiscellaneousWillModifier { get; set; }

		public IOffensiveScore Melee =>
			new OffensiveScore(
				OffensiveType.Melee,
				Strength,
				BaseAttackBonus,
				(int)Size,
				TemporaryMeleeModifier,
				MiscellaneousMeleeModifier);
		private int TemporaryMeleeModifier =>
			Effects
				?.Where(x => x.Active)
				.Sum(x => x.MeleeAttackModifier) ?? 0;

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

		public IEnumerable<IFeat> Feats { get; private set; } = ImmutableList.Create<IFeat>();

		public int MaxSkillRanks => Classes.Sum(x => x.Level * (Intelligence.Modifier + x.SkillAddend));
		private IRepository<ISkill> SkillRepository { get; }
		private IEnumerable<ISkill> Skills => SkillRepository;

		private ImmutableDictionary<ISkill, int> SkillRanks { get; set; } = new Dictionary<ISkill, int>().ToImmutableDictionary();
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
				return
					new SkillScore(
						pSkill,
						this[pSkill.AbilityType],
						ranks,
						classModifier,
						misc,
						temp,
						ArmorCheckPenalty);
			}
		}

		public ISkillScore this[string pSkillName]
		{
			get
			{
				var skill = Skills.FirstOrDefault(x => x.Name.Equals(pSkillName));
				return this[skill];
			}
		}

		public IInventory Inventory { get; private set; } = new Inventory();
		public IEnumerable<IItem> Weapons =>
			Inventory
				.Select(x => x.Key)
				.Where(x => x.WeaponComponent != null)
				.ToImmutableList();

		public IDictionary<ItemType, IItem> EquipedArmor => _equipedArmor;

		private int ArmorBonus
		{
			get
			{
				if (EquipedArmor.All(x => x.Value?.ArmorComponent == null))
				{
					return 0;
				}
				return EquipedArmor
					.Select(x => x.Value?.ArmorComponent)
					.Sum(x => x?.ArmorBonus ?? 0);
			}
		}

		private int ShieldBonus
		{
			get
			{
				if (EquipedArmor.All(x => x.Value?.ArmorComponent == null))
				{
					return 0;
				}
				return EquipedArmor
					.Select(x => x.Value?.ArmorComponent)
					.Sum(x => x?.ShieldBonus ?? 0);
			}
		}

		public int ArmorCheckPenalty
		{
			get
			{
				if (EquipedArmor.All(x => x.Value?.ArmorComponent == null))
				{
					return 0;
				}
				return EquipedArmor
					.Select(x => x.Value?.ArmorComponent)
					.Sum(x => x?.ArmorCheckPenalty ?? 0);
			}
		}

		private int MaximumDexterityBonus
		{
			get
			{
				if (EquipedArmor.All(x => x.Value?.ArmorComponent == null))
				{
					return -1; // Special Value used by AbilityScore class to disable limit.
				}
				return EquipedArmor
					.Select(x => x.Value?.ArmorComponent)
					.Where(x => x != null && x.MaximumDexterityBonus > 0)
					.Min(x => x.MaximumDexterityBonus);
			}
		}

		public IEnumerable<IEffect> Effects { get; private set; }

		public IPurse Purse { get; private set; } = new Purse(0);

		public IEnumerable<ITrait> Traits => Race?.Traits;

		/*
		 * Apply Methods
		 */
		protected override void Apply(IEvent pEvent)
		{
			Version = pEvent.Version;
			switch (pEvent)
			{
				case CharacterCreated asEvent:
					ApplyEvent(asEvent);
					break;
				case NameSet asEvent:
					ApplyEvent(asEvent);
					break;
				case AgeSet asEvent:
					ApplyEvent(asEvent);
					break;
				case RaceSet asEvent:
					ApplyEvent(asEvent);
					break;
				case LanguageAdded asEvent:
					ApplyEvent(asEvent);
					break;
				case LanguageRemoved asEvent:
					ApplyEvent(asEvent);
					break;
				case AlignmentSet asEvent:
					ApplyEvent(asEvent);
					break;
				case HomelandSet asEvent:
					ApplyEvent(asEvent);
					break;
				case HeightSet asEvent:
					ApplyEvent(asEvent);
					break;
				case WeightSet asEvent:
					ApplyEvent(asEvent);
					break;
				case DeitySet asEvent:
					ApplyEvent(asEvent);
					break;
				case GenderSet asEvent:
					ApplyEvent(asEvent);
					break;
				case EyesSet asEvent:
					ApplyEvent(asEvent);
					break;
				case HairSet asEvent:
					ApplyEvent(asEvent);
					break;
				case StrengthSet asEvent:
					ApplyEvent(asEvent);
					break;
				case DexteritySet asEvent:
					ApplyEvent(asEvent);
					break;
				case ConstitutionSet asEvent:
					ApplyEvent(asEvent);
					break;
				case IntelligenceSet asEvent:
					ApplyEvent(asEvent);
					break;
				case WisdomSet asEvent:
					ApplyEvent(asEvent);
					break;
				case CharismaSet asEvent:
					ApplyEvent(asEvent);
					break;
				case ClassAdded asEvent:
					ApplyEvent(asEvent);
					break;
				case ClassLevelRaised asEvent:
					ApplyEvent(asEvent);
					break;
				case DamageTaken asEvent:
					ApplyEvent(asEvent);
					break;
				case DamageHealed asEvent:
					ApplyEvent(asEvent);
					break;
				case FeatAdded asEvent:
					ApplyEvent(asEvent);
					break;
				case ItemAddedToInventory asEvent:
					ApplyEvent(asEvent);
					break;
				case ItemRemovedFromInventory asEvent:
					ApplyEvent(asEvent);
					break;
				case ExperienceAdded asEvent:
					ApplyEvent(asEvent);
					break;
				case SkillRanksAssigned asEvent:
					ApplyEvent(asEvent);
					break;
				case ArmorRemoved asEvent:
					ApplyEvent(asEvent);
					break;
				case ArmorEquiped asEvent:
					ApplyEvent(asEvent);
					break;
				case PurseSet asEvent:
					ApplyEvent(asEvent);
					break;
				default:
					throw new NotSupportedException($"Event {pEvent.GetType()} is not supported by {GetType()}.");
			}
		}

		// ReSharper disable once SuggestBaseTypeForParameter
		private void ApplyEvent(CharacterCreated pEvent)
		{
			Id = pEvent.Id;
			Version = pEvent.Version;
		}

		/*
		 * METHODS
		 */

		private int _GetSkillRanks(ISkill pSkill)
		{
			if (!SkillRanks.TryGetValue(pSkill, out int ranks))
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
			if (!MiscellenaousSkillBonuses.TryGetValue(pSkill, out int misc))
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
			newCharacter.ApplyChange(new RaceSet(newCharacter.Id, newCharacter.Version + 1, pRace), true);
			return newCharacter;
		}
		private void ApplyEvent(RaceSet pEvent)
		{
			Race = pEvent.Race;
			Languages =
				Languages
					.Union(Race?.Languages ?? new List<ILanguage>())
					.Distinct().ToList();
		}

		public ICharacter SetName(string pName)
		{
			Assert.ArgumentNotNull(pName, nameof(pName));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new NameSet(Id, newCharacter.Version + 1, pName), true);
			return newCharacter;
		}
		private void ApplyEvent(NameSet pEvent)
		{
			Name = pEvent.Name;
		}

		public ICharacter SetAge(int pAge)
		{
			Assert.IsTrue(pAge > 0, $"{nameof(pAge)} cannot be less than 1.");

			var newCharacter = _copy();
			newCharacter.ApplyChange(new AgeSet(newCharacter.Id, newCharacter.Version + 1, pAge), true);
			return newCharacter;
		}
		private void ApplyEvent(AgeSet pEvent)
		{
			Age = pEvent.Age;
		}

		public ICharacter SetAlignment(Alignment pAlignment)
		{
			var newCharacter = _copy();
			newCharacter.ApplyChange(new AlignmentSet(newCharacter.Id, newCharacter.Version + 1, pAlignment), true);
			return newCharacter;
		}
		private void ApplyEvent(AlignmentSet pEvent)
		{
			Alignment = pEvent.Alignment;
		}

		public ICharacter SetHomeland(string pHomeland)
		{
			Assert.ArgumentNotNull(pHomeland, nameof(pHomeland));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new HomelandSet(newCharacter.Id, newCharacter.Version + 1, pHomeland), true);
			return newCharacter;
		}
		private void ApplyEvent(HomelandSet pEvent)
		{
			Homeland = pEvent.Homeland;
		}

		public ICharacter SetDeity(IDeity pDeity)
		{
			Assert.ArgumentNotNull(pDeity, nameof(pDeity));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new DeitySet(newCharacter.Id, newCharacter.Version + 1, pDeity), true);
			return newCharacter;
		}
		private void ApplyEvent(DeitySet pEvent)
		{
			Deity = pEvent.Deity;
		}

		public ICharacter SetGender(Gender pGender)
		{
			var newCharacter = _copy();
			newCharacter.ApplyChange(new GenderSet(newCharacter.Id, newCharacter.Version + 1, pGender), true);
			return newCharacter;
		}
		private void ApplyEvent(GenderSet pEvent)
		{
			Gender = pEvent.Gender;
		}

		public ICharacter SetEyes(string pEyes)
		{
			Assert.ArgumentNotNull(pEyes, nameof(pEyes));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new EyesSet(newCharacter.Id, newCharacter.Version + 1, pEyes), true);
			return newCharacter;
		}
		private void ApplyEvent(EyesSet pEvent)
		{
			Eyes = pEvent.Eyes;
		}

		public ICharacter SetHair(string pHair)
		{
			Assert.ArgumentNotNull(pHair, nameof(pHair));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new HairSet(newCharacter.Id, newCharacter.Version + 1, pHair), true);
			return newCharacter;
		}
		private void ApplyEvent(HairSet pEvent)
		{
			Hair = pEvent.Hair;
		}

		public ICharacter SetHeight(string pHeight)
		{
			Assert.ArgumentNotNull(pHeight, nameof(pHeight));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new HeightSet(newCharacter.Id, newCharacter.Version + 1, pHeight), true);
			return newCharacter;
		}
		private void ApplyEvent(HeightSet pEvent)
		{
			Height = pEvent.Height;
		}

		public ICharacter SetWeight(string pWeight)
		{
			Assert.ArgumentNotNull(pWeight, nameof(pWeight));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new WeightSet(newCharacter.Id, newCharacter.Version + 1, pWeight), true);
			return newCharacter;
		}
		private void ApplyEvent(WeightSet pEvent)
		{
			Weight = pEvent.Weight;
		}

		public ICharacter AddLanguage(ILanguage pLanguage)
		{
			Assert.ArgumentNotNull(pLanguage, nameof(pLanguage));
			if (Languages.Any(pLanguage.Equals))
			{
				return this;
			}

			var newCharacter = _copy();

			newCharacter.ApplyChange(new LanguageAdded(newCharacter.Id, newCharacter.Version + 1, pLanguage), true);

			return newCharacter;
		}
		private void ApplyEvent(LanguageAdded pEvent)
		{
			var language = pEvent.Language;
			Languages = Languages.Append(language);
		}

		public ICharacter RemoveLanguage(ILanguage pLanguage)
		{
			Assert.ArgumentNotNull(pLanguage, nameof(pLanguage));
			if (Race != null && Race.Languages.Contains(pLanguage))
			{
				return this;
			}

			var newCharacter = _copy();

			newCharacter.ApplyChange(new LanguageRemoved(newCharacter.Id, newCharacter.Version + 1, pLanguage), true);

			return newCharacter;
		}
		private void ApplyEvent(LanguageRemoved pEvent)
		{
			var language = pEvent.Language;
			Languages = Languages.Where(x => !x.Equals(language));
		}

		public ICharacter SetStrength(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.ApplyChange(new StrengthSet(newCharacter.Id, newCharacter.Version + 1, pBase, pEnhanced, pInherent), true);
			return newCharacter;
		}
		// ReSharper disable once SuggestBaseTypeForParameter
		private void ApplyEvent(StrengthSet pEvent)
		{
			BaseStrength = pEvent.Base;
			EnhancedStrength = pEvent.Enhanced;
			InherentStrength = pEvent.Inherent;
		}

		public ICharacter SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.ApplyChange(new DexteritySet(newCharacter.Id, newCharacter.Version + 1, pBase, pEnhanced, pInherent), true);
			return newCharacter;
		}
		// ReSharper disable once SuggestBaseTypeForParameter
		private void ApplyEvent(DexteritySet pEvent)
		{
			BaseDexterity = pEvent.Base;
			EnhancedDexterity = pEvent.Enhanced;
			InherentDexterity = pEvent.Inherent;
		}

		public ICharacter SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.ApplyChange(new ConstitutionSet(newCharacter.Id, newCharacter.Version + 1, pBase, pEnhanced, pInherent), true);
			return newCharacter;
		}
		// ReSharper disable once SuggestBaseTypeForParameter
		private void ApplyEvent(ConstitutionSet pEvent)
		{

			BaseConstitution = pEvent.Base;
			EnhancedConstitution = pEvent.Enhanced;
			InherentConstitution = pEvent.Inherent;
		}

		public ICharacter SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.ApplyChange(new IntelligenceSet(newCharacter.Id, newCharacter.Version + 1, pBase, pEnhanced, pInherent), true);
			return newCharacter;
		}
		// ReSharper disable once SuggestBaseTypeForParameter
		private void ApplyEvent(IntelligenceSet pEvent)
		{
			BaseIntelligence = pEvent.Base;
			EnhancedIntelligence = pEvent.Enhanced;
			InherentIntelligence = pEvent.Inherent;
		}

		public ICharacter SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.ApplyChange(new WisdomSet(newCharacter.Id, newCharacter.Version + 1, pBase, pEnhanced, pInherent), true);
			return newCharacter;
		}
		// ReSharper disable once SuggestBaseTypeForParameter
		private void ApplyEvent(WisdomSet pEvent)
		{
			BaseWisdom = pEvent.Base;
			EnhancedWisdom = pEvent.Enhanced;
			InherentWisdom = pEvent.Inherent;
		}

		public ICharacter SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Assert.IsTrue(pBase > 0, $"{nameof(pBase)} cannot be less than 1.");
			Assert.IsTrue(pEnhanced >= 0, $"{nameof(pEnhanced)} cannot be less than 0.");
			Assert.IsTrue(pInherent >= 0, $"{nameof(pInherent)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.ApplyChange(new CharismaSet(newCharacter.Id, newCharacter.Version + 1, pBase, pEnhanced, pInherent), true);
			return newCharacter;
		}
		// ReSharper disable once SuggestBaseTypeForParameter
		private void ApplyEvent(CharismaSet pEvent)
		{
			BaseCharisma = pEvent.Base;
			EnhancedCharisma = pEvent.Enhanced;
			InherentCharisma = pEvent.Inherent;
		}

		public ICharacter AddClass(IClass pClass)
		{
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

			var newCharacter = _copy();

			var characterClass = new CharacterClass(pClass, pLevel, pIsFavoredClass, hitPointsList);
			newCharacter.ApplyChange(new ClassAdded(Id, newCharacter.Version + 1, characterClass), true);

			return newCharacter;
		}
		private void ApplyEvent(ClassAdded pEvent)
		{
			Classes = Classes.Append(pEvent.Class);
		}

		public ICharacter IncrementClass(IClass pClass, int pHitPoints = 0)
		{
			Assert.ArgumentNotNull(pClass, nameof(pClass));

			var characterClass = Classes.FirstOrDefault(x => x.Class.Equals(pClass));
			if (characterClass == null)
			{
				throw new ArgumentException($"Class not found. Class name was {pClass.Name}");
			}

			var newCharacter = _copy();
			newCharacter.ApplyChange(new ClassLevelRaised(Id, newCharacter.Version + 1, pClass, pHitPoints), true);
			return newCharacter;
		}
		private void ApplyEvent(ClassLevelRaised pEvent)
		{
			var characterClass = Classes.FirstOrDefault(x => x.Class.Equals(pEvent.Class));
			if (characterClass == null)
			{
				throw new ArgumentException($"Class not found. Class name was {pEvent.Class.Name}");
			}

			var hitPoints = pEvent.HitPoints > 0 ? pEvent.HitPoints : pEvent.Class.HitDie.Faces;
			var newCharacterClass = characterClass.IncrementLevel(hitPoints);
			Classes = Classes.Where(x => !x.Equals(characterClass)).Append(newCharacterClass);
		}

		public ICharacter SetDamage(int pDamage)
		{
			Assert.IsTrue(pDamage >= 0, $"{nameof(pDamage)} cannot be less than 0.");

			var newCharacter = _copy();
			newCharacter.ApplyChange(new DamageTaken(Id, newCharacter.Version + 1, pDamage), true);
			return newCharacter;
		}
		private void ApplyEvent(DamageTaken pEvent)
		{
			Damage += pEvent.Damage;
		}

		public ICharacter AddDamage(int pDamage)
		{
			var newCharacter = _copy();
			var damageEvent =
				pDamage > 0
					? (IEvent)new DamageTaken(Id, newCharacter.Version + 1, pDamage)
					: new DamageHealed(Id, newCharacter.Version + 1, -pDamage);

			newCharacter.ApplyChange(damageEvent, true);

			return newCharacter;
		}
		private void ApplyEvent(DamageHealed pEvent)
		{
			Damage -= pEvent.Healed;
		}

		public ICharacter AppendExperience(IExperienceEvent pEvent)
		{
			Assert.ArgumentNotNull(pEvent, nameof(pEvent));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new ExperienceAdded(Id, newCharacter.Version + 1, pEvent), true);

			return newCharacter;
		}
		private void ApplyEvent(ExperienceAdded pEvent)
		{
			Experience = Experience.Append(pEvent.ExperienceEvent);
		}

		public ICharacter AppendExperience(IExperience pExperience)
		{
			Assert.ArgumentNotNull(pExperience, nameof(pExperience));

			var newCharacter = _copy();
			foreach (var experienceEvent in pExperience)
			{
				newCharacter.ApplyChange(new ExperienceAdded(Id, newCharacter.Version + 1, experienceEvent), true);
			}

			return newCharacter;
		}

		public ICharacter AssignSkillPoint(ISkill pSkill, int pPoint)
		{
			Assert.ArgumentNotNull(pSkill, nameof(pSkill));

			var newCharacter = _copy();

			int originalRank =
				SkillRanks.TryGetValue(pSkill, out originalRank) ? originalRank : 0;
			int rank = pPoint + originalRank;

			newCharacter.ApplyChange(new SkillRanksAssigned(Id, newCharacter.Version + 1, pSkill, rank), true);

			return newCharacter;
		}
		private void ApplyEvent(SkillRanksAssigned pEvent)
		{
			SkillRanks = SkillRanks.Add(pEvent.Skill, pEvent.Ranks);
		}

		public ICharacter AddFeat(IFeat pFeat, string pSpecialization = null)
		{
			Assert.ArgumentNotNull(pFeat, nameof(pFeat));

			if (pFeat.IsSpecialized)
			{
				Assert.ArgumentIsNotEmpty(pSpecialization, nameof(pSpecialization));
			}

			var newCharacter = _copy();
			newCharacter.ApplyChange(new FeatAdded(Id, newCharacter.Version + 1, pFeat, pSpecialization), true);

			return newCharacter;
		}
		private void ApplyEvent(FeatAdded pEvent)
		{
			Feats = Feats.Append(new Feat(pEvent.Feat, pEvent.Specialization));
		}

		public ICharacter SetPurse(int pCopper, int pSilver = 0, int pGold = 0, int pPlatinum = 0)
		{
			var newCharacter = _copy();
			newCharacter.ApplyChange(new PurseSet(Id, newCharacter.Version + 1, pCopper, pSilver, pGold, pPlatinum), true);
			return newCharacter;
		}
		private void ApplyEvent(PurseSet pEvent)
		{
			Purse =
				Purse.Add(
					pEvent.Copper,
					pEvent.Silver,
					pEvent.Gold,
					pEvent.Platinum);
		}

		public ICharacter AddToInventory(IItem pItem)
		{
			Assert.ArgumentNotNull(pItem, nameof(pItem));

			var newCharacter = _copy();
			newCharacter.ApplyChange(new ItemAddedToInventory(Id, newCharacter.Version + 1, pItem), true);
			return newCharacter;
		}
		private void ApplyEvent(ItemAddedToInventory pEvent)
		{
			Inventory = Inventory.Add(pEvent.Item, 1);
		}

		public ICharacter RemoveFromInventory(IItem pItem)
		{
			Assert.ArgumentNotNull(pItem, nameof(pItem));
			if (!Inventory.Contains(pItem))
			{
				return this;
			}

			var newCharacter = _copy();
			newCharacter.ApplyChange(new ItemRemovedFromInventory(Id, newCharacter.Version + 1, pItem), true);
			return newCharacter;
		}
		private void ApplyEvent(ItemRemovedFromInventory pEvent)
		{
			Inventory = Inventory.Remove(pEvent.Item, 1);
		}

		public ICharacter UpdateInventory(IItem pItem)
		{
			//Tracer.Message(pMessage: $"{nameof(pItem)}: {pItem}");

			throw new NotImplementedException();
		}

		public ICharacter EquipArmor(IItem pArmorComponent)
		{
			Assert.ArgumentNotNull(pArmorComponent, nameof(pArmorComponent));

			if (!Inventory.Contains(pArmorComponent))
			{
				throw new ArgumentException("Item not in inventory.");
			}

			var newCharacter = _copy();
			newCharacter.ApplyChange(new ArmorEquiped(Id, newCharacter.Version + 1, pArmorComponent), true);
			return newCharacter;
		}
		private void ApplyEvent(ArmorEquiped pEvent)
		{
			_equipedArmor = _equipedArmor.Add(pEvent.Armor.ItemType, pEvent.Armor);
		}

		public ICharacter ReplaceArmor(IItem pArmorToReplace, IItem pArmorToEquip)
		{
			Assert.ArgumentNotNull(pArmorToReplace, nameof(pArmorToReplace));
			Assert.ArgumentNotNull(pArmorToEquip, nameof(pArmorToEquip));

			if (!Inventory.Contains(pArmorToReplace))
			{
				throw new ArgumentException($"Cannot remove item. Item not in inventory. {pArmorToReplace.Name}");
			}

			if (!Inventory.Contains(pArmorToEquip))
			{
				throw new ArgumentException($"Cannot equip item. Item not in inventory. {pArmorToEquip.Name}");
			}

			if (pArmorToReplace.ItemType != pArmorToEquip.ItemType)
			{
				throw new ArgumentException($"Item Types do not match; Equiped '{pArmorToReplace.ItemType}', Other '{pArmorToEquip.ItemType}'");
			}

			if (!_equipedArmor.TryGetValue(pArmorToReplace.ItemType, out IItem equipedItem)
				|| !Equals(equipedItem, pArmorToReplace))
			{
				throw new ArgumentException($"Armor not equiped; {pArmorToReplace.Name}");
			}

			var newCharacter = _copy();
			newCharacter.ApplyChange(new ArmorRemoved(Id, newCharacter.Version + 1, pArmorToReplace), true);
			newCharacter.ApplyChange(new ArmorEquiped(Id, newCharacter.Version + 1, pArmorToEquip), true);
			return newCharacter;
		}
		private void ApplyEvent(ArmorRemoved pEvent)
		{
			_equipedArmor = _equipedArmor.Remove(pEvent.Armor.ItemType);
		}

		/**
		 * Utility Methods
		 */

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

			var result = ComparisonUtilities.Compare(GetType().Name, Age, pOther.Age, nameof(Age));
			result &= ComparisonUtilities.Compare(GetType().Name, Alignment, pOther.Alignment, nameof(Alignment));
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, Classes, pOther.Classes, nameof(Classes));
			result &= ComparisonUtilities.Compare(GetType().Name, Deity, pOther.Deity, nameof(Deity));
			result &= ComparisonUtilities.Compare(GetType().Name, Gender, pOther.Gender, nameof(Gender));

			result &= ComparisonUtilities.Compare(GetType().Name, Eyes, pOther.Eyes, nameof(Eyes));
			result &= ComparisonUtilities.Compare(GetType().Name, Hair, pOther.Hair, nameof(Hair));
			result &= ComparisonUtilities.Compare(GetType().Name, Height, pOther.Height, nameof(Height));
			result &= ComparisonUtilities.Compare(GetType().Name, Weight, pOther.Weight, nameof(Weight));
			result &= ComparisonUtilities.Compare(GetType().Name, Homeland, pOther.Homeland, nameof(Homeland));
			result &= ComparisonUtilities.Compare(GetType().Name, Name, pOther.Name, nameof(Name));

			result &= ComparisonUtilities.Compare(GetType().Name, Race, pOther.Race, nameof(Race));

			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, Languages, pOther.Languages, nameof(Languages));

			result &= ComparisonUtilities.Compare(GetType().Name, Damage, pOther.Damage, nameof(Damage));
			result &= ComparisonUtilities.Compare(GetType().Name, Speed, pOther.Speed, nameof(Speed));

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

			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, Feats, pOther.Feats, nameof(Feats));

			//result &= Equals(Weapons, other.Weapons);
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, Inventory, pOther.Inventory, nameof(Inventory));

			//result &= Equals(EquipedArmor, other.EquipedArmor);
			//result &= Equals(Effects, other.Effects);
			result &= ComparisonUtilities.Compare(GetType().Name, Purse, pOther.Purse, nameof(Purse));

			//Tracer.Message(pMessage: $"\tComparison resulted with {result}");

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
				hashCode = (hashCode * 397) ^ Speed;
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
				hashCode = (hashCode * 397) ^ (SkillRepository?.GetHashCode() ?? 0);
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