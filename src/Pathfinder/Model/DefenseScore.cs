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
			Debug.Assert(AbilityType.Dexterity == pDexterity.Type);

			Armor = pArmorBonus;
			Shield = pShieldBonus;

			Dexterity = pDexterity;

			Natural = pNaturalBonus;
			Dodge = pDodgeBonus;
		}

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
				var values = new List<int> {
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
				var score = values.Sum();

				Tracer.Message(pMessage: $"{Type} = {string.Join(" + ", values)} = {score}");

				return score;
			}
		}
		
		
		public DefensiveType Type { get; }
		public int ArmorBonus => UseArmor ? Armor : 0;
		public int ShieldBonus => UseShield ? Shield : 0;
		public int DexterityModifier
		{
			get
			{
				var modifier = Dexterity?.Modifier ?? 0;
				return UseDexterity ? modifier : 0;
			}
		}
		public int StrengthModifier
		{
			get
			{
				return Strength?.Modifier ?? 0;
			}
		}
		public int BaseAttackBonus { get; }
		public int SizeModifier { get; }
		public int DeflectBonus => Deflect;
		public int DodgeBonus => UseDodge ? Dodge : 0;
		public int MiscellaneousBonus { get; }
		public int NaturalBonus => UseNatural ? Natural : 0;
		public int TemporaryBonus => Temporary;
	}
}