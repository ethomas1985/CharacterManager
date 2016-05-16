using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Class : IClass, IEquatable<IClass>
	{
		public Class(
			string pName,
			ISet<Alignment> pAlignments,
			IDie pHitDie,
			int pSkillAddend,
			ISet<string> pSkills,
			IEnumerable<IClassLevel> pClassLevels,
			IEnumerable<string> pFeatures)
		{
			Alignments = pAlignments;
			HitDie = pHitDie;
			Skills = pSkills;
			IndexedClassLevels = pClassLevels?.ToDictionary(k => k.Level);
			Features = pFeatures;
			SkillAddend = pSkillAddend;
			Name = pName;
		}

		public string Name { get; }
		public ISet<Alignment> Alignments { get; }
		public IDie HitDie { get; }
		public int SkillAddend { get; }
		public ISet<string> Skills { get; }
		public IEnumerable<string> Features { get; }

		private IDictionary<int, IClassLevel> IndexedClassLevels { get; }
		public IEnumerable<IClassLevel> ClassLevels => IndexedClassLevels.Values;
		public IClassLevel this[int pLevel] => IndexedClassLevels?[pLevel];

		public bool TryGetLevel(int pLevel, out IClassLevel pValue)
		{
			return IndexedClassLevels.TryGetValue(pLevel, out pValue);
		}

		public override string ToString()
		{
			return $"Class: {Name}";
		}

		public override bool Equals(object pObject)
		{
			return Equals(pObject as IClass);
		}

		public bool Equals(IClass pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}
			var result = string.Equals(Name, pOther.Name);
			result &= Equals(Alignments, pOther.Alignments);
			result &= Equals(HitDie, pOther.HitDie);
			result &= SkillAddend == pOther.SkillAddend;
			result &= (Skills != null && pOther.Skills != null) && Skills.SetEquals(pOther.Skills);
			result &= Equals(Features, pOther.Features);

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Name?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (Alignments?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (HitDie?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ SkillAddend;
				hashCode = (hashCode*397) ^ (Skills?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (Features?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (IndexedClassLevels?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}
