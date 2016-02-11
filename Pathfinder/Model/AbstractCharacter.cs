using System;
using System.Collections.Generic;
using Pathfinder.Enum;
using Pathfinder.Interface;
using System.Linq;

namespace Pathfinder.Model
{
	internal abstract class AbstractCharacter : ICharacter
	{
		internal AbstractCharacter()
		{
			Classes = new List<IClass>();
			Languages = new List<Language>();

			Strength = new AbilityScore(AbilityType.Strength, GetStrengthTemporaryModifier);
			Dexterity = new AbilityScore(AbilityType.Dexterity, GetDexterityTemporaryModifier);
			Constitution = new AbilityScore(AbilityType.Constitution, GetConstitutionTemporaryModifier);
			Intelligence = new AbilityScore(AbilityType.Intelligence, GetIntelligenceTemporaryModifier);
			Wisdom = new AbilityScore(AbilityType.Wisdom, GetWisdomTemporaryModifier);
			Charisma = new AbilityScore(AbilityType.Charisma, GetCharismaTemporaryModifier);

			ArmorClass =
				new DefenseScore(
					DefensiveType.ArmorClass,
					GetArmorBonus,
					GetShieldBonus,
					Dexterity,
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
					Dexterity,
					GetSizeModifier,
					null,
					GetDeflectBonus,
					GetDodgeBonus,
					GetTemporaryBonus);
			CombatManeuverDefense =
				new DefenseScore(
					() => BaseAttackBonus,
					Strength,
					Dexterity,
					GetSizeModifier,
					GetDeflectBonus,
					GetDodgeBonus,
					GetTemporaryBonus);

			Fortitude = new SavingThrow(
				SavingThrowType.Fortitude,
				Constitution,
				() => BaseFortitude,
				GetFortitudeResistance,
				GetFortitudeTemporary);
			Reflex = new SavingThrow(
				SavingThrowType.Reflex,
				Dexterity,
				() => BaseReflex,
				GetReflexResistance,
				GetReflexTemporary);
			Will = new SavingThrow(
				SavingThrowType.Will,
				Wisdom,
				() => BaseWill,
				GetWillResistance,
				GetWillTemporary);

			Melee = new OffensiveScore(
				OffensiveType.Melee,
				Strength,
				() => BaseAttackBonus,
				GetSizeModifier,
				GetMeleeTemporaryModifier);
			Ranged = new OffensiveScore(
				OffensiveType.Ranged,
				Dexterity,
				() => BaseAttackBonus,
				GetSizeModifier,
				GetRangedTemporaryModifier);
			CombatManeuverBonus = new OffensiveScore(
				OffensiveType.CombatManeuverBonus,
				Strength,
				() => BaseAttackBonus,
				GetSizeModifier,
				GetMeleeTemporaryModifier);

			Experience = new Experience();

			Feats = new FeatsCollection();
			Skills = new SkillsCollection();

			Inventory = new Inventory();
		}

		public int Age { get; internal set; }
		public Alignment Alignment { get; internal set; }

		public IEnumerable<IClass> Classes { get; }

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

		public IEnumerable<Language> Languages { get; }

		public int MaxHealthPoints { get; }
		public int Damage { get; }
		public int HealthPoints { get; }

		public abstract decimal BaseSpeed { get; }
		public abstract decimal ArmoredSpeed { get; }

		public IAbilityScore Strength { get; }
		public IAbilityScore Dexterity { get; }
		public IAbilityScore Constitution { get; }
		public IAbilityScore Intelligence { get; }
		public IAbilityScore Wisdom { get; }
		public IAbilityScore Charisma { get; }

		public IDefenseScore ArmorClass { get; }
		public IDefenseScore FlatFooted { get; }
		public IDefenseScore Touch { get; }
		public IDefenseScore CombatManeuverDefense { get; }

		public ISavingThrow Fortitude { get; }
		public ISavingThrow Reflex { get; }
		public ISavingThrow Will { get; }

		public int BaseAttackBonus { get { return Classes.Sum(x => x.BaseAttackBonus); } }
		public int BaseFortitude { get { return Classes.Sum(x => x.Fortitude); } }
		public int BaseReflex { get { return Classes.Sum(x => x.Reflex); } }
		public int BaseWill { get { return Classes.Sum(x => x.Will); } }

		public IOffensiveScore Melee { get; }
		public IOffensiveScore Ranged { get; }
		public IOffensiveScore CombatManeuverBonus { get; }

		public int ExperiencePoints
		{
			get { return Experience.Sum(x => x.ExperiencePoints); }
		}
		public IExperience Experience { get; }

		public IEnumerable<IHitDice> HitDice { get; }

		public IEnumerable<IFeat> Feats { get; }
		public IEnumerable<ISkill> Skills { get; }

		public IEnumerable<IWeapon> Weapons { get; }

		public IInventory Inventory { get; }
		public IEnumerable<IArmor> EquipedArmor { get; }
		public IEnumerable<IEffect> Effects { get; }

		private int GetSizeModifier()
		{
			return (int) Size;
		}

		private int GetStrengthTemporaryModifier()
		{
			return
				Effects
					.Where(x => x.Active && x.StrengthModifier != 0)
					.Sum(x => x.StrengthModifier);
		}
		private int GetDexterityTemporaryModifier()
		{
			return
				Effects
					.Where(x => x.Active && x.DexterityModifier != 0)
					.Sum(x => x.DexterityModifier);
		}
		private int GetConstitutionTemporaryModifier()
		{
			return
				Effects
					.Where(x => x.Active && x.ConstitutionModifier != 0)
					.Sum(x => x.ConstitutionModifier);
		}
		private int GetIntelligenceTemporaryModifier()
		{
			return
				Effects
					.Where(x => x.Active && x.IntelligenceModifier != 0)
					.Sum(x => x.IntelligenceModifier);
		}
		private int GetWisdomTemporaryModifier()
		{
			return
				Effects
					.Where(x => x.Active && x.WisdomModifier != 0)
					.Sum(x => x.WisdomModifier);
		}
		private int GetCharismaTemporaryModifier()
		{
			return
				Effects
					.Where(x => x.Active && x.CharismaModifier != 0)
					.Sum(x => x.CharismaModifier);
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
	}
}