using Pathfinder.Enum;

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
		int Base { get; }
		int Shield { get; }
		int DexterityModifier { get; }
		int SizeModifier { get; }
		int Natural { get; }
		int Deflect { get; }
		int Dodge { get; }

		int MiscModifier { get; }

		int Temporary { get; }
	}
}
