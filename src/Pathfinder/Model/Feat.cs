using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;

namespace Pathfinder.Model
{
	internal class Feat : IFeat, IEquatable<IFeat>
	{
		public Feat(
			string name,
			FeatType featType,
			IEnumerable<string> prerequisites,
			string description,
			string benefit,
			string special)
		{
			Name = name;
			FeatType = featType;
			Prerequisites = prerequisites;
			Description = description;
			Benefit = benefit;
			Special = special;
		}

		public string Name { get; }
		public FeatType FeatType { get; }
		public IEnumerable<string> Prerequisites { get; }
		public string Description { get; }
		public string Benefit { get; }
		public string Special { get; }

		public override string ToString()
		{
			return $"{nameof(Feat)}: {Name}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IFeat);
		}

		public bool Equals(IFeat pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return ComparisonUtilities.CompareString(GetType().Name, Name, pOther.Name, nameof(Name))
				&& ComparisonUtilities.Compare(GetType().Name, FeatType, pOther.FeatType, nameof(FeatType))
				&& ComparisonUtilities.CompareEnumerables(GetType().Name, Prerequisites, pOther.Prerequisites, nameof(Prerequisites))
				&& ComparisonUtilities.CompareString(GetType().Name, Description, pOther.Description, nameof(Description))
				&& ComparisonUtilities.CompareString(GetType().Name, Benefit, pOther.Benefit, nameof(Benefit))
				&& ComparisonUtilities.CompareString(GetType().Name, Special, pOther.Special, nameof(Special));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Name?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (int) FeatType;
				hashCode = (hashCode * 397) ^ (Prerequisites?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Benefit?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Special?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}
