using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface IDefenseScore
	{
		DefensiveType Type { get; }

		int Score { get; }

		/// <summary>
		/// For <c>DefensiveType.ArmorClass</c> and <c>FlatFooted</c> scores, 
		///		this will pull from the Characters Armor.
		/// For <c>DefensiveType.Touch</c>, this value will be 0.
		/// For <c>DefensiveType.CombatManeuverDefense</c>, this will pull 
		///		from Base Attack Bonus.
		/// </summary>
		/// <see cref="DefensiveType"/>
		int ArmorBonus { get; }
		int ShieldBonus { get; }
		int DexterityModifier { get; }
		int SizeModifier { get; }
		int NaturalBonus { get; }
		int DeflectBonus { get; }
		int DodgeBonus { get; }

		int MiscellaneousBonus { get; }

		int TemporaryBonus { get; }
		int BaseAttackBonus { get; }
		int StrengthModifier { get; }
	}
}
