using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class CharacterClass : ICharacterClass, IEquatable<ICharacterClass>
	{
		public CharacterClass(IClass pClass, int pLevel, bool pIsFavored, IEnumerable<int> pHitPoints)
		{
			Assert.ArgumentNotNull(pClass, nameof(pClass));

			Class = pClass;
			Level = pLevel;
			IsFavored = pIsFavored;

			var hitPoints = pHitPoints ?? new List<int>();
			foreach (var hitPoint in hitPoints)
			{
				HitPoints = HitPoints.Append(hitPoint);
			}
		}

		public IClass Class { get; }
		public int Level { get; }
		public bool IsFavored { get; }

		public IEnumerable<int> HitPoints { get; private set; }  = new List<int>().ToImmutableList();

		public int SkillAddend => Class.SkillAddend;

		public int BaseAttackBonus => Class[Level]?.BaseAttackBonus?.FirstOrDefault() ?? 0;
		public int Fortitude => Class[Level]?.Fortitude ?? 0;
		public int Reflex => Class[Level]?.Reflex ?? 0;
		public int Will => Class[Level]?.Will ?? 0;

		public ICharacterClass IncrementLevel(int pHitPoints)
		{
			var newCharacterClass = _copy();

			newCharacterClass.HitPoints = HitPoints.Append(pHitPoints);

			return newCharacterClass;
		}

		public ICharacterClass Copy()
		{
			return _copy();
		}

		private CharacterClass _copy()
		{
			return (CharacterClass) MemberwiseClone();
		}

		public override string ToString()
		{
			return $"Level {Level} {Class.Name}";
		}

		public override bool Equals(object pObject)
		{
			return Equals(pObject as ICharacterClass);
		}

		public bool Equals(ICharacterClass pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var result = Equals(Class, pOther.Class);
			result &= Level == pOther.Level;
			result &= IsFavored == pOther.IsFavored;
			result &= HitPoints.SequenceEqual(pOther.HitPoints);

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Class?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ Level;
				hashCode = (hashCode * 397) ^ IsFavored.GetHashCode();
				hashCode = (hashCode * 397) ^ (HitPoints?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}
