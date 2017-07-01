namespace Pathfinder.Interface.Model.Item
{
	public interface IArmorComponent
	{
		int ArmorBonus { get; }
		int ShieldBonus { get; }

		int MaximumDexterityBonus { get; }
		int ArmorCheckPenalty { get; }
		decimal ArcaneSpellFailureChance { get; }
		int SpeedModifier { get; }
	}
}
