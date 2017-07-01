using Pathfinder.Interface;
using Pathfinder.Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
	internal class WeaponSpecialsRepository : IRepository<IWeaponSpecial>
	{
		private readonly Dictionary<string, IWeaponSpecial> _library;

		public WeaponSpecialsRepository()
		{
			_library =
				new Dictionary<string, IWeaponSpecial>
				{
					["brace"] = new WeaponSpecial("Brace", "If you use a readied action to set a brace weapon against a charge, you deal double damage on a successful hit against a charging character (see Combat)."),
					["disarm"] = new WeaponSpecial("Disarm", "When you use a disarm weapon, you get a +2 bonus on Combat Maneuver Checks to disarm an enemy."),
					["double"] = new WeaponSpecial("Double", "You can use a double weapon to fight as if fighting with two weapons, but if you do, you incur all the normal attack penalties associated with fighting with two weapons, just as if you were using a one-handed weapon and a light weapon. You can choose to wield one end of a double weapon two-handed, but it cannot be used as a double weapon when wielded in this way—only one end of the weapon can be used in any given round."),
					["monk"] = new WeaponSpecial("Monk", "A monk weapon can be used by a monk to perform a flurry of blows (see Classes)."),
					["nonlethal"] = new WeaponSpecial("Nonlethal", "These weapons deal nonlethal damage (see Combat)."),
					["reach"] = new WeaponSpecial("Reach", "You use a reach weapon to strike opponents 10 feet away, but you can't use it against an adjacent foe."),
					["trip"] = new WeaponSpecial("Trip", "You can use a trip weapon to make trip attacks. If you are tripped during your own trip attempt, you can drop the weapon to avoid being tripped."),
				};
		}

		public IEnumerable<string> Keys => _library.Keys.ToImmutableList();
		public IEnumerable<IWeaponSpecial> Values => _library.Values.ToImmutableList();

		public IWeaponSpecial this[string pKey]
		{
			get
			{
				IWeaponSpecial value;
				if (_library.TryGetValue(pKey.ToLower(), out value))
				{
					return value;
				}
				throw new KeyNotFoundException($" Invalid {typeof(IWeaponSpecial).Name} Key := \"{pKey}\"");
			}
		}

		public bool TryGetValue(string pKey, out IWeaponSpecial pValue)
		{
			return _library.TryGetValue(pKey, out pValue);
		}

		public void Save(IWeaponSpecial pValue, int pVersion)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerator<IWeaponSpecial> GetEnumerator()
		{
			return Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
