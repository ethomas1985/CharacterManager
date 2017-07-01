using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface.Model.Item
{
	public interface IWeaponComponent
	{
		Proficiency Proficiency { get; }
		WeaponType WeaponType { get; }
		Encumbrance Encumbrance { get; }
		WeaponSize Size { get; }
		DamageType DamageType { get; }
		IEnumerable<IDice> BaseWeaponDamage { get; }
		int CriticalThreat { get; }
		int CriticalMultiplier { get; }
		int Range { get; }

		IEnumerable<IWeaponSpecial> Specials { get; }
	}
}