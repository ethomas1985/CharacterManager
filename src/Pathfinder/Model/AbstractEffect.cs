using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	public abstract class AbstractEffect : IEffect, IEquatable<IEffect>
	{
		protected AbstractEffect(string pName, EffectType pType, bool pActive, string pText, IDictionary<string, int> pPropertyModifiers)
		{
			Name = pName;
			Type = pType;
			Active = pActive;
			Text = pText;

			PropertyModifiers =
				pPropertyModifiers as ImmutableDictionary<string, int>
					?? pPropertyModifiers.ToImmutableDictionary();
		}

		public string Name { get; }
		public EffectType Type { get; }
		public string Text { get; }

		public virtual bool Active { get; }

		protected IDictionary<string, int> PropertyModifiers { get; }

		public int ArmorClassNaturalModifier => PropertyModifiers.TryGetValue(nameof(ArmorClassNaturalModifier), out int value) ? value : 0;
		public int ArmorClassOtherModifier => PropertyModifiers.TryGetValue(nameof(ArmorClassOtherModifier), out int value) ? value : 0;

		public int FortitudeModifier => PropertyModifiers.TryGetValue(nameof(FortitudeModifier), out int value) ? value : 0;
		public int ReflexModifier => PropertyModifiers.TryGetValue(nameof(ReflexModifier), out int value) ? value : 0;
		public int WillModifier => PropertyModifiers.TryGetValue(nameof(WillModifier), out int value) ? value : 0;

		public int StrengthModifier => PropertyModifiers.TryGetValue(nameof(StrengthModifier), out int value) ? value : 0;
		public int DexterityModifier => PropertyModifiers.TryGetValue(nameof(DexterityModifier), out int value) ? value : 0;
		public int ConstitutionModifier => PropertyModifiers.TryGetValue(nameof(ConstitutionModifier), out int value) ? value : 0;
		public int IntelligenceModifier => PropertyModifiers.TryGetValue(nameof(IntelligenceModifier), out int value) ? value : 0;
		public int WisdomModifier => PropertyModifiers.TryGetValue(nameof(WisdomModifier), out int value) ? value : 0;
		public int CharismaModifier => PropertyModifiers.TryGetValue(nameof(CharismaModifier), out int value) ? value : 0;

		public int MeleeAttackModifier => PropertyModifiers.TryGetValue(nameof(MeleeAttackModifier), out int value) ? value : 0;
		public int MeleeDamageModifier => PropertyModifiers.TryGetValue(nameof(MeleeDamageModifier), out int value) ? value : 0;

		public int MeleeOffHandModifier => PropertyModifiers.TryGetValue(nameof(MeleeOffHandModifier), out int value) ? value : 0;

		public int MeleeTwoHandedModifier => PropertyModifiers.TryGetValue(nameof(MeleeTwoHandedModifier), out int value) ? value : 0;

		public int RangedAttackModifier => PropertyModifiers.TryGetValue(nameof(RangedAttackModifier), out int value) ? value : 0;
		public int RangedDamageModifier => PropertyModifiers.TryGetValue(nameof(RangedDamageModifier), out int value) ? value : 0;

		public int ExtraAttackTwoWeaponFightingModifier => PropertyModifiers.TryGetValue(nameof(ExtraAttackTwoWeaponFightingModifier), out int value) ? value : 0;
		public int ExtraAttackMeleeModifier => PropertyModifiers.TryGetValue(nameof(ExtraAttackMeleeModifier), out int value) ? value : 0;
		public int ExtraAttackRangedModifier => PropertyModifiers.TryGetValue(nameof(ExtraAttackRangedModifier), out int value) ? value : 0;

		public int GlobalSkillCheckModifier => PropertyModifiers.TryGetValue(nameof(GlobalSkillCheckModifier), out int value) ? value : 0;

		public int SizeModifier => PropertyModifiers.TryGetValue(nameof(SizeModifier), out int value) ? value : 0;

		public int this[string pProperty] => PropertyModifiers.TryGetValue(pProperty, out int value) ? value : 0;

		public IEnumerable<string> AffectedProperties => PropertyModifiers.Keys;

		public override string ToString()
		{
			return $"{Name} | {Text}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IEffect);
		}

		public bool Equals(IEffect pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var result = ComparisonUtilities.Compare(GetType().Name, Name, pOther.Name, nameof(Name))
				&& ComparisonUtilities.Compare(GetType().Name, Type, pOther.Type, nameof(Type))
				&& ComparisonUtilities.Compare(GetType().Name, Text, pOther.Text, nameof(Text))
				&& ComparisonUtilities.Compare(GetType().Name, Active, pOther.Active, nameof(Active));

			return AffectedProperties
				.Aggregate(result, 
				(current, affectedProperty) => current & ComparisonUtilities.Compare(GetType().Name, this[affectedProperty], pOther[affectedProperty], $"{nameof(IEffect)}['{affectedProperty}']"));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (int)Type;
				hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Active.GetHashCode();
				hashCode = (hashCode * 397) ^ (PropertyModifiers != null ? PropertyModifiers.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}