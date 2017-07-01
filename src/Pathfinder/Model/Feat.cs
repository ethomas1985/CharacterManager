using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using Pathfinder.Interface.Model;

namespace Pathfinder.Model
{
	internal class Feat : IFeat, IEquatable<IFeat>
	{
		public Feat(
			string pName,
			FeatType pFeatType,
			IEnumerable<string> pPrerequisites,
			string pDescription,
			string pBenefit,
			string pSpecial,
			string pSpecialization = null)
		{
			Name = pName;
			FeatType = pFeatType;
			Prerequisites = pPrerequisites;
			Description = pDescription;
			Benefit = pBenefit;
			Special = pSpecial;
			Specialization = pSpecialization;
		}

		internal Feat(IFeat pFeat,
					  string pSpecialization = null)
		{
			Name = pFeat.Name;
			FeatType = pFeat.FeatType;
			Prerequisites = pFeat.Prerequisites;
			Description = pFeat.Description;
			Benefit = pFeat.Benefit;
			Special = pFeat.Special;
			Specialization = pSpecialization;
		}

		public string Name { get; }
		public FeatType FeatType { get; }

		public bool IsSpecialized => !string.IsNullOrWhiteSpace(Specialization);
		public string Specialization { get; }

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

			return ComparisonUtilities.Compare(GetType().Name, Name, pOther.Name, nameof(Name))
				&& ComparisonUtilities.Compare(GetType().Name, IsSpecialized, pOther.IsSpecialized, nameof(IsSpecialized))
				&& ComparisonUtilities.Compare(GetType().Name, Specialization, pOther.Specialization, nameof(Specialization))
				&& ComparisonUtilities.Compare(GetType().Name, FeatType, pOther.FeatType, nameof(FeatType))
				&& ComparisonUtilities.CompareEnumerables(GetType().Name, Prerequisites, pOther.Prerequisites, nameof(Prerequisites))
				&& ComparisonUtilities.Compare(GetType().Name, Description, pOther.Description, nameof(Description))
				&& ComparisonUtilities.Compare(GetType().Name, Benefit, pOther.Benefit, nameof(Benefit))
				&& ComparisonUtilities.Compare(GetType().Name, Special, pOther.Special, nameof(Special));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Name?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (int) FeatType;
				hashCode = (hashCode * 397) ^ (Specialization?.GetHashCode() ?? 0);
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
