using System;
using Pathfinder.Enum;
using Pathfinder.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class DefenseScore : IDefenseScore
	{
		private int _dodge;
		private int _natural;

		private DefenseScore(
			DefensiveType pDefensiveType,
			Func<int> pGetSize,
			Func<int> pGetDeflectBonus,
			Func<int> pGetTemporaryBonus)
		{
			Debug.Assert(pGetSize != null);
			Debug.Assert(pGetDeflectBonus != null);
			Debug.Assert(pGetTemporaryBonus != null);

			Type = pDefensiveType;

			GetSize = pGetSize;
			GetDeflectBonus = pGetDeflectBonus;
			GetTemporaryBonus = pGetTemporaryBonus;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pDefensiveType"></param>
		/// <param name="pGetArmorBonus"></param>
		/// <param name="pGetShieldBonus"></param>
		/// <param name="pDexterity"></param>
		/// <param name="pGetSize"></param>
		/// <param name="pGetNaturalBonus"></param>
		/// <param name="pGetDeflectBonus"></param>
		/// <param name="pGetDodgeBonus"></param>
		/// <param name="pGetTemporaryBonus"></param>
		public DefenseScore(
			DefensiveType pDefensiveType,
			Func<int> pGetArmorBonus,
			Func<int> pGetShieldBonus,
			IAbilityScore pDexterity,
			Func<int> pGetSize,
			Func<int> pGetNaturalBonus,
			Func<int> pGetDeflectBonus,
			Func<int> pGetDodgeBonus,
			Func<int> pGetTemporaryBonus) : this(pDefensiveType, pGetSize, pGetDeflectBonus, pGetTemporaryBonus)
		{
			Debug.Assert(AbilityType.Dexterity == pDexterity.Type);

			GetArmorBonus = pGetArmorBonus ?? GetZero;
			GetShieldBonus = pGetShieldBonus ?? GetZero;

			Dexterity = pDexterity;

			GetNaturalBonus = pGetNaturalBonus ?? GetZero;
			GetDodgeBonus = pGetDodgeBonus ?? GetZero;
		}

		/// <summary>
		/// Creates a DefenseScore for calculating CombatManeuverDefense scores.
		/// </summary>
		/// <param name="pDexterity">IAbilityScore configured for representing Dexterity.</param>
		/// <param name="pStrength">IAbilityScore configured for representing Strength.</param>
		/// <param name="pGetSize">Delegate that returns the current Size Modifier</param>
		/// <param name="pGetBaseAttackBonus">Delegate the returns the current BaseAttackBonus.</param>
		/// <param name="pGetDeflectBonus"></param>
		/// <param name="pGetDodgeBonus"></param>
		/// <param name="pGetTemporaryBonus"></param>
		public DefenseScore(
			Func<int> pGetBaseAttackBonus,
			IAbilityScore pStrength,
			IAbilityScore pDexterity,
			Func<int> pGetSize,
			Func<int> pGetDeflectBonus,
			Func<int> pGetDodgeBonus,
			Func<int> pGetTemporaryBonus) : this(DefensiveType.CombatManeuverDefense, pGetSize, pGetDeflectBonus, pGetTemporaryBonus)
		{
			Debug.Assert(AbilityType.Dexterity == pDexterity.Type);
			Debug.Assert(AbilityType.Strength == pStrength.Type);

			GetBaseAttackBonus = pGetBaseAttackBonus ?? GetZero;
			Strength = pStrength;
			Dexterity = pDexterity;
			GetNaturalBonus = GetZero;
			GetDodgeBonus = pGetDodgeBonus ?? GetZero;
		}

		private IAbilityScore Dexterity { get; }
		private IAbilityScore Strength { get; }
		private Func<int> GetSize { get; }
		private Func<int> GetArmorBonus { get; }
		private Func<int> GetNaturalBonus { get; }
		private Func<int> GetDeflectBonus { get; }
		private Func<int> GetDodgeBonus { get; }
		private Func<int> GetTemporaryBonus { get; }
		private Func<int> GetShieldBonus { get; }
		private Func<int> GetBaseAttackBonus { get; }
		public DefensiveType Type { get; }

		public int Score
		{
			get
			{
				var values = new List<int> {
					10,
					Type == DefensiveType.CombatManeuverDefense ? BaseAttackBonus: ArmorBonus,
					Type == DefensiveType.CombatManeuverDefense ? StrengthModifier: ShieldBonus,
					DexterityModifier,
					SizeModifier,
					Natural,
					Deflect,
					Dodge,
					MiscModifier,
					Temporary
				};
				var score = values.Sum();

				Tracer.Message(pMessage: $"{Type} = {string.Join(" + ", values)} = {score}");

				return score;
			}
		}

		public int DexterityModifier => UseDexterity ? Dexterity.Modifier : 0;
		public int StrengthModifier => Strength?.Modifier ?? 0;
		public int SizeModifier => GetSize();
		public int BaseAttackBonus => GetBaseAttackBonus();

		private bool UseDexterity => Type != DefensiveType.FlatFooted;
		private bool UseArmor => Type == DefensiveType.ArmorClass || Type == DefensiveType.FlatFooted;
		private bool UseShield => Type == DefensiveType.ArmorClass || Type == DefensiveType.FlatFooted;
		private bool UseNatural => Type != DefensiveType.Touch;
		private bool UseDodge => Type != DefensiveType.FlatFooted;

		public int ArmorBonus => UseArmor ? GetArmorBonus() : 0;
		public int ShieldBonus => UseShield ? GetShieldBonus() : 0;
		public int Deflect => GetDeflectBonus();
		public int Dodge => UseDodge ? GetDodgeBonus() : 0;
		public int MiscModifier { get; internal set; }
		public int Natural => UseNatural ? GetNaturalBonus() : 0;
		public int Temporary => GetTemporaryBonus();

		private static int GetZero()
		{
			return 0;
		}
	}
}