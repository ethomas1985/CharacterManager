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

		/// <summary>
		/// Creates a DefenseScore for calculating non-CombatManeuverDefense scores.
		/// </summary>
		/// <param name="pDefensiveType">DefensiveType this DefenseScore is representing.</param>
		/// <param name="pDexterity">IAbilityScore configured for representing Dexterity.</param>
		/// <param name="pGetSize">Delegate that returns the current Size Modifier</param>
		private DefenseScore(
			DefensiveType pDefensiveType,
			IAbilityScore pDexterity,
			Func<int> pGetSize)
		{
			Debug.Assert(AbilityType.Dexterity == pDexterity.Ability);
			Debug.Assert(pGetSize != null);

			Type = pDefensiveType;
			Dexterity = pDexterity;
			GetSize = pGetSize;
		}

		public DefenseScore(
		  DefensiveType pDefensiveType,
		  IAbilityScore pDexterity,
		  Func<int> pGetSize, 
		  Func<int> pGetArmorBonus, 
		  Func<int> pGetShieldBonus) : this(pDefensiveType, pDexterity, pGetSize)
		{
			GetArmorBonus = pGetArmorBonus;
			GetShieldBonus = pGetShieldBonus;
		}

		/// <summary>
		/// Creates a DefenseScore for calculating CombatManeuverDefense scores.
		/// </summary>
		/// <param name="pDexterity">IAbilityScore configured for representing Dexterity.</param>
		/// <param name="pStrength">IAbilityScore configured for representing Strength.</param>
		/// <param name="pGetSize">Delegate that returns the current Size Modifier</param>
		/// <param name="pGetBaseAttackBonus">Delegate the returns the current BaseAttackBonus.</param>
		public DefenseScore(
			IAbilityScore pDexterity,
			IAbilityScore pStrength,
			Func<int> pGetSize,
			Func<int> pGetBaseAttackBonus) : this(DefensiveType.CombatManeuverDefense, pDexterity, pGetSize)
		{
			Debug.Assert(AbilityType.Strength == pStrength.Ability);

			Strength = pStrength;
			GetBaseAttackBonus = pGetBaseAttackBonus;
		}

		private IAbilityScore Dexterity { get; }
		private IAbilityScore Strength { get; }
		private Func<int> GetSize { get; }
		private Func<int> GetArmorBonus { get; }
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

		public int DexterityModifier { get { return UseDexterity ? Dexterity.Modifier : 0; } }
		public int StrengthModifier { get { return Strength?.Modifier ?? 0; } }
		public int SizeModifier { get { return GetSize(); } }
		public int BaseAttackBonus { get { return GetBaseAttackBonus(); } }

		private bool UseDexterity { get { return Type != DefensiveType.FlatFooted; } }
		private bool UseArmor { get { return Type == DefensiveType.ArmorClass || Type == DefensiveType.FlatFooted; } }
		private bool UseShield { get { return Type == DefensiveType.ArmorClass || Type == DefensiveType.FlatFooted; } }
		private bool UseNatural { get { return Type != DefensiveType.Touch; } }
		private bool UseDodge { get { return Type != DefensiveType.FlatFooted; } }

		// TODO: Armor Bonus needs to be backed by a delegate to tie into the Character's equipped armor bonus.
		public int ArmorBonus
		{
			get
			{
				return UseArmor ? GetArmorBonus() : 0;
			}
		}

		// TODO: Shield Bonus needs to be backed by a delegate to tie into the Character's equipped Shield bonus.
		public int ShieldBonus
		{
			get { return UseShield ? GetShieldBonus() : 0; }
		}

		public int Deflect { get; internal set; }

		public int Dodge
		{
			get { return UseDodge ? _dodge : 0; }
			internal set { _dodge = value; }
		}

		public int MiscModifier { get; internal set; }

		public int Natural
		{
			get { return UseNatural ? _natural : 0; }
			internal set { _natural = value; }
		}

		public int Temporary { get; internal set; }

		internal int this[string pPropertyName]
		{
			get
			{
				switch (pPropertyName)
				{
					case nameof(Score):
						return Score;
					case nameof(ArmorBonus):
						return ArmorBonus;
					case nameof(BaseAttackBonus):
						return BaseAttackBonus;
					case nameof(ShieldBonus):
						return ShieldBonus;
					case nameof(Deflect):
						return Deflect;
					case nameof(DexterityModifier):
						return DexterityModifier;
					case nameof(StrengthModifier):
						return StrengthModifier;
					case nameof(Dodge):
						return Dodge;
					case nameof(MiscModifier):
						return MiscModifier;
					case nameof(Natural):
						return Natural;
					case nameof(SizeModifier):
						return SizeModifier;
					case nameof(Temporary):
						return Temporary;
					default:
						throw new ArgumentException($"'{pPropertyName}' is not a valid Property.");
				}
			}
			set
			{
				switch (pPropertyName)
				{
					case nameof(Deflect):
						Deflect = value;
						break;
					case nameof(Dodge):
						Dodge = value;
						break;
					case nameof(MiscModifier):
						MiscModifier = value;
						break;
					case nameof(Natural):
						Natural = value;
						break;
					case nameof(Temporary):
						Temporary = value;
						break;
					default:
						throw new ArgumentException($"'{pPropertyName}' is not a valid Property.");
				}
			}
		}
	}
}