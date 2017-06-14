using System;
using System.Collections.Generic;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class ClassLevel : IClassLevel, IEquatable<IClassLevel>
	{
		public ClassLevel(
			int pLevel,
			IEnumerable<int> pBaseAttackBonus,
			int pFortitude,
			int pReflex,
			int pWill,
			IEnumerable<string> pSpecials,
			IDictionary<int, int> pSpellsPerDay = null,
			IDictionary<int, int> pSpellsKnown = null,
			IDictionary<int, IEnumerable<string>> pSpells = null)
		{
			Level = pLevel;
			BaseAttackBonus = pBaseAttackBonus;
			Fortitude = pFortitude;
			Reflex = pReflex;
			Will = pWill;
			Specials = pSpecials;
			SpellsPerDay = pSpellsPerDay;
			SpellsKnown = pSpellsKnown;
			Spells = pSpells;
		}

		public int Level { get; }
		public IEnumerable<int> BaseAttackBonus { get; }
		public int Fortitude { get; }
		public int Reflex { get; }
		public int Will { get; }
		public IEnumerable<string> Specials { get; }
		public IDictionary<int, int> SpellsPerDay { get; }
		public IDictionary<int, int> SpellsKnown { get; }
		public IDictionary<int, IEnumerable<string>> Spells { get; }

		public override string ToString()
		{
			return $"[{nameof(ClassLevel)}] {Level} | {string.Join(",", BaseAttackBonus)} | {Fortitude} | {Reflex} | {Will}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IClassLevel);
		}

		public bool Equals(IClassLevel pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var result = ComparisonUtilities.Compare(GetType().Name, Level, pOther.Level, nameof(Level));
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, BaseAttackBonus, pOther.BaseAttackBonus, nameof(BaseAttackBonus));
			result &= ComparisonUtilities.Compare(GetType().Name, Fortitude, pOther.Fortitude, nameof(Fortitude));
			result &= ComparisonUtilities.Compare(GetType().Name, Reflex, pOther.Reflex, nameof(Reflex));
			result &= ComparisonUtilities.Compare(GetType().Name, Will, pOther.Will, nameof(Will));
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, Specials, pOther.Specials, nameof(Specials));
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, SpellsPerDay, pOther.SpellsPerDay, nameof(SpellsPerDay));
			result &= ComparisonUtilities.CompareEnumerables(GetType().Name, SpellsKnown, pOther.SpellsKnown, nameof(SpellsKnown));
			result &= ComparisonUtilities.CompareDictionaries(GetType().Name, Spells, pOther.Spells, nameof(Spells));

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Level;
				hashCode = (hashCode * 397) ^ (BaseAttackBonus != null ? BaseAttackBonus.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Fortitude;
				hashCode = (hashCode * 397) ^ Reflex;
				hashCode = (hashCode * 397) ^ Will;
				hashCode = (hashCode * 397) ^ (Specials != null ? Specials.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (SpellsPerDay != null ? SpellsPerDay.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (SpellsKnown != null ? SpellsKnown.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Spells != null ? Spells.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
