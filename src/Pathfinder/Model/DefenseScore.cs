using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinder.Model
{
	internal class DefenseScore : IDefenseScore, IEquatable<IDefenseScore>
	{
		private DefenseScore(
			DefensiveType pDefensiveType,
			int pSizeModifier,
			int pDeflectBonus,
			int pTemporaryBonus,
			int pMiscellaneousModifier)
		{
			Type = pDefensiveType;

			SizeModifier = pSizeModifier;
			Deflect = pDeflectBonus;
			Temporary = pTemporaryBonus;
			MiscellaneousBonus = pMiscellaneousModifier;
		}

		public DefenseScore(
			DefensiveType pDefensiveType,
			int pArmorBonus,
			int pShieldBonus,
			IAbilityScore pDexterity,
			int pSize,
			int pNaturalBonus,
			int pDeflectBonus,
			int pDodgeBonus,
			int pTemporaryBonus,
			int pMiscellaneousModifier ) : this(pDefensiveType, pSize, pDeflectBonus, pTemporaryBonus, pMiscellaneousModifier)
		{
			Debug.Assert(pDexterity == null || AbilityType.Dexterity == pDexterity.Type);

			Armor = pArmorBonus;
			Shield = pShieldBonus;

			Dexterity = pDexterity;

			Natural = pNaturalBonus;
			Dodge = pDodgeBonus;
		}
		/// <summary>
		/// Creates a DefenseScore for <c>DefensiveType.CombatManeuverDefense</c>
		/// </summary>
		/// <param name="pBaseAttackBonus"></param>
		/// <param name="pStrength"></param>
		/// <param name="pDexterity"></param>
		/// <param name="pSize"></param>
		/// <param name="pDeflectBonus"></param>
		/// <param name="pDodgeBonus"></param>
		/// <param name="pTemporaryBonus"></param>
		/// <param name="pMiscellaneousModifier"></param>
		public DefenseScore(
			int pBaseAttackBonus,
			IAbilityScore pStrength,
			IAbilityScore pDexterity,
			int pSize,
			int pDeflectBonus,
			int pDodgeBonus,
			int pTemporaryBonus,
			int pMiscellaneousModifier) : this(DefensiveType.CombatManeuverDefense, pSize, pDeflectBonus, pTemporaryBonus, pMiscellaneousModifier)
		{
			Debug.Assert(AbilityType.Dexterity == pDexterity.Type);
			Debug.Assert(AbilityType.Strength == pStrength.Type);

			BaseAttackBonus = pBaseAttackBonus;
			Strength = pStrength;
			Dexterity = pDexterity;
			Dodge = pDodgeBonus;
		}

		private bool UseDexterity => Type != DefensiveType.FlatFooted;
		private bool UseArmor => Type == DefensiveType.ArmorClass || Type == DefensiveType.FlatFooted;
		private bool UseShield => Type == DefensiveType.ArmorClass || Type == DefensiveType.FlatFooted;
		private bool UseNatural => Type != DefensiveType.Touch;
		private bool UseDodge => Type != DefensiveType.FlatFooted;

		private IAbilityScore Dexterity { get; }
		private IAbilityScore Strength { get; }
		private int Armor { get; }
		private int Natural { get; }
		private int Deflect { get; }
		private int Dodge { get; }
		private int Temporary { get; }
		private int Shield { get; }

		public int Score
		{
			get
			{
				var score = Values.Sum();

				//Tracer.Message(pMessage: $"{Type} = {string.Join(" + ", values)} = {score}");

				return score;
			}
		}
		
		
		public DefensiveType Type { get; }
		public int ArmorBonus => UseArmor ? Armor : 0;
		public int ShieldBonus => UseShield ? Shield : 0;
		public int DexterityModifier => UseDexterity ? (Dexterity?.Modifier ?? 0) : 0;
		public int StrengthModifier => Strength?.Modifier ?? 0;
		public int BaseAttackBonus { get; }
		public int SizeModifier { get; }
		public int DeflectBonus => Deflect;
		public int DodgeBonus => UseDodge ? Dodge : 0;
		public int MiscellaneousBonus { get; }
		public int NaturalBonus => UseNatural ? Natural : 0;
		public int TemporaryBonus => Temporary;

		private IEnumerable<int> Values
			=> new List<int>
			{
				10,
				Type == DefensiveType.CombatManeuverDefense ? BaseAttackBonus: ArmorBonus,
				Type == DefensiveType.CombatManeuverDefense ? StrengthModifier: ShieldBonus,
				DexterityModifier,
				SizeModifier,
				NaturalBonus,
				DeflectBonus,
				DodgeBonus,
				MiscellaneousBonus,
				TemporaryBonus
			};

		public override string ToString()
		{
			return $"{Type}[{Score}] = {string.Join(" + ", Values)} = {Score}";
		}

		public override bool Equals(object pObject)
		{
			return Equals(pObject as IDefenseScore);
		}

		public bool Equals(IDefenseScore pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var equal =
				DexterityModifier == pOther.DexterityModifier
				&& StrengthModifier == pOther.StrengthModifier
				&& ArmorBonus == pOther.ArmorBonus
				&& NaturalBonus == pOther.NaturalBonus
				&& DeflectBonus == pOther.DeflectBonus
				&& DodgeBonus == pOther.DodgeBonus
				&& TemporaryBonus == pOther.TemporaryBonus
				&& ShieldBonus == pOther.ShieldBonus
				&& Type == pOther.Type
				&& BaseAttackBonus == pOther.BaseAttackBonus
				&& SizeModifier == pOther.SizeModifier
				&& MiscellaneousBonus == pOther.MiscellaneousBonus;

			return equal;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Dexterity?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (Strength?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Armor;
				hashCode = (hashCode*397) ^ Natural;
				hashCode = (hashCode*397) ^ Deflect;
				hashCode = (hashCode*397) ^ Dodge;
				hashCode = (hashCode*397) ^ Temporary;
				hashCode = (hashCode*397) ^ Shield;
				hashCode = (hashCode*397) ^ (int) Type;
				hashCode = (hashCode*397) ^ BaseAttackBonus;
				hashCode = (hashCode*397) ^ SizeModifier;
				hashCode = (hashCode*397) ^ MiscellaneousBonus;
				return hashCode;
			}
		}
	}
}