using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Race : IRace, IEquatable<IRace>
	{
		public Race(
			string pName,
			string pAdjective,
			string pDescription,
			Size pSize,
			int pBaseSpeed,
			IDictionary<AbilityType, int> pAbilityScores,
			IEnumerable<ITrait> pTraits,
			IEnumerable<ILanguage> pLanguages)
		{
			Name = pName;
			Adjective = pAdjective;
			Description = pDescription;
			Size = pSize;
			BaseSpeed = pBaseSpeed;
			AbilityScores = pAbilityScores;
			Traits = pTraits;
			Languages = pLanguages;
		}

		public string Name { get; }
		public string Adjective { get; }
		public string Description { get; }
		public Size Size { get; }
		public int BaseSpeed { get; }
		public IDictionary<AbilityType, int> AbilityScores { get; }
		public IEnumerable<ITrait> Traits { get; }
		public IEnumerable<ILanguage> Languages { get; }

		public bool TryGetAbilityScore(AbilityType pAbilityType, out int pValue)
		{
			if (AbilityScores == null)
			{
				pValue = 0;
				return false;
			}

			return AbilityScores.TryGetValue(pAbilityType, out pValue);
		}

		public override string ToString()
		{
			return $"Race: {Name}";
		}

		public override bool Equals(object pObject)
		{
			return Equals(pObject as IRace);
		}

		public bool Equals(IRace pOther)
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
			result &= string.Equals(Adjective, pOther.Adjective);
			result &= string.Equals(Description, pOther.Description);
			result &= Size == pOther.Size;
			result &= BaseSpeed == pOther.BaseSpeed;
			result &= Equals(AbilityScores, pOther.AbilityScores);
			result &= Equals(Traits, pOther.Traits);
			result &= (Languages != null && pOther.Languages != null) && Languages.SequenceEqual(pOther.Languages);

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Name?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (Adjective?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (int) Size;
				hashCode = (hashCode * 397) ^ BaseSpeed;
				hashCode = (hashCode * 397) ^ (AbilityScores?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Traits?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Languages?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}
