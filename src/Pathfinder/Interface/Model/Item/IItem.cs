using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface.Model.Currency;

namespace Pathfinder.Interface.Model.Item
{
	public interface IItem : INamed
	{
		string Category { get; }
		IPurse Cost { get; }
		decimal Weight { get; }
		IEnumerable<string> Description { get; }
		ItemType ItemType { get; }

		IWeaponComponent WeaponComponent { get; }
		IArmorComponent ArmorComponent { get; }
	}
}
