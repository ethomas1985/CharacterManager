using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface IWeapon
	{
		Proficiency Proficiency { get; }
		WeaponType WeaponType { get; }
		Encumbrance Encumbrance { get; }
		WeaponSize Size { get; }
		DamageType DamageType { get; }
		IEnumerable<IWeaponDamage> BaseWeaponDamage { get; }
		int CriticalThreat { get; }
		int CriticalMultiplier { get; }
		int Range { get; }

		IEnumerable<IWeaponSpecial> Specials { get; }
	}
}