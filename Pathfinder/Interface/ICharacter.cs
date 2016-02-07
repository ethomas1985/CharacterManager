using System.Collections.Generic;
using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface ICharacter
	{
		int Age { get; }
		Alignment Alignment { get; }

		IEnumerable<IClass> Classes { get; }

		Deity Deity { get; }

		string Eyes { get; }
		Gender Gender { get; }
		string Hair { get; }
		decimal Height { get; }

		string Homeland { get; }

		string Name { get; }

		Race Race { get; }
		Size BaseSize { get; }
		Size Size { get; }

		decimal Weight { get; }

		IEnumerable<Language> Languages { get; }

		int MaxHealthPoints { get;  }
		int HealthPoints { get;  }
		int Damage { get;  }

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

		// Offensive Scores
		IOffensiveScore Melee { get; }
		IOffensiveScore Ranged { get; }
		IOffensiveScore CombatManeuverBonus { get; }

		//IDictionary<BonusType, int> Bonus { get; }

		int ExperiencePoints { get; }
		IExperience Experience { get; }
		
		IEnumerable<IHitDice> HitDice { get; }

		IEnumerable<IFeat> Feats { get; }

		IEnumerable<ISkill> Skills { get; }

		IEnumerable<IWeapon> Weapons { get; }

		IInventory Inventory { get; }

		int ArmorBonus { get; }
		int ShieldBonus { get; }

		IEnumerable<IArmor> EquipedArmor { get; }
		IEnumerable<IEffect> Effects { get; }
	}
}