using Pathfinder.Enums;
using Pathfinder.Interface.Currency;

namespace Pathfinder.Interface.Item
{
	public interface IItem : INamed
	{
		string Category { get; }
		IPurse Cost { get; }
		decimal Weight { get; }
		string Description { get; }
		ItemType ItemType { get; }

		IWeaponComponent WeaponComponent { get; }
		IArmorComponent ArmorComponent { get; }
	}
}