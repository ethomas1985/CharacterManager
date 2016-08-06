namespace Pathfinder.Interface.Item
{
	public interface IArmor : IItem
	{
		int ArmorBonus { get; }
		int ShieldBonus { get; }

		int MaximumDexterityBonus { get; }
		int ArmorCheckPenalty { get; }
		decimal ArcaneSpellFailureChance { get; }
		int Speed { get; }
	}
}
