namespace Pathfinder.Interface
{
	public interface IArmor : IItem
	{
		bool IsShield { get; }

		int Bonus { get; }
		int MaximumDexterityBonus { get; }
		int ArmorCheckPenalty { get; }
		decimal ArcaneSpellFailureChance { get; }
		int Speed { get; }
	}
}
