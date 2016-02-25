using Pathfinder.Enum;
using Pathfinder.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Pathfinder.Model.Character
{
	internal abstract class AbstractCharacter : ICharacter
	{
		internal AbstractCharacter()
		{

		}

		public int Age { get; internal set; }
		public Alignment Alignment { get; internal set; }

		internal ICollection<IClass> classes { get; } = new List<IClass>();
		public IEnumerable<IClass> Classes
		{
			get { return classes.ToImmutableList(); }
		}

		public Deity Deity { get; internal set; }

		public string Eyes { get; internal set; }

		public Gender Gender { get; internal set; }
		public string Hair { get; internal set; }
		public decimal Height { get; internal set; }

		public string Homeland { get; internal set; }

		public string Name { get; internal set; }

		public abstract Race Race { get; }
		public abstract Size BaseSize { get; }
		public virtual Size Size { get; }

		public decimal Weight { get; internal set; }

		public IEnumerable<Language> Languages { get; } = new List<Language>();

		public int MaxHealthPoints { get; }
		public int Damage { get; }
		public int HealthPoints { get; }

		public abstract decimal BaseSpeed { get; }
		public abstract decimal ArmoredSpeed { get; }

		public IAbilityScore Strength { get; internal set; }
		public IAbilityScore Dexterity { get; internal set; }
		public IAbilityScore Constitution { get; internal set; }
		public IAbilityScore Intelligence { get; internal set; }
		public IAbilityScore Wisdom { get; internal set; }
		public IAbilityScore Charisma { get; internal set; }

		public IDefenseScore ArmorClass { get; internal set; }
		public IDefenseScore FlatFooted { get; internal set; }
		public IDefenseScore Touch { get; internal set; }
		public IDefenseScore CombatManeuverDefense { get; internal set; }

		public ISavingThrow Fortitude { get; internal set; }
		public ISavingThrow Reflex { get; internal set; }
		public ISavingThrow Will { get; internal set; }

		public int BaseAttackBonus { get { return Classes.Sum(x => x.BaseAttackBonus); } }
		public int BaseFortitude { get { return Classes.Sum(x => x.Fortitude); } }
		public int BaseReflex { get { return Classes.Sum(x => x.Reflex); } }
		public int BaseWill { get { return Classes.Sum(x => x.Will); } }

		public IOffensiveScore Melee { get; internal set; }
		public IOffensiveScore Ranged { get; internal set; }
		public IOffensiveScore CombatManeuverBonus { get; internal set; }

		public IExperience Experience { get; } = new Experience();
		public int ExperiencePoints
		{
			get { return Experience.Sum(x => x.ExperiencePoints); }
		}

		public IEnumerable<IHitDice> HitDice { get; }

		public FeatsCollection feats { get; } = new FeatsCollection();
		public IEnumerable<IFeat> Feats { get { return feats.ToImmutableList(); } }

		public SkillsCollection skills { get; } = new SkillsCollection();
		public IEnumerable<ISkill> Skills { get { return skills.Values.ToImmutableList(); } }

		public IEnumerable<IWeapon> Weapons { get; }

		public IInventory Inventory { get; } = new Inventory();
		public IEnumerable<IArmor> EquipedArmor { get; }
		public IEnumerable<IEffect> Effects { get; }

		public IPurse Purse
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		internal AbstractCharacter InternalCopy()
		{
			var character = CopyCore();

			// Todo: need to actually copy something!

			return character;
		}

		public ICharacter Copy()
		{
			return InternalCopy();
		}

		/// <summary>
		/// Returns a new instance of ICharacter for Copy().
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// I really don't like this pattern.
		/// </remarks>
		protected abstract AbstractCharacter CopyCore();

		private int GetSizeModifier()
		{
			return (int) Size;
		}

		private int GetStrengthTemporaryModifier()
		{
			return
				GetRaceStrengthModifier() +
				Effects
					.Where(x => x.Active && x.StrengthModifier != 0)
					.Sum(x => x.StrengthModifier);
		}
		protected virtual int GetRaceStrengthModifier()
		{
			return 0;
		}

		private int GetDexterityTemporaryModifier()
		{
			return
				GetRaceWisdomModifierGetRaceDexterityModifier() +
				Effects
					.Where(x => x.Active && x.DexterityModifier != 0)
					.Sum(x => x.DexterityModifier);
		}
		protected virtual int GetRaceWisdomModifierGetRaceDexterityModifier()
		{
			return 0;
		}

		private int GetConstitutionTemporaryModifier()
		{
			return
				GetRaceConstitutionModifier() +
				Effects
					.Where(x => x.Active && x.ConstitutionModifier != 0)
					.Sum(x => x.ConstitutionModifier);
		}
		protected virtual int GetRaceConstitutionModifier()
		{
			return 0;
		}

		private int GetIntelligenceTemporaryModifier()
		{
			return
				GetRaceIntelligenceModifier() +
				Effects
					.Where(x => x.Active && x.IntelligenceModifier != 0)
					.Sum(x => x.IntelligenceModifier);
		}
		protected virtual int GetRaceIntelligenceModifier()
		{
			return 0;
		}

		private int GetWisdomTemporaryModifier()
		{
			return
				GetRaceWisdomModifier() +
				Effects
					.Where(x => x.Active && x.WisdomModifier != 0)
					.Sum(x => x.WisdomModifier);
		}
		protected virtual int GetRaceWisdomModifier()
		{
			return 0;
		}

		private int GetCharismaTemporaryModifier()
		{
			return
				GetRaceCharismaModifier() +
				Effects
					.Where(x => x.Active && x.CharismaModifier != 0)
					.Sum(x => x.CharismaModifier);
		}
		protected virtual int GetRaceCharismaModifier()
		{
			return 0;
		}

		private int GetArmorBonus()
		{
			return EquipedArmor.Where(x => !x.IsShield).Sum(x => x.Bonus);
		}
		private int GetShieldBonus()
		{
			return EquipedArmor.Where(x => x.IsShield).Sum(x => x.Bonus);
		}
		private int GetNaturalBonus()
		{
			return
				Effects
					.Where(x => x.Active && x.ArmorClassNaturalModifier != 0)
					.Sum(x => x.ArmorClassNaturalModifier);
		}
		private int GetDeflectBonus()
		{
			return
				Effects
					.Where(x => x.Active && x.Type == EffectType.Deflection)
					.Sum(x => x.ArmorClassOtherModifier);
		}
		private int GetDodgeBonus()
		{
			return
				Effects
					.Where(x => x.Active && x.Type == EffectType.Dodge)
					.Sum(x => x.ArmorClassOtherModifier);
		}
		private int GetTemporaryBonus()
		{
			return
				Effects
					.Where(x => x.Active
						&& x.Type != EffectType.Deflection
						&& x.Type != EffectType.Dodge)
					.Sum(x => x.ArmorClassNaturalModifier);
		}

		private int GetFortitudeResistance()
		{
			return Effects
				.Where(x => x.Active && x.Type == EffectType.Resistance)
				.Sum(x => x.FortitudeModifier);
		}
		private int GetReflexResistance()
		{
			return Effects
				.Where(x => x.Active && x.Type == EffectType.Resistance)
				.Sum(x => x.ReflexModifier);
		}
		private int GetWillResistance()
		{
			return Effects
				.Where(x => x.Active && x.Type == EffectType.Resistance)
				.Sum(x => x.WillModifier);
		}

		private int GetFortitudeTemporary()
		{
			return Effects
				.Where(x => x.Active && x.Type != EffectType.Resistance)
				.Sum(x => x.FortitudeModifier);
		}
		private int GetReflexTemporary()
		{
			return Effects
				.Where(x => x.Active && x.Type != EffectType.Resistance)
				.Sum(x => x.ReflexModifier);
		}
		private int GetWillTemporary()
		{
			return Effects
				.Where(x => x.Active && x.Type != EffectType.Resistance)
				.Sum(x => x.WillModifier);
		}

		private int GetMeleeTemporaryModifier()
		{
			return Effects
				.Where(x => x.Active)
				.Sum(x => x.MeleeAttackModifier);
		}
		private int GetRangedTemporaryModifier()
		{
			return Effects
				.Where(x => x.Active)
				.Sum(x => x.RangedAttackModifier);
		}

		internal void SetStrength(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Strength =
				new AbilityScore(
					AbilityType.Strength,
					GetStrengthTemporaryModifier,
					pBase,
					pEnhanced,
					pInherent);
		}
		internal void SetDexterity(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Dexterity =
				new AbilityScore(
					AbilityType.Dexterity,
					GetDexterityTemporaryModifier,
					pBase,
					pEnhanced,
					pInherent);
		}
		internal void SetConstitution(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Constitution =
				new AbilityScore(
					AbilityType.Constitution,
					GetConstitutionTemporaryModifier,
					pBase,
					pEnhanced,
					pInherent);
		}
		internal void SetIntelligence(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Intelligence =
				new AbilityScore(
					AbilityType.Intelligence,
					GetIntelligenceTemporaryModifier,
					pBase,
					pEnhanced,
					pInherent);
		}
		internal void SetWisdom(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Wisdom =
				new AbilityScore(
					AbilityType.Wisdom,
					GetWisdomTemporaryModifier,
					pBase,
					pEnhanced,
					pInherent);
		}
		internal void SetCharisma(int pBase, int pEnhanced = 0, int pInherent = 0)
		{
			Charisma =
				new AbilityScore(
					AbilityType.Charisma,
					GetCharismaTemporaryModifier,
					pBase,
					pEnhanced,
					pInherent);
		}
		internal void Initialize()
		{
			ArmorClass =
				new DefenseScore(
					DefensiveType.ArmorClass,
					GetArmorBonus,
					GetShieldBonus,
					() => Dexterity,
					GetSizeModifier,
					GetNaturalBonus,
					GetDeflectBonus,
					GetDodgeBonus,
					GetTemporaryBonus);
			FlatFooted =
				new DefenseScore(
					DefensiveType.FlatFooted,
					GetArmorBonus,
					GetShieldBonus,
					null,
					GetSizeModifier,
					GetNaturalBonus,
					GetDeflectBonus,
					null,
					GetTemporaryBonus);
			Touch =
				new DefenseScore(
					DefensiveType.Touch,
					null,
					null,
					() => Dexterity,
					GetSizeModifier,
					null,
					GetDeflectBonus,
					GetDodgeBonus,
					GetTemporaryBonus);
			CombatManeuverDefense =
				new DefenseScore(
					() => BaseAttackBonus,
					() => Strength,
					() => Dexterity,
					GetSizeModifier,
					GetDeflectBonus,
					GetDodgeBonus,
					GetTemporaryBonus);

			Fortitude =
				new SavingThrow(
					SavingThrowType.Fortitude,
					() => Constitution,
					() => BaseFortitude,
					GetFortitudeResistance,
					GetFortitudeTemporary);
			Reflex =
				new SavingThrow(
					SavingThrowType.Reflex,
					() => Dexterity,
					() => BaseReflex,
					GetReflexResistance,
					GetReflexTemporary);
			Will =
				new SavingThrow(
					SavingThrowType.Will,
					() => Wisdom,
					() => BaseWill,
					GetWillResistance,
					GetWillTemporary);

			Melee =
				new OffensiveScore(
					OffensiveType.Melee,
					() => Strength,
					() => BaseAttackBonus,
					GetSizeModifier,
					GetMeleeTemporaryModifier);
			Ranged =
				new OffensiveScore(
					OffensiveType.Ranged,
					() => Dexterity,
					() => BaseAttackBonus,
					GetSizeModifier,
					GetRangedTemporaryModifier);
			CombatManeuverBonus =
				new OffensiveScore(
					OffensiveType.CombatManeuverBonus,
					() => Strength,
					() => BaseAttackBonus,
					GetSizeModifier,
					GetMeleeTemporaryModifier);
		}
	}
}