using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface.Model
{
	public interface IEffect: INamed
	{
		string Text { get; }

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

		int ExtraAttackTwoWeaponFightingModifier { get; }
		int ExtraAttackMeleeModifier { get; }
		int ExtraAttackRangedModifier { get; }

		int GlobalSkillCheckModifier { get; }

		int SizeModifier { get; }

		IEnumerable<string> AffectedProperties{ get; }
		int this[string pProperty] { get; }
	}
}