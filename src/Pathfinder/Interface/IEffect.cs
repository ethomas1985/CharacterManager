using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface IEffect
	{
		string Name { get; }

		EffectType Type { get; }

		bool Active { get; }

		int ArmorClassNaturalModifier { get; }
		int ArmorClassOtherModifier { get; }

		int FortitudeModifier { get; }
		int ReflexModifier { get; }
		int WillModifier { get; }

		int StrengthModifier { get; }
		int DexterityModifier { get; }
		int ConstitutionModifier { get; }
		int IntelligenceModifier { get; }
		int WisdomModifier { get; }
		int CharismaModifier { get; }

		int MeleeAttackModifier { get; }
		int MeleeDamageModifier { get; }
		int MeleeOffHandModifier { get; }
		int MeleeTwoHandedModifier { get; }

		int RangedAttackModifier { get; }
		int RangedDamageModifier { get; }

		int ExtraAttack_TwoWeaponFightingModifier { get; }
		int ExtraAttack_MeleeModifier { get; }
		int ExtraAttack_RangedModifier { get; }

		int GlobalSkillCheckModifier { get; }
		IDictionary<ISkill, int> UniqueSkillCheckModifiers { get; }
		int this[ISkill pSkill] { get; }

		int SizeModifier { get; }
	}
}