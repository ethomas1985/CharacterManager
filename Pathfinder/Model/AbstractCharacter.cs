using System.Collections.Generic;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Model.Interface;
using System.Linq;

namespace Pathfinder.Model
{
	internal abstract class AbstractCharacter : ICharacter
	{
		internal AbstractCharacter()
		{
			Classes = new List<IClass>();
			Languages = new List<Language>();

			Strength = new AbilityScore(AbilityType.Strength);
			Dexterity = new AbilityScore(AbilityType.Dexterity);
			Constitution = new AbilityScore(AbilityType.Constitution);
			Intelligence = new AbilityScore(AbilityType.Intelligence);
			Wisdom = new AbilityScore(AbilityType.Wisdom);
			Charisma = new AbilityScore(AbilityType.Charisma);

			ArmorClass = new DefenseScore(
				DefensiveType.ArmorClass,
				Dexterity,
				() => (int)Size,
				() => ArmorBonus,
				() => ShieldBonus);
			FlatFooted = new DefenseScore(
				DefensiveType.FlatFooted,
				Dexterity,
				() => (int)Size,
				() => ArmorBonus,
				() => ShieldBonus);
			Touch = new DefenseScore(
				DefensiveType.Touch,
				Dexterity,
				() => (int)Size,
				() => ArmorBonus,
				() => ShieldBonus);
			CombatManeuverDefense = new DefenseScore(
				Dexterity,
				Strength,
				() => (int)Size,
				() => BaseAttackBonus);

			Fortitude = new SavingThrow(
				SavingThrowType.Fortitude,
				Constitution,
				() => Classes.Sum(x => x.Fortitude));
			Reflex = new SavingThrow(
				SavingThrowType.Reflex,
				Dexterity,
				() => Classes.Sum(x => x.Reflex));
			Will = new SavingThrow(
				SavingThrowType.Will,
				Wisdom,
				() => Classes.Sum(x => x.Will));

			Melee = new OffensiveScore(
				OffensiveType.Melee,
				Strength,
				() => BaseAttackBonus);
			Ranged = new OffensiveScore(
				OffensiveType.Ranged,
				Dexterity,
				() => BaseAttackBonus);
			CombatManeuverBonus = new OffensiveScore(
				OffensiveType.CombatManeuverBonus,
				Strength,
				() => BaseAttackBonus);

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

		public int BaseAttackBonus { get; }

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
		public int ArmorBonus
		{
			get
			{
				return EquipedArmor.Where(x => !x.IsShield).Sum(x => x.Bonus);
			}
		}

		public int ShieldBonus
		{
			get
			{
				return EquipedArmor.Where(x => x.IsShield).Sum(x => x.Bonus);
			}
		}
		public IEnumerable<IArmor> EquipedArmor { get; }
	}
}